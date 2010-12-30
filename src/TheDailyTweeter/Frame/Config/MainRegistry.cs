﻿using System;
using MemBus;
using MemBus.Configurators;
using Membus.WpfTwitterClient.Frame.Twitter;
using Membus.WpfTwitterClient.Frame.UI;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using MemBus.Support;
using TheDailyTweeter.Properties;

namespace Membus.WpfTwitterClient.Frame.Config
{
    internal class MainRegistry : Registry
    {
        public MainRegistry()
        {
            Scan(s =>
                     {
                         s.AssembliesFromApplicationBaseDirectory();
                         s.AddAllTypesOf(typeof (IHandles<>));
                         s.Convention<ThingsToBeSingletonsConvention>();
                     });
            ForSingletonOf<IBus>().Use(constructBus);
            For(typeof (IObservable<>)).Use(typeof (MessageObservable<>));
            For<IConfigReader>().Use<AppConfigReader>();
            For<TwitterKeys>().Use(ctx => ctx.GetInstance<IConfigReader>().GetSection<TwitterKeys>());
            ForSingletonOf<ITwitterSession>().Use<TwitterSession>();
            //For<IUserSettings>().Use(Settings.Default);
            For<IUserSettings>().Use(new CheapUserSettings());
            Forward<Settings,IUserSettings>();
            Forward<IBus,ISubscriber>();
        }

        private static IBus constructBus()
        {
            return BusSetup.StartWith<AsyncRichClientFrontend, ClientPublishingConventions>(
                new IoCSupport(new ServiceLocator(() => ObjectFactory.Container)))
                .Construct();
        }
    }

    internal class ThingsToBeSingletonsConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.HasAttribute<SingleAttribute>())
                registry.For(type).LifecycleIs(InstanceScope.Singleton);
        }
    }
}