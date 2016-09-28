﻿using Autofac;
using Warden.Api.Core.Mongo.Repositories;
using Warden.Api.Core.Repositories;

namespace Warden.Api.Core.IoC.Modules
{
    public class MongoRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrganizationRepository>()
                .As<IOrganizationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WardenConfigurationRepository>()
                .As<IWardenConfigurationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SecuredRequestRepository>()
                .As<ISecuredRequestRepository>()
                .InstancePerLifetimeScope();
        }
    }
}