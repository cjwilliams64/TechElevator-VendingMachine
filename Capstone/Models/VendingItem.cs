namespace Capstone
{
    public abstract class VendingItem
    {
        

        public string ProductName { get; set; }

        // make these private
        public decimal Price { get; set; }

        public int AddedItems { get; set; }

        public int ItemsRemaining { get; set; }
        public string MessageWhenDelivered { get; set; }
        public string MessageWhenSoldOut { get; set; }


        public VendingItem(string productName, decimal price, int itemsRemaining, string messageWhenDelivered)
        {
            
            this.ProductName = productName;
            this.Price = price;
            this.ItemsRemaining = itemsRemaining;
            this.MessageWhenDelivered = messageWhenDelivered;
            this.MessageWhenSoldOut = $"Sorry! We are sold out of {this.ProductName}. Please select another item.";
        }

        public bool RemoveItem()
        {
            if(this.ItemsRemaining > 0)
            {
                this.ItemsRemaining--;
                return true;
            }
            return false;
        }
        public bool AddItemToSalesReport()
        {
            if (this.ItemsRemaining < 5)
            {
                this.AddedItems++;
                return true;
            }
            return false;
        }
    }
}