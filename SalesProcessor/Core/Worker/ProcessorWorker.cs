using SalesProcessor.Core.Handlers;
using SalesProcessor.Util;
using System;

namespace SalesProcessor.Core.Worker
{
    public class ProcessorWorker
    {
        private readonly DataHandler _dataHandler;
        private readonly FileHandler _fileHandler;
        private readonly string _directoryDataIn;

        public ProcessorWorker()
        {
            _dataHandler = new DataHandler();
            _fileHandler = new FileHandler();

            _directoryDataIn = $@"{_fileHandler.GetDataDirectory()}\in";

            _fileHandler.CreateDirectory(_directoryDataIn);

            try
            {
                var files = _fileHandler.GetFiles(_directoryDataIn);

                _dataHandler.ProcessFile(files);
            }
            catch (Exception ex)
            {
                var log = new Log();

                log.Error(ex);
            }
        }
    }
}
