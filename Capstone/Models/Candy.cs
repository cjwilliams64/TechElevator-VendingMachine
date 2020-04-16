using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Candy : VendingItem
    {
        public const string Message = "Munch Munch, Yum!";

        public Candy(string productName, decimal price, int itemsRemaining) : base(productName, price, itemsRemaining, Message)
        {

        }
    }
}
