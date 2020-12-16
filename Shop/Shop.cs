using System;
using System.Collections.Generic;

namespace Shop
{
    public class Shop
    {
        private static int _shopsCount;
        public string ShopName { get; }

        public string ShopAddress { get; }

        public int ShopId { get; }

        public List<Item> ShopItemsGet { get; } = new List<Item>();

        public Shop(string shopName, string shopAddress)
        {
            if (shopName == "" || shopAddress == "")
            {
                Console.WriteLine("Error! Shop must have name and address.\nShop was not created!");
                return;
            }
            _shopsCount++;
            ShopId = _shopsCount;
            this.ShopName = shopName;
            this.ShopAddress = shopAddress;
        }

        public void AddProduct(Item newItem)
        {
            var existItem = ShopItemsGet.Find(x => x.ItemName == newItem.ItemName && x.UnitPrice == newItem.UnitPrice);
            if (existItem != null) existItem.ItemAmount += newItem.ItemAmount;
            else
            {
                ShopItemsGet.Add(newItem);
                newItem.ItemId = ShopItemsGet.Count;
            }
        }

        public void AddProducts(IEnumerable<Item> newItems)
        {
            foreach (var newItem in newItems)
            {
                var existItem = ShopItemsGet.Find(x => x.ItemName == newItem.ItemName && x.UnitPrice == newItem.UnitPrice);
                if (existItem != null) existItem.ItemAmount += newItem.ItemAmount;
                else
                {
                    ShopItemsGet.Add(newItem);
                    newItem.ItemId = ShopItemsGet.Count;
                }
            }
        }

        public void ChangePrice(Item newItem, int unitPrice)
        {
            newItem.UnitPrice = unitPrice;
        }
        
    }
}
