using SalesProcessor.Interfaces;
using SalesProcessor.Models;

namespace SalesProcessor.Core.Strategies
{
    public class SaleStrategy : IBaseStrategy
    {
        public object Execute(string[] fileLine)
        {
            var saleId = int.TryParse(fileLine[1], out var outSaleId) ? outSaleId : 0;

            var sale = new Sale
            {
                Id = fileLine[0],
                SaleId = saleId,
                Salesman = fileLine[3]
            };

            var saleItemsText = fileLine[2];

            var saleItems = saleItemsText.Substring(1, saleItemsText.Length - 2);

            var saleItemsSplitted = saleItems.Split(',');

            foreach (var item in saleItemsSplitted)
            {
                var itemSplitted = item.Split('-');

                var itemId = int.TryParse(itemSplitted[0], out var outItemId) ? outItemId : 0;
                var itemPrice = double.TryParse(itemSplitted[2], out var outItemPrice) ? outItemPrice : 0;
                var itemQuantity = int.TryParse(itemSplitted[1], out var outItemQuantity) ? outItemQuantity : 0;

                var saleItem = new SaleItem
                {
                    Id = itemId,
                    Price = itemPrice,
                    Quantity = itemQuantity
                };

                sale.SaleItems.Add(saleItem);
            }

            return sale;
        }
    }
}
