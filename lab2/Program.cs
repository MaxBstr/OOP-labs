using System;
using System.Collections.Generic;

namespace Shop
{
    public static class Program
    {
        private static void Main()
        {
            ShopManager.AddShop(new Shop("Lenta", "Moscow"));
            ShopManager.AddShop(new Shop("Metro", "SPB"));
            ShopManager.AddShop(new Shop("Ashan", "Tambov"));

            ShopManager.AddProductsToShop(1, new List<Item>
            {
                new Item("Milk", 10, 40),
                new Item("Bread", 20, 25),
                new Item("Meat", 15, 120),
                new Item("Milk", 15, 40),
                new Item("Bread", 8, 35),
                
                new Item("Eggs", 13, 30),
                new Item("Chicken", 20, 50),
                new Item("Ketchup", 8, 40),
                new Item("Chips", 20, 20),
                new Item("Fruits", 10, 70)
            });
            
            ShopManager.AddProductsToShop(2, new List<Item>
            {
                new Item("Milk", 15, 50),
                new Item("Vegetables", 15, 70),
                new Item("Meat", 20, 90),
                new Item("Bread", 22, 30),
                new Item("Eggs", 20, 40),
                
                new Item("Milk", 10, 60),
                new Item("Cheese", 8, 90),
                new Item("Salmon", 5, 120),
                new Item("Bread", 40, 45),
                new Item("Chicken", 10, 50)
            });
            
            ShopManager.AddProductsToShop(3, new List<Item>
            {
                new Item("Milk", 12, 45),
                new Item("Lemonade", 10, 10),
                new Item("Meat", 15, 120),
                new Item("Bread", 24, 36),
                new Item("Milk", 16, 45),
                
                new Item("Nuts", 14, 80),
                new Item("Mayo", 13, 47),
                new Item("Salmon", 8, 100),
                new Item("Cheese", 17, 90),
                new Item("Bread", 13, 40)
            });
            
            
            var totalPrice = 0;
            var canMakeOrder = ShopManager.MakeOrder(1, ref totalPrice, new Dictionary<string, int>
            {
                {"Milk", 13}, {"Bread", 10}, {"Eggs", 10}
            });
            
            if(canMakeOrder)
                Console.WriteLine(totalPrice);

            
            try
            {
                var shop = ShopManager.FindShopWithMinPrice("Bread");
                Console.WriteLine(shop.ShopName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            
            ShopManager.PrintWhatCanIBuy(3, 400);

            try
            {
                var sh = ShopManager.FindShopWithMinPrice(new Dictionary<string, int>
                {
                    {"Bread", 10}, {"Milk", 15}
                });
                Console.WriteLine(sh.ShopId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
