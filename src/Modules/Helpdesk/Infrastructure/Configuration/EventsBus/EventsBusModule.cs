﻿using Autofac;
using HelpLine.BuildingBlocks.Bus.EventsBus;

namespace HelpLine.Modules.Helpdesk.Infrastructure.Configuration.EventsBus
{
    internal class EventsBusModule : Autofac.Module
    {
        private readonly IEventBusFactory _busFactory;

        public EventsBusModule(IEventBusFactory busFactory)
        {
            _busFactory = busFactory;
        }


        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_busFactory.MakeEventsBus("HelpLine.Helpdesk.EventBus"))
                .As<IEventsBus>()
                .SingleInstance();
        }
    }
}
