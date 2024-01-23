// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using Microsoft.Extensions.Logging;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Converters;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Plugin;
using Playground.Core;
using Playground.Core.Converters;
using Playground.Droid.Bindings;
using Playground.Droid.Controls;
using Serilog;
using Serilog.Extensions.Logging;

namespace Playground.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies =>
            new List<Assembly>(base.AndroidViewAssemblies)
            {
                typeof(MvxRecyclerView).Assembly
            };

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<BinaryEdit>(
                "MyCount",
                (arg) => new BinaryEditTargetBinding(arg));

            base.FillTargetFactories(registry);
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            registry.AddOrOverwrite("TextToColor", new TextToColorValueConverter());
        }

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);

            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Visibility.Platforms.Android.Plugin>();
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Color.Platforms.Android.Plugin>();
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Json.Plugin>();
        }

        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override ILoggerFactory CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Async(a => a.AndroidLog())
                .WriteTo.Async(a => a.Trace())
                .CreateLogger();

            return new SerilogLoggerFactory();
        }
    }
}
