﻿using System.Reflection;

namespace HelpLine.Services.Jobs.Infrastructure
{
    internal static class ServiceInfo
    {
        public static string NameSpace = "Jobs";
        public static Assembly Assembly = typeof(ServiceInfo).Assembly;
    }
}
