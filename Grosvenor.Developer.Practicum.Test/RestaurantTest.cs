using Grosvenor.Developer.Practicum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Grosvenor.Developer.Practicum.Enums;

namespace Grosvenor.Developer.Practicum.Test
{   
    
    /// <summary>
    ///This is a test class for RestaurantTest and is intended
    ///to contain all RestaurantTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RestaurantTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region ValidteTimeOfDayInputTest

        /// <summary>
        ///A test for valid Time of Day Valid using "Morning"
        ///Rule: 1. You must enter time of day as “morning” or “night”
        ///Rule: 5.	Input is not case sensitive
        ///</summary>
        [TestMethod()]
        public void ValidteTimeOfDayInputTest_Valid_1()
        {
            Restaurant target = new Restaurant();
            string paramOrderTimeOfDayInputString = TimeOfDayEnum.Morning.ToString().ToLower(); 
            TimeOfDayEnum expected = TimeOfDayEnum.Morning; 
            TimeOfDayEnum actual;
            actual = target.ValidteTimeOfDayInput(paramOrderTimeOfDayInputString);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for invalid Time of Day using "Invalid"
        ///Rule: 1. You must enter time of day as “morning” or “night”
        ///</summary>
        [TestMethod()]
        public void ValidteTimeOfDayInputTest_InValid_1()
        {
            Restaurant target = new Restaurant();
            string paramOrderTimeOfDayInputString = "Invalid";
            TimeOfDayEnum expected = TimeOfDayEnum.None;
            TimeOfDayEnum actual;
            actual = target.ValidteTimeOfDayInput(paramOrderTimeOfDayInputString);
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        ///A test for invalid Time of Day using number "1"
        ///Rule: 1. You must enter time of day as “morning” or “night”. [Not as a number.]
        ///</summary>
        [TestMethod()]
        public void ValidteTimeOfDayInputTest_InValid_2()
        {
            Restaurant target = new Restaurant();
            string paramOrderTimeOfDayInputString = "1";
            TimeOfDayEnum expected = TimeOfDayEnum.None;
            TimeOfDayEnum actual;
            actual = target.ValidteTimeOfDayInput(paramOrderTimeOfDayInputString);
            Assert.AreEqual(expected, actual); //Assert.AreNotEqual(expected, TimeOfDayEnum.Morning);            
        }

        #endregion

        #region ValidateDishTypeInputTest

        /// <summary>
        ///A test for Validate Dish Type
        ///Rule: 6.	If invalid selection is encountered, display valid selections up to the error, then print error
        ///</summary>
        [TestMethod()]
        public void ValidateDishTypeInputTest_Valid_1()
        {
            Restaurant target = new Restaurant();
            string paramDishTypeInputString = "1";
            DishTypeEnum expected = DishTypeEnum.Entree;
            DishTypeEnum actual;
            actual = target.ValidateDishTypeInput(paramDishTypeInputString);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Validate Dish Type
        ///Rule: 6.	If invalid selection is encountered, display valid selections up to the error, then print error
        ///</summary>
        [TestMethod()]
        public void ValidateDishTypeInputTest_InValid_1()
        {
            Restaurant target = new Restaurant();
            string paramDishTypeInputString = "5";
            DishTypeEnum expected = DishTypeEnum.None;
            DishTypeEnum actual;
            actual = target.ValidateDishTypeInput(paramDishTypeInputString);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ValidateDishTest

        /// <summary>
        ///A test for valid Dish identification by time of day
        ///Rule: 6.	If invalid selection is encountered, display valid selections up to the error, then print error
        ///</summary>
        [TestMethod()]
        public void ValidateDishTest_Valid_1()
        {
            Restaurant target = new Restaurant();
            TimeOfDayEnum paramTimeOfDay = TimeOfDayEnum.Morning; 
            DishTypeEnum paramDishType = DishTypeEnum.Entree; 
            DishEnum expected = DishEnum.Eggs; 
            DishEnum actual;
            actual = target.ValidateDish(paramTimeOfDay, paramDishType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for invalid Dish identification by time of day
        ///Rule: 6.	If invalid selection is encountered, display valid selections up to the error, then print error
        ///</summary>
        [TestMethod()]
        public void ValidateDishTest_InValid_1()
        {
            Restaurant target = new Restaurant();
            TimeOfDayEnum paramTimeOfDay = TimeOfDayEnum.Morning;
            DishTypeEnum paramDishType = DishTypeEnum.Desert;
            DishEnum expected = DishEnum.None;
            DishEnum actual;
            actual = target.ValidateDish(paramTimeOfDay, paramDishType);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region ValidteOrderInputTest

        /// <summary>
        ///A test for valid Order input
        ///Rule: 2.	You must enter a comma delimited list of dish types with at least one selection
        ///</summary>
        [TestMethod()]
        public void ValidteOrderInputTest_Valid_1()
        {
            Restaurant target = new Restaurant(); 
            string paramOrderInputString = "morning, 1"; 
            bool expected = true; 
            bool actual;
            actual = target.ValidteOrderInput(paramOrderInputString);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for invalid Order input - no selection
        ///Rule: 2.	You must enter a comma delimited list of dish types with at least one selection
        ///</summary>
        [TestMethod()]
        public void ValidteOrderInputTest_InValid_1()
        {
            Restaurant target = new Restaurant();
            string paramOrderInputString = "morning";
            bool expected = false;
            bool actual;
            actual = target.ValidteOrderInput(paramOrderInputString);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for invalid Order input - incorrect delimiter
        ///Rule: 2.	You must enter a comma delimited list of dish types with at least one selection
        ///</summary>
        [TestMethod()]
        public void ValidteOrderInputTest_InValid_2()
        {
            Restaurant target = new Restaurant();
            string paramOrderInputString = "morning; 1";
            bool expected = false;
            bool actual;
            actual = target.ValidteOrderInput(paramOrderInputString);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region PlaceOrderTest

        /// <summary>
        ///A test for valid Place Order
        ///</summary>
        [TestMethod()]
        public void PlaceOrderTest_Valid_1()
        {
            Restaurant target = new Restaurant();
            string paramOrderInputString = "morning, 2, 1, 3";
            int expected = 1; 
            int actual;
            actual = target.PlaceOrder(paramOrderInputString);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for valid Place Order
        ///</summary>
        [TestMethod()]
        public void PlaceOrderTest_Valid_2()
        {
            Restaurant target = new Restaurant();
            string paramOrderInputString = "morning, 2, 1, 3";
            string expected = "morning, 2, 1, 3";
            string actual;
            target.PlaceOrder(paramOrderInputString);
            var orderDeails = target.GetOrder();
            actual = orderDeails.RowOrderData;
            Assert.AreEqual(actual, expected); //Assert.Equals(orderDeails.IsValid, true);
        }

        /// <summary>
        ///A test for invalid Place Order
        ///</summary>
        [TestMethod()]
        public void PlaceOrderTest_InValid_1()
        {
            Restaurant target = new Restaurant();
            string paramOrderInputString = "morning, 2, 1, 2, 3";
            bool expected = false;
            bool actual;
            target.PlaceOrder(paramOrderInputString);
            var orderDeails = target.GetOrder();
            actual = orderDeails.IsValid;
            Assert.AreEqual(actual, expected);
        }

        #endregion

        #region ProcessOrderTest

        /// <summary>
        ///A test for valid Process Order
        ///</summary>
        [TestMethod()]
        public void ProcessOrderTest_Valid_1()
        {
            Restaurant target = new Restaurant();
            string paramOrderInputString = "morning, 2, 1, 3";
            string expected = "eggs, toast, coffee";
            string actual;
            target.PlaceOrder(paramOrderInputString);
            actual = target.ProcessOrder().Trim();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for valid Process Order with multiple repeatable item
        ///</summary>
        [TestMethod()]
        public void ProcessOrderTest_Valid_2()
        {
            Restaurant target = new Restaurant();
            string paramOrderInputString = "morning, 1, 2, 3, 3, 3";
            string expected = "eggs, toast, coffee(x3)";
            string actual;
            target.PlaceOrder(paramOrderInputString);
            actual = target.ProcessOrder().Trim();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for invalid Process Order
        ///</summary>
        [TestMethod()]
        public void ProcessOrderTest_InValid_1()
        {
            Restaurant target = new Restaurant();
            string paramOrderInputString = "night, 1, 2, 3, 5";
            string expected = "steak, potato, wine, error";
            string actual;
            target.PlaceOrder(paramOrderInputString);
            actual = target.ProcessOrder().Trim();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for invalid Process Order with multiple non-repeatable item
        ///</summary>
        [TestMethod()]
        public void ProcessOrderTest_InValid_2()
        {
            Restaurant target = new Restaurant();
            string paramOrderInputString = "night, 1, 1";
            string expected = "steak, error";
            string actual;
            target.PlaceOrder(paramOrderInputString);
            actual = target.ProcessOrder().Trim();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
