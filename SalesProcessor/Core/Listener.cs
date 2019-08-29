using SalesProcessor.Core.Worker;
using System.Timers;

namespace SalesProcessor.Core
{
    public class Listener
    {
        private readonly Timer _timer;
        private const int ExecutionInterval = 10000;

        public Listener()
        {
            _timer = new Timer(ExecutionInterval) { AutoReset = true };

            _timer.Elapsed += (sender, eventArgs) => new ProcessorWorker();
        }

        public void Start() { _timer.Start(); }

        public void Stop()
        {
            _timer.Stop();
        }

    }
}
