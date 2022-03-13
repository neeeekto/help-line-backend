﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;
using HelpLine.Modules.UserAccess.Application.Configuration.Commands;
using FluentValidation;
using HelpLine.BuildingBlocks.Infrastructure.DI;
using MediatR;
using MediatR.Pipeline;

namespace HelpLine.Modules.UserAccess.Infrastructure.Configuration.Mediation
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterSource(new ScopedContravariantRegistrationSource(
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>)
            ));

            var mediatorOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>)
            };

            foreach (var mediatorOpenType in mediatorOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(ThisAssembly, Assemblies.Application)
                    .AsClosedTypesOf(mediatorOpenType)
                    .AsImplementedInterfaces()
                    .FindConstructorsWith(new AllConstructorFinder());
            }

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            }).InstancePerLifetimeScope();
        }
    }
}
