using Capstone.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Capstone
{
    public class VendingItemManager
    {

        public List<VendingItem> VendingItemList { get; set; }



        public Dictionary<string, VendingItem> GetVendingItems()
        { //make it take a filepath
            Dictionary<string, VendingItem> VendingItemList = new Dictionary<string, VendingItem>();

            if (File.Exists(@"C:\Users\Student\git\c-module-1-capstone-team-8\19_Capstone\vendingmachine.csv"))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(@"C:\Users\Student\git\c-module-1-capstone-team-8\19_Capstone\vendingmachine.csv"))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();

                            string[] productDetails = line.Split("|");

                            string slotLocation = productDetails[0];
                            string productName = productDetails[1];
                            string price = productDetails[2];
                            string type = productDetails[3];

                            if (!decimal.TryParse(productDetails[2], out decimal productPrice))
                            {
                                productPrice = 0M;
                            }

                            int itemsRemaining = 5;

                            VendingItem item;

                            switch (productDetails[3])
                            {
                                case "Chip":
                                    item = new Chip(productName, productPrice, itemsRemaining);
                                    break;
                                case "Drink":
                                    item = new Beverage(productName, productPrice, itemsRemaining);
                                    break;
                                case "Gum":
                                    item = new Gum(productName, productPrice, itemsRemaining);
                                    break;
                                case "Candy":
                                    item = new Candy(productName, productPrice, itemsRemaining);
                                    break;
                                default: throw new ArgumentOutOfRangeException();
                            }

                            VendingItemList.Add(productDetails[0], item);
                        }
                    }

                }
                catch
                {

                }
            }
            else
            {
                Console.WriteLine("The input file is missing. Is this vending machine even real? Are we even real?");
            }
            return VendingItemList;
        }
    }
}
