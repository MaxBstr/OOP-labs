using System;
using System.Collections.Generic;

namespace Shop
{
    public class Item
    {
        private string _itemName;
        private int _itemAmount, _unitPrice;
        private int _itemID = 0;

        public int ItemID
        {
            get => _itemID; 
            set => _itemID = value;
        }
        public string ItemName => _itemName;

        public int UnitPrice
        {
            get => _unitPrice;
            set => _unitPrice = value;
        }

        public int ItemAmount
        {
            get => _itemAmount;
            set => _itemAmount = value;
        }

        public Item(string itemName, int itemAmount, int unitPrice)
        {
            this._itemName = itemName;
            this._itemAmount = itemAmount;
            this._unitPrice = unitPrice;
        }
    }

    public class Shop
    {
        private int _shopID;
        private static int _shopsCount = 0;
        private string _shopName, _shopAdress;
        public string ShopName => _shopName; 
        public string ShopAdress => _shopAdress;
        public int ShopID => _shopID;
        
        private List<Item> ShopItems = new List<Item>();
        public List<Item> ShopItemsGet => ShopItems;
        public Shop(string shopName, string shopAddress)
        {
            if (shopName == "" || shopAddress == "")
            {
                Console.WriteLine("Error! Shop must have name and address.\nShop was not created!");
                return;
            }
            _shopsCount++;
            _shopID = _shopsCount;
            this._shopName = shopName ?? throw new ArgumentNullException(nameof(shopName));
            this._shopAdress = shopAddress ?? throw new ArgumentNullException(nameof(shopAddress));
        }

        public void AddProduct(Item newItem)
        {
            var existItem = ShopItems.Find(x => x.ItemName == newItem.ItemName && x.UnitPrice == newItem.UnitPrice);
            if (existItem != null) existItem.ItemAmount += newItem.ItemAmount;
            else
            {
                ShopItems.Add(newItem);
                newItem.ItemID = ShopItems.Count;
            }
        }

        public void AddProducts(IEnumerable<Item> newItems)
        {
            foreach (var newItem in newItems)
            {
                var existItem = ShopItems.Find(x => x.ItemName == newItem.ItemName && x.UnitPrice == newItem.UnitPrice);
                if (existItem != null) existItem.ItemAmount += newItem.ItemAmount;
                else
                {
                    ShopItems.Add(newItem);
                    newItem.ItemID = ShopItems.Count;
                }
            }
        }

        public void ChangePrice(Item newItem, int unitPrice)
        {
            newItem.UnitPrice = unitPrice;
        }

        public void PrintCart()
        {
            foreach (var item in ShopItems)
            {
                Console.WriteLine($"ID: {item.ItemID}\nItem: {item.ItemName}\nAmount: {item.ItemAmount}\nUnitPrice: {item.UnitPrice}");
                Console.WriteLine("========================");
            }
        }
    }
}