using System;

namespace Capstone
{
    public class Money
    {
        private FileLog fileLog = new FileLog();
        public decimal MoneyProvided { get; private set; }

        public decimal GrossPerItem { get; private set; } // Money Added to sales report total

        public Money(FileLog fileLog)
        {
            this.MoneyProvided = 0;
            this.fileLog = fileLog;
        }

        

        public bool AddMoney(string amount)
        {
            if (!decimal.TryParse(amount, out decimal amountInserted))
            {
                amountInserted = 0M;
                return false;
            }

            string message = $"FEED MONEY: ";

            // Log current amount of money in the machine
            decimal before = this.MoneyProvided;

            // Add the money
            this.MoneyProvided += amountInserted;

            // Log current money in machine after adding
            decimal after = this.MoneyProvided;

            // Log message, before, after
            this.fileLog.Log(message, before, after);

            return true;
        }

        public bool RemoveMoney(decimal amountToRemove)
        {
            if(this.MoneyProvided < amountToRemove)
            {
                return false;
            }
            this.MoneyProvided -= amountToRemove;
            return true;
        }

        public bool TrackSales(decimal salesAdded)
        {
            this.GrossPerItem += salesAdded;
            return true;
        }
        public string GiveChange()
            // turn this into an object so you can test the values...not the message itself. separate numbers from message
        {
            string result = string.Empty;
            int quarters = 0;
            int dimes = 0;
            int nickels = 0;

            string message = $"GIVE CHANGE: ";

            decimal before = this.MoneyProvided;

            if(this.MoneyProvided > 0)
            {
                while(this.MoneyProvided > 0)
                {
                    if(this.MoneyProvided >= 0.25M)
                    {
                        quarters++;
                        this.RemoveMoney(0.25M);
                    }
                    else if(this.MoneyProvided >= 0.10M)
                    {
                        dimes++;
                        this.RemoveMoney(0.10M);
                    }
                    else if(this.MoneyProvided >= 0.05M)
                    {
                        nickels++;
                        this.RemoveMoney(0.05M);
                    }
                }
                result = GetMessage(quarters, dimes, nickels);

                // Log current money in the machine
                decimal after = this.MoneyProvided;

                // Log message, before, after
                this.fileLog.Log(message, before, after);
            }
            else
            {
                result = "No money to return";
            }
            return result;
        }

        private string GetMessage(int quarters, int dimes, int nickels)
        {
            string quarterString = string.Empty;
            string dimeString = string.Empty;
            string nickelString = string.Empty;

            if(quarters > 0)
            {
                if (quarters > 1)
                {
                    quarterString = $"{quarters} quarters";
                }
                else
                {
                    quarterString = $"{quarters} quarter";
                }
            }
            if (dimes > 0)
            {
                if (dimes > 1)
                {
                    dimeString = $"{dimes} dimes";
                }
                else
                {
                    dimeString = $"{dimes} dime";
                }
            }
            if (nickels > 0)
            {
                if (nickels > 1)
                {
                    nickelString = $"{nickels} nickels";
                }
                else
                {
                    nickelString = $"{nickels} nickel";
                }
            }

            string result = $"Change dispensed: ";

            if (quarters > 0 && dimes > 0 && nickels > 0)
            {
                result += $"{quarterString}, {dimeString} and {nickelString}";
            }
            else if(quarters > 0 && dimes > 0 || quarters > 0 && nickels > 0)
            {
                result += $"{quarterString} and {dimeString}{nickelString}";
            }
            else if (dimes > 0 && nickels > 0)
            {
                result += $"{dimeString} and {nickelString}";
            }
            else if (quarters > 0 || dimes > 0 || nickels > 0)
            {
                result += $"{quarterString}{dimeString}{nickelString}";
            }
            else
            {
                result = "No change to give.";
            }
            return result;
        }
    }
}