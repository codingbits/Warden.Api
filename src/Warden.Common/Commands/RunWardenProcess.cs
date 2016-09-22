﻿namespace Warden.Common.Commands
{
    public class RunWardenProcess : ICommand
    {
        public string ConfigurationId { get; }
        public string Token { get; }

        protected RunWardenProcess()
        {
        }

        public RunWardenProcess(string configurationId, string token)
        {
            ConfigurationId = configurationId;
            Token = token;
        }
    }
}