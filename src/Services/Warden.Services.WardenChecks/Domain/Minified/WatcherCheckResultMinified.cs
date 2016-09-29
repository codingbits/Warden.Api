﻿namespace Warden.Services.WardenChecks.Domain.Minified
{
    public class WatcherCheckResultMinified
    {
        public string n { get; set; }
        public string t { get; set; }
        public string d { get; set; }
        public bool v { get; set; }

        public WatcherCheckResultMinified()
        {
        }

        public WatcherCheckResultMinified(WatcherCheckResult check)
        {
            n = check.Watcher.Name;
            t = check.Watcher.Type;
            d = check.Description;
            v = check.IsValid;
        }
    }
}