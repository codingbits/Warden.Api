﻿using Warden.Services.Host;
using Warden.Services.Organizations.Framework;

namespace Warden.Services.Organizations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10002)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq()
                .Build()
                .Run();
        }
    }
}
