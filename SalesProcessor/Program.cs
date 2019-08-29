using SalesProcessor.Core;
using System;
using Topshelf;

namespace SalesProcessor
{
    public class Program
    {
        private const string ServiceDescription = "Sales Processor Service";
        private const string ServiceDisplayName = "Sales Processor Service";
        private const string ServiceNameInSystem = "SalesProcessorService";

        static void Main()
        {
            var runnerExitCode = HostFactory.Run(x =>
            {
                x.Service<Listener>(s =>
                {
                    s.ConstructUsing(name => new Listener());
                    s.WhenStarted(listener => listener.Start());
                    s.WhenStopped(listener => listener.Stop());
                });

                x.RunAsLocalSystem();
                
                x.SetDescription(ServiceDescription);
                x.SetDisplayName(ServiceDisplayName);
                x.SetServiceName(ServiceNameInSystem);
            });

            var exitCode = (int)Convert.ChangeType(runnerExitCode, runnerExitCode.GetTypeCode());

            Environment.ExitCode = exitCode;
        }
    }
}
