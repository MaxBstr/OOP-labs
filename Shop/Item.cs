namespace Shop
{
    public class Item
    {
        public int ItemId { get; set; }

        public string ItemName { get; }

        public int UnitPrice { get; set; }

        public int ItemAmount { get; set; }

        public Item(string itemName, int itemAmount, int unitPrice)
        {
            this.ItemName = itemName;
            this.ItemAmount = itemAmount;
            this.UnitPrice = unitPrice;
        }
    }
}