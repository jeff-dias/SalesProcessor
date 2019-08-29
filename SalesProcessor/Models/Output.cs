namespace SalesProcessor.Models
{
    public class Output
    {
        public Output()
        {
            CustomerQuantity = 0;
            SalesmanQuantity = 0;
            MostExpensiveSaleId = 0;
            WorseSalesman = string.Empty;
        }

        public void CustomerQuantityUpdate(int newData = 0)
        {
            CustomerQuantity += newData;
        }

        public void SalesmanQuantityUpdate(int newData = 0)
        {
            SalesmanQuantity += newData;
        }

        public void MostExpensiveSaleIdUpdate(int newData, double saleValue)
        {
            if(newData != 0 && saleValue > MostExpensiveSaleIdValue)
            {
                MostExpensiveSaleId = newData;
                MostExpensiveSaleIdValue = saleValue;
            }
        }

        public void WorseSalesmanUpdate(string newData, double salesAmount = 0)
        {
            if(!string.IsNullOrWhiteSpace(newData) && (WorseSalesmanTotalSales == 0 || salesAmount <= WorseSalesmanTotalSales))
            {
                WorseSalesman = newData;
                WorseSalesmanTotalSales = salesAmount;
            }
        }

        public int CustomerQuantity { get; set; }
        public int SalesmanQuantity { get; set; }
        public int MostExpensiveSaleId { get; set; }
        public string WorseSalesman { get; set; }
        public double WorseSalesmanTotalSales { get; set; }
        public double MostExpensiveSaleIdValue { get; set; }
    }
}
