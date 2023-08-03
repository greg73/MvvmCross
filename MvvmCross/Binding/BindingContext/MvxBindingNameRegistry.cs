// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace MvvmCross.Binding.BindingContext
{
    public class MvxBindingNameRegistry
        : IMvxBindingNameLookup, IMvxBindingNameRegistry
    {
        private readonly Dictionary<Type, string> _lookup = new();

        public string DefaultFor([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] Type type)
        {
            string toReturn;
            TryDefaultFor(type, out toReturn, true);
            return toReturn;
        }

        private bool TryDefaultFor(
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] Type type,
            out string toReturn, bool includeInterfaces = true)
        {
            if (type == typeof(object))
            {
                toReturn = null;
                return false;
            }

            if (_lookup.TryGetValue(type, out toReturn))
                return true;

            if (type.IsConstructedGenericType)
            {
                var openType = type.GetGenericTypeDefinition();
                if (_lookup.TryGetValue(openType, out toReturn))
                    return true;
            }

            if (type.GetTypeInfo().IsInterface)
                return false;

            if (includeInterfaces)
            {
                var interfaces = type.GetInterfaces();
                foreach (var iface in interfaces)
                {
                    if (TryDefaultFor(iface, out toReturn, false))
                        return true;
                }
            }

            return TryDefaultFor(type.GetTypeInfo().BaseType, out toReturn, false);
        }

        public void AddOrOverwrite([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] Type type, string name)
        {
            _lookup[type] = name;
        }

        public void AddOrOverwrite<T>(Expression<Func<T, object>> nameExpression)
        {
            var path = MvxBindingSingletonCache.Instance?.PropertyExpressionParser.Parse(nameExpression);
            _lookup[typeof(T)] = path?.Print();
        }
    }
}
