﻿using Nancy;

namespace Warden.Services.Spawn.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule()
        {
            Get("/", args => Response.AsJson(new { name = "Warden.Services.Spawn" }));
        }
    }
}