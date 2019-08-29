using SalesProcessor.Interfaces;
using SalesProcessor.Models;

namespace SalesProcessor.Core.Strategies
{
    public class SalesmanStrategy : IBaseStrategy
    {
        public object Execute(string[] fileLine)
        {
            var salary = double.TryParse(fileLine[3], out var valuParsed) ? valuParsed : 0;

            var salesman = new Salesman
            {
                Id = fileLine[0],
                Cpf = fileLine[1],
                Name = fileLine[2],
                Salary = salary
            };

            return salesman;
        }
    }
}
