using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop
{
    public static class ShopManager
    {
        private static readonly List<Shop> Shops = new List<Shop>();
        
        public static void AddShop(Shop newShop)
        {
            var existShop = Shops.Find(x => x.ShopName == newShop.ShopName && x.ShopAddress == newShop.ShopAddress);
            if (existShop == null)
                Shops.Add(newShop);
        }

        public static Shop FindShopWithMinPrice(string itemName)
        {
            var minPrice = int.MaxValue;
            var isFind = false;
            var shopId = 0;

            foreach (var shop in Shops)
            {
                foreach (var item in shop.ShopItemsGet.FindAll(it => itemName == it.ItemName).Where(item => item.UnitPrice < minPrice))
                {
                    minPrice = item.UnitPrice;
                    shopId = shop.ShopId;
                    isFind = true;
                }
            }

            if (isFind) return Shops.Find(sh => sh.ShopId == shopId);
            Console.WriteLine($"Item {itemName} was not found");
            return null;
        }

        public static void AddProductToShop(int shopId, Item newItem)
        {
            var shop = Shops.Find(sh => sh.ShopId == shopId);
            if (shop != null)
                shop.AddProduct(newItem);
            else
                Console.WriteLine($"Shop with ID: {shopId} was not found!");
        }

        public static void AddProductsToShop(int shopId, IEnumerable<Item> newItems)
        {
            var shop = Shops.Find(sh => sh.ShopId == shopId);
            if (shop != null)
                shop.AddProducts(newItems);
            else
                Console.WriteLine($"Shop with ID: {shopId} was not found!");
        }

        public static void ChangeItemPrice(int shopID, int itemID, int unitPrice)
        {
            var shop = Shops.Find(sh => sh.ShopId == shopID);
            if (shop == null)
            {
                Console.WriteLine($"Error! Can`t find the shop with {shopID} ID");
                return;
            }
            
            var item = shop.ShopItemsGet.Find(it => it.ItemId == itemID);
            if (item == null)
            {
                Console.WriteLine($"Error! Can`t find item with {itemID} ID in the shop with {shopID} ID");
                return;
            }
            
            shop.ChangePrice(item, unitPrice);
        }

        public static bool MakeOrder(int shopId, ref int orderSum, Dictionary<string, int> order)
        {
            var shop = Shops.Find(sh => sh.ShopId == shopId);
            if (shop == null)
            {
                Console.WriteLine($"Error! Can`t find shop with {shopId} ID");
                return false;
            }
            
            var isError = false;
            
            foreach (var (itemName, amount) in order)
            {
                var items = shop.ShopItemsGet.FindAll(it => it.ItemName == itemName);
                
                if (items.Count == 0)
                {
                    Console.WriteLine($"Item {itemName} was not found!");
                    isError = true;
                    break;
                }
                
                if (items.Count == 1 && items[0].ItemAmount < amount)
                {
                    Console.WriteLine($"Can not make an order. Not enough items of {itemName}.");
                    isError = true;
                    break;
                }

                var isFind = false;
                foreach (var item in items.Where(item => item.ItemAmount >= amount))
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
        
        public static void PrintWhatCanIBuy(int shopId, int money)
        {
            var items = WhatCanBuy(shopId, money);
            if (items == null)
            {
                Console.WriteLine($"Sorry, you can not buy anything, because you have not enough money: {money}");
                return;
            }

            var shop = Shops.Find(sh => sh.ShopId == shopId);
            if (shop == null)
            {
                Console.WriteLine($"Shop with {shopId} ID does not exist!");
                return;
            }
            
            Console.WriteLine($"In {shop.ShopName} which is located in {shop.ShopAddress}, having {money} rubles you can buy:");
            foreach (var item in items)
                Console.WriteLine($"{item.ItemAmount} {item.ItemName} for {item.UnitPrice} rub per unit");
        }

        private static List<Item> WhatCanBuy(int shopId, int money)
        {
            var shop = Shops.Find(sh => sh.ShopId == shopId);
            if (shop == null)
            {
                Console.WriteLine($"Shop with {shopId} ID does not exists!");
                return null;
            }
            var isAdded = false;
            var items = new List<Item>();
            
            foreach (var item in shop.ShopItemsGet)
            {
                var count = money / item.UnitPrice;

                if (0 < count && count <= item.ItemAmount)
                {
                    items.Add(new Item(item.ItemName, count, item.UnitPrice));
                    isAdded = true;
                }
            }

            return !isAdded ? null : items;
        }

        public static Shop FindShopWithMinPrice(Dictionary<string, int> order)
        {
            var shopId = 0;
            var minPrice = int.MaxValue;
            var isFind = false;
            
            foreach (var shop in Shops)
            {
                var curPrice = 0;
                var isError = false;
                
                foreach (var (itemName, amount) in order)
                {
                    //одинаковые имена, но разные id и цены за единицу
                    var sameItems = shop.ShopItemsGet.FindAll(it => it.ItemName == itemName);
                    //если не нашли хоть 1 товар
                    if (sameItems.Count == 0)
                    {
                        isError = true;
                        break;
                    }

                    var isFindItem = false;
                    var minUnitPrice = int.MaxValue;
                    //перебираем все товары с одинаковыми именами, ищем мин стоимость при выполнении условия на кол-во товаров
                    foreach (var item in sameItems.Where(item => item.UnitPrice < minUnitPrice && item.ItemAmount > amount))
                    {
                        minUnitPrice = item.UnitPrice;
                        isFindItem = true;
                    }

                    curPrice += amount * minUnitPrice;
                    
                    if(isFindItem) continue;
                    
                    isError = true;
                    break;
                }

                if (isError || curPrice >= minPrice) continue;

                minPrice = curPrice;
                shopId = shop.ShopId;
                isFind = true;
            }

            if (isFind) return Shops.Find(sh => sh.ShopId == shopId);
            
            Console.Write("Sorry, but you can`t buy all items that you mentioned in order in all shops");
            return null;
        }
    }
}
