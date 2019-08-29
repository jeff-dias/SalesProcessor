using System.Collections.Generic;

namespace SalesProcessor.Models
{
    public class Sale : Identifier
    {
        public Sale()
        {
            SaleItems = new List<SaleItem>();
        }

        public int SaleId { get; set; }

        public string Salesman { get; set; }

        public List<SaleItem> SaleItems { get; set; }
    }
}
