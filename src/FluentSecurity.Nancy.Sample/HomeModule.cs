﻿using Nancy;

namespace FluentSecurity.Nancy.Sample
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => "Hello from Nancy running on CoreCLR");
        }
    }
}