using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    class Beverage : VendingItem
    {
        public const string Message = "Glug Glug, Yum!";

        public Beverage(string productName, decimal price, int itemsRemaining) : base(productName, price, itemsRemaining, Message)
        {

        }
    }
}
