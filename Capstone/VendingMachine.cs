using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class VendingMachine
        // 
    {
        public Dictionary<string, VendingItem> VendingMachineItems = new Dictionary<string, VendingItem>();
        public Money money { get; }
        private FileLog fileLog = new FileLog();
        VendingItemManager ItemManager = new VendingItemManager();
        public string MessageToUser;
     
        // these should be private
        public string NotEnoughMoney = "Sorry, please insert more money into the machine to complete the transaction. ";
        public string InvalidItem = "Invalid Item Selected. Please try again. ";


        public VendingMachine()
        {
            this.VendingMachineItems = this.ItemManager.GetVendingItems();
            this.money = new Money(this.fileLog);
        }

        public decimal MoneyProvided
        {
            get
            {
                return this.money.MoneyProvided;
            }
        }

        public void DisplayItems()
        {
            Console.WriteLine($"{"Location",-5} {"Product",10} {"Price",32} {"Available",14}");
            // Foreach for Vending Items
            foreach (KeyValuePair<string, VendingItem> kvp in this.VendingMachineItems)
            {
                // If items stock is greater than 0 than show the slot location, product name, price quantity remaining
                if (kvp.Value.ItemsRemaining > 0)
                {
                    // item location
                    string itemLocation = kvp.Key;
                    // Product
                    string itemName = kvp.Value.ProductName;

                    // Price
                    string itemPrice = kvp.Value.Price.ToString("C");

                    // Number Available
                    string itemRemaining = kvp.Value.ItemsRemaining.ToString();

                    Console.WriteLine($"   {itemLocation,-8} {itemName, -18} {itemPrice,21} {itemRemaining,10}");
                }
                else
                {
                    // Else return that the item is sold out
                    Console.WriteLine($"{kvp.Key} {kvp.Value.ProductName} is sold out!");

                    // return user to the purchase menu
                }
            }


        }

        public bool ItemExists(string itemNumber)
        {
            return this.VendingMachineItems.ContainsKey(itemNumber);
        }

        
        public bool GetItem(string itemNumber)
        {
            // If the item exists and there is stock left and we have the money to buy
            // UPDATED to call remove item method to update the items remaining
            if (this.ItemExists(itemNumber) && this.VendingMachineItems[itemNumber].ItemsRemaining > 0 && this.money.MoneyProvided >= this.VendingMachineItems[itemNumber].Price && this.VendingMachineItems[itemNumber].RemoveItem() && this.VendingMachineItems[itemNumber].AddItemToSalesReport())
            {
                // Log the item selected
                string message = $"{this.VendingMachineItems[itemNumber].ProductName} {itemNumber}";

                // Log current amount of money in the machine
                decimal before = this.money.MoneyProvided;

                // Remove the money in the machine
                this.money.RemoveMoney(this.VendingMachineItems[itemNumber].Price);

                // Log the money left in the machine
                decimal after = this.money.MoneyProvided;

                // Log message, before, after
                this.fileLog.Log(message, before, after);

                // Log the item selected
                string message2 = $"{this.VendingMachineItems[itemNumber].ProductName}"; // Product Name

                //string priceOfItemsSold = $"{this.money.TrackSales(this.VendingMachineItems[itemNumber].Price)}"; // Sum of items sold

                string numberOfItemsSold = $"{this.VendingMachineItems[itemNumber].AddedItems}"; // Number of items per product

                // Remove the money in the machine
                this.money.TrackSales(this.VendingMachineItems[itemNumber].Price);

                // Log the money left in the machine
                string priceOfItemsSold = this.money.GrossPerItem.ToString("C");

                return true;
            }
            else
            {
                return false;
            }

        }

        public void PrintSalesReport()
        {
            DateTime date = DateTime.Now;
            string calendarDate = date.ToString("MM-dd-yyyy_hh-mm-ss-tt");
            string fileName =$"SalesReport_{calendarDate}.txt";

            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                decimal totalSales = 0;
                foreach (KeyValuePair<string, VendingItem> item in VendingMachineItems)
                {
                    sw.WriteLine($"{item.Value.ProductName}|{5 - item.Value.ItemsRemaining}");
                    totalSales += ((5 - item.Value.ItemsRemaining) * item.Value.Price);
                }
                sw.WriteLine();
                sw.WriteLine($"Total Sales: {totalSales:C}");
            }
        }
    }
}





