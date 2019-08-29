using SalesProcessor.Core.Strategies;
using SalesProcessor.Helpers;
using SalesProcessor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SalesProcessor.Core.Handlers
{
    public class DataHandler
    {
        private readonly FileHandler _fileHandler;
        private readonly string _directoryOut;
        private readonly string _directoryProcessed;
        private readonly ContextStrategy _contextStrategy;
        private readonly string _termination;

        private Output Output { get; set; }
        private List<Salesman> SalesmanList { get; set; }
        private List<Customer> CustomerList { get; set; }
        private List<Sale> SaleList { get; set; }

        public DataHandler()
        {
            _fileHandler = new FileHandler();

            _directoryOut = $@"{_fileHandler.GetDataDirectory()}\out";

            _directoryProcessed = $@"{_fileHandler.GetDataDirectory()}\processed";

            _fileHandler.CreateDirectory(_directoryOut);
            _fileHandler.CreateDirectory(_directoryProcessed);

            _contextStrategy = new ContextStrategy();

            _termination = $"-Processed-in-{DateTimeHelper.DateTimeBrazil("yyyy-MM-dd-HHmmss")}.txt";

            Output = new Output();
            SalesmanList = new List<Salesman>();
            CustomerList = new List<Customer>();
            SaleList = new List<Sale>();
        }

        public void ProcessFile(FileInfo[] files)
        {
            var line = string.Empty;

            foreach (var file in files)
            {
                using (StreamReader stream = file.OpenText())
                {
                    while ((line = stream.ReadLine()) != null)
                    {
                        var identifier = ExtractIdentifier(line);

                        ApplyStrategy(identifier, line);
                    }
                }

                HandleOutput();

                WriteFileOutput(file.Name);

                MoveFileToProcessedDirectory(file);
            }
        }

        private void MoveFileToProcessedDirectory(FileInfo file)
        {
            var path = $@"{_directoryProcessed}\{file.Name.Replace(".txt", _termination)}";

            File.Move(file.FullName, path);

            file.Delete();
        }

        private void WriteFileOutput(string fileName)
        {
            var path = $@"{_directoryOut}\{fileName.Replace(".txt",_termination)}";

            _fileHandler.CreateFile(path);

            using(var writer = new StreamWriter(path))
            {
                writer.WriteLine($"DADOS PROCESSADOS DO ARQUIVO DE ENTRADA");
                writer.WriteLine($"Número de clientes: {Output.CustomerQuantity}");
                writer.WriteLine($"Número de vendedores: {Output.SalesmanQuantity}");
                writer.WriteLine($"ID da venda mais cara: {Output.MostExpensiveSaleId}");
                writer.WriteLine($"Pior vendedor: {Output.WorseSalesman}");
            }
        }

        private string ExtractIdentifier(string data)
        {
            var dataSplitted = data.Split('ç');

            var identifier = dataSplitted[0];

            return identifier;
        }

        private void ApplyStrategy(string identifier, string line)
        {
            switch (identifier)
            {
                case "001":
                    _contextStrategy.SetStrategy(new SalesmanStrategy());
                    var salesman = (Salesman)_contextStrategy.ApplyStrategy(line);
                    SalesmanList.Add(salesman);
                    Output.SalesmanQuantityUpdate(1);
                    break;

                case "002":
                    Output.CustomerQuantityUpdate(1);
                    break;

                case "003":
                    _contextStrategy.SetStrategy(new SaleStrategy());
                    var sale = (Sale)_contextStrategy.ApplyStrategy(line);
                    SaleList.Add(sale);
                    break;

                default:
                    break;
            }
        }

        private void HandleOutput()
        {
            foreach (var salesman in SalesmanList)
            {
                var saleAmount = SaleList.Where(x => x.Salesman.Equals(salesman.Name)).ToList().Sum(x => x.SaleItems.Sum(y => (y.Price * y.Quantity)));

                Output.WorseSalesmanUpdate(salesman.Name, saleAmount);
            }

            foreach (var sale in SaleList)
            {
                var maxSaleValue = sale.SaleItems.Sum(x => (x.Price * x.Quantity));

                Output.MostExpensiveSaleIdUpdate(sale.SaleId, maxSaleValue);
            }            
        }
    }
}
