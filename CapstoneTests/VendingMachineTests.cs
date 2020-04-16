using Capstone;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [DataTestMethod]
        [DataRow("1", 1)]
        [DataRow("2", 2)]
        [DataRow("5", 5)]
        [DataRow("10", 10)]
        public void TestIfDepositingCashWorks(string input, int expected) //amount inserted
        {
            // Assign
            VendingMachine vm = new VendingMachine();

            // Act
            vm.money.AddMoney(input);
            decimal result = vm.money.MoneyProvided;

            // Assert
            Assert.AreEqual(expected, result);
        }
        [DataTestMethod]
        [DataRow("1.35", "Change dispensed: 5 quarters and 1 dime")]
        [DataRow("2", "Change dispensed: 8 quarters")]
        [DataRow("4.30", "Change dispensed: 17 quarters and 1 nickel")]
        [DataRow("2.60", "Change dispensed: 10 quarters and 1 dime")]
        [DataRow("3.65", "Change dispensed: 14 quarters, 1 dime and 1 nickel")]
        public void TestIfChangeGivenCorrectly(string input, string expected) //change given is correct and least amount of coins possible
        {
            // Assign
            VendingMachine vm = new VendingMachine();

            // Act
            vm.money.AddMoney(input);
            string result = vm.money.GiveChange();

            // Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestIfReturningOutOfStock() //correct message give (SOLD OUT)
        {// this test is fragile. 
            // Assign
            VendingMachine vm = new VendingMachine();

            // Act
            var menu = new MainMenu();
            vm.money.AddMoney("10");
            vm.GetItem("D4");
            vm.GetItem("D4");
            vm.GetItem("D4");
            vm.GetItem("D4");
            vm.GetItem("D4");
            vm.GetItem("D4");
            string result = vm.VendingMachineItems["D4"].MessageWhenSoldOut;
            string expected = $"Sorry! We are sold out of Triplemint. Please select another item.";

            // Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestNotEnoughMoneyToPurchaseItem() //correct message give (NOT ENOUGH MONEY)
        {
            // Assign
            VendingMachine vm = new VendingMachine();

            // Act
            vm.GetItem("D4");
            string result = vm.NotEnoughMoney;


            // Assert
            Assert.AreEqual("Sorry, please insert more money into the machine to complete the transaction. ", result);
        }
        [TestMethod]
        public void TestInvalidItemIfItemDoesNotExist()  //correct message give (INVALID ITEM)
        {
            // Assign
            VendingMachine vm = new VendingMachine();

            // Act
            string result = vm.InvalidItem;

            // Assert
            Assert.AreEqual("Invalid Item Selected. Please try again. ", result);
        }
      
      

    }
}

