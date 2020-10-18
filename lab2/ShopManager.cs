using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop
{
    public class ShopManager
    {
        private List<Shop> Shops = new List<Shop>();
        
        public ShopManager() {}
        
        public ShopManager(Shop newShop) { Shops.Add(newShop); }

        public ShopManager(IEnumerable<Shop> newShops)
        {
            foreach (var shop in newShops)
            {
                var existShop = Shops.Find(x => x.ShopName == shop.ShopName && x.ShopAdress == shop.ShopAdress);
                if (existShop == null)
                    Shops.Add(shop);
            }
        }

        public void AddShop(Shop newShop)
        {
            var existShop = Shops.Find(x => x.ShopName == newShop.ShopName && x.ShopAdress == newShop.ShopAdress);
            if (existShop == null)
                Shops.Add(newShop);
        }

        public Shop FindShopWithMinPrice(string itemName)
        {
            var minPrice = int.MaxValue;
            var isFind = false;
            var shopID = 0;

            foreach (var shop in Shops)
            {
                foreach (var item in shop.ShopItemsGet.FindAll(it => itemName == it.ItemName).Where(item => item.UnitPrice < minPrice))
                {
                    minPrice = item.UnitPrice;
                    shopID = shop.ShopID;
                    isFind = true;
                }
            }

            if (isFind) return Shops.Find(sh => sh.ShopID == shopID);
            Console.WriteLine($"Item {itemName} was not found");
            return null;
        }

        public void AddProductToShop(int shopID, Item newItem)
        {
            var shop = Shops.Find(sh => sh.ShopID == shopID);
            if (shop != null)
                shop.AddProduct(newItem);
            else
                Console.WriteLine($"Shop with ID: {shopID} was not found!");
        }

        public void AddProductsToShop(int shopID, IEnumerable<Item> newItems)
        {
            var shop = Shops.Find(sh => sh.ShopID == shopID);
            if (shop != null)
                shop.AddProducts(newItems);
            else
                Console.WriteLine($"Shop with ID: {shopID} was not found!");
        }

        public void ChangeItemPrice(int shopID, int itemID, int unitPrice)
        {
            var shop = Shops.Find(sh => sh.ShopID == shopID);
            if (shop == null)
            {
                Console.WriteLine($"Error! Can`t find the shop with {shopID} ID");
                return;
            }
            
            var item = shop.ShopItemsGet.Find(it => it.ItemID == itemID);
            if (item == null)
            {
                Console.WriteLine($"Error! Can`t find item with {itemID} ID in the shop with {shopID} ID");
                return;
            }
            
            shop.ChangePrice(item, unitPrice);
        }

        public bool MakeOrder(int shopID, ref int orderSum, Dictionary<string, int> Order)
        {
            var shop = Shops.Find(sh => sh.ShopID == shopID);
            if (shop == null)
            {
                Console.WriteLine($"Error! Can`t find shop with {shopID} ID");
                return false;
            }
            
            var isError = false;
            
            foreach (var (itemName, amount) in Order)
            {
                var Items = shop.ShopItemsGet.FindAll(it => it.ItemName == itemName);
                
                if (Items.Count == 0)
                {
                    Console.WriteLine($"Item {itemName} was not found!");
                    isError = true;
                    break;
                }
                
                if (Items.Count == 1 && Items[0].ItemAmount < amount)
                {
                    Console.WriteLine($"Can not make an order. Not enough items of {itemName}.");
                    isError = true;
                    break;
                }

                var isFind = false;
                foreach (var item in Items.Where(item => item.ItemAmount > amount))
                {
                    isFind = true;
                    orderSum += amount * item.UnitPrice;
                }

                if (isFind) continue;
                Console.WriteLine($"Can not make an order. Not enough items of {itemName}.");
                isError = true;
                break;
            }

            if(!isError)
                return true;
            
            orderSum = 0;
            return false;
        }
        
        public void PrintWhatCanIBuy(int shopID, int money)
        {
            var Items = WhatCanBuy(shopID, money);
            if (Items == null)
            {
                Console.WriteLine($"Sorry, you can not buy anything, because you have not enough money: {money}");
                return;
            }

            var shop = Shops.Find(sh => sh.ShopID == shopID);
            Console.WriteLine($"In {shop.ShopName} which is located in {shop.ShopAdress}, having {money} rubles you can buy:");
            foreach (var item in Items)
                Console.WriteLine($"{item.ItemAmount} {item.ItemName} for {item.UnitPrice} rub per unit");
        }

        private List<Item> WhatCanBuy(int shopID, int money)
        {
            var shop = Shops.Find(sh => sh.ShopID == shopID);
            var isAdded = false;
            var Items = new List<Item>();
            
            foreach (var item in shop.ShopItemsGet)
            {
                //если цена за единицу товара больше имеющихся средств, пропускаем товар
                if (item.UnitPrice > money) continue;
                
                var curPrice = item.UnitPrice;
                var i = 0;
                do
                {
                    i++;
                    curPrice = item.UnitPrice * i;
                } while (curPrice < money && i < item.ItemAmount);
                
                Items.Add(new Item(item.ItemName, i, item.UnitPrice));
                isAdded = true;
            }

            return !isAdded ? null : Items;
        }

        public Shop FindShopWithMinPrice(Dictionary<string, int> Order)
        {
            int shopID = 0;
            var minPrice = int.MaxValue;
            var isFind = false;
            
            foreach (var shop in Shops)
            {
                var curPrice = 0;
                var isError = false;
                foreach (var (itemName, amount) in Order)
                {
                    var item = shop.ShopItemsGet.Find(it => it.ItemName == itemName);
                    if (item == null || item.ItemAmount < amount)
                    {
                        isError = true;
                        break;
                    }
                    curPrice += amount * item.UnitPrice;
                }

                if (isError || curPrice >= minPrice) continue;

                minPrice = curPrice;
                shopID = shop.ShopID;
                isFind = true;
            }

            if (isFind) return Shops.Find(sh => sh.ShopID == shopID);
            Console.Write("Sorry, but you can`t buy all items that you mentioned in order in all shops");
            return null;
        }
        
        public void PrintShops()
        {
            foreach (var shop in Shops)
            {
                Console.WriteLine($"Shop: {shop.ShopName}\nLocation: {shop.ShopAdress}");
                Console.WriteLine("=================");
            }
        }
        
    }
}