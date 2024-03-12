﻿using System;
using System.Diagnostics;
using NLog;

namespace hCMD
{
    internal class ProcessExecutor
    {
        private static ProcessExecutor _instance;
        private static readonly object _lock = new object();
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private ProcessExecutor() { }

        public static ProcessExecutor GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ProcessExecutor();
                    logger.Trace("ProcessExecutor instance created");
                }
                return _instance;
            }
        }
        public void Execute(string processName, string arguments)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = processName;
                process.StartInfo.Arguments = arguments;
                process.Start();
                logger.Info($"Process {processName} started with arguments {arguments}");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error executing process");
            }
        }
    }
}
