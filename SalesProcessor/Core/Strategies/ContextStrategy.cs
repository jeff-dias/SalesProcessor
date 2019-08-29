using SalesProcessor.Interfaces;

namespace SalesProcessor.Core.Strategies
{
    public class ContextStrategy
    {
        private IBaseStrategy _strategy;
        private const char Separator = 'ç';

        public ContextStrategy()
        {
        }

        public ContextStrategy(IBaseStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IBaseStrategy strategy)
        {
            _strategy = strategy;
        }

        public object ApplyStrategy(string fileLine)
        {
            var fileLineSplitted = fileLine.Split(Separator);

            return _strategy.Execute(fileLineSplitted);
        }

    }
}
