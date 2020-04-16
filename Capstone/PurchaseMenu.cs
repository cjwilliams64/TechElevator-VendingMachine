using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class PurchaseMenu
    {
        private VendingMachine vm;
        public PurchaseMenu(VendingMachine vm)
        {
            this.vm = vm;
        }
        public void Run()
        {
            while (true)
            {
                //Purchase Process Flow

                Console.WriteLine($"{"Current Amount Tendered: ",40} {this.vm.MoneyProvided.ToString("C")}");

                Console.Write(@"
                (1) Feed Me Money, Please
                (2) Select Item
                (3) Finish Transaction 

                Enter your choice here: ");

                string input = Console.ReadLine().Trim();

                if (input == "1")
                {
                    Console.Clear();
                    Console.WriteLine("How much would you like to feed me?");
                    Console.WriteLine("Choose from one of the options below: ");

                    while (true)
                    {
                        Console.WriteLine("$1, $2, $5, $10 ");
                        string amountSelected = Console.ReadLine();

                        if (amountSelected != "1" && amountSelected != "2" && amountSelected != "5" && amountSelected != "10")
                        {
                            Console.WriteLine("Please insert a valid amount");
                        }
                        else if (this.vm.money.AddMoney(amountSelected))
                        {
                            Console.Clear();
                            Console.WriteLine($"Current Amount Tendered: {this.vm.MoneyProvided.ToString("C")}. Thank you!");
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    }
                }
                else if (input == "2")
                {
                    Console.Clear();
                    while (true)
                    {
                        this.vm.DisplayItems();
                        Console.WriteLine();
                        Console.WriteLine("Please make your selection. (A1, for example)");
                        string selection = Console.ReadLine().Trim().ToUpper();

                        if (this.vm.ItemExists(selection) && this.vm.GetItem(selection))

                        {
                            Console.WriteLine($"Enjoy your {this.vm.VendingMachineItems[selection].ProductName}! You paid {this.vm.VendingMachineItems[selection].Price.ToString("C")} and have {this.vm.MoneyProvided.ToString("C")} remaining. {this.vm.VendingMachineItems[selection].MessageWhenDelivered}");
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                        else if (!this.vm.ItemExists(selection))
                        {
                            Console.Clear();
                            Console.WriteLine(this.vm.InvalidItem);
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else if (this.vm.ItemExists(selection) && this.vm.MoneyProvided >= this.vm.VendingMachineItems[selection].Price)
                        {
                            Console.WriteLine(this.vm.VendingMachineItems[selection].MessageWhenSoldOut);
                        }
                        else if (this.vm.MoneyProvided < this.vm.VendingMachineItems[selection].Price)
                        {
                            Console.WriteLine(this.vm.NotEnoughMoney);
                            Console.WriteLine($"Feed me {vm.VendingMachineItems[selection].Price - vm.MoneyProvided:C} to complete the transaction.");
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    }
                }
                else if (input == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Finish Transaction");
                    Console.WriteLine(this.vm.money.GiveChange());
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Please try again.");
                }
                Console.ReadLine();
            }
        }
    }
}
