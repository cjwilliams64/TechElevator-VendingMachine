using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class MainMenu
    {
        public void Run() 
        {
            VendingMachine vm = new VendingMachine();

            
            while (true)
            {
                
                    Console.Write(@"
                Welcome, Hungry One. Please select an option.

                (1) Display Vending Item Selection
                (2) Purchase Item
                (3) Exit Vendomatic 800
                
                Enter your option here: ");
                

                string input = Console.ReadLine().Trim();
                Console.Clear();

                if (input == "1")
                {
                   
                    Console.Clear();
                    Console.WriteLine("Display vending machine items");
                    Console.WriteLine();
                    vm.DisplayItems();
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                }

                else if (input == "2")
                {
                    // Simplifying to call a new purchase menu using this info so it sends the user to that menu from here
                    Console.Clear();
                    PurchaseMenu pm = new PurchaseMenu(vm);
                    pm.Run();
                }

                else if (input == "3")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                else if (input == "4")
                {
                    vm.PrintSalesReport();
                }

                else
                {
                    Console.WriteLine($"{input} is invalid. Please enter 1, 2, or 3. Thank you!");
                }
            }


        }
    }
}
