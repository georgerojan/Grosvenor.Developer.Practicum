using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Grosvenor.Developer.Practicum.Common;
using Grosvenor.Developer.Practicum.Enums;
using Grosvenor.Developer.Practicum.Interfaces;
using Grosvenor.Developer.Practicum.Types;

namespace Grosvenor.Developer.Practicum
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IRestaurant))]
    public class Restaurant : IRestaurant
    {
        #region Member Data

        private Menu<MenuItem> Menu { get; set; }
        private Queue<Order<OrderItem>> Orders { get; set; }
        
        private int orderSequence = 0;
        public int OrderSequence 
        {
            get
            {
                lock (this)
                {
                    return ++orderSequence;
                }
            }
        }

        #endregion

        #region Constructure
        
        /// <summary>
        /// Constructor. Initilize and load food menu
        /// </summary>
        public Restaurant()
        {
            this.Initialize();
        }

        #endregion
        
        #region Public Methods
        
        public int PlaceOrder(string paramOrderInputString)
        {
            var currentOrder = this.ValidateCreateOrder(paramOrderInputString);
            this.Orders.Enqueue(currentOrder);
            return currentOrder.OrderNumber;
        }
                
        public string ProcessOrder()
        {
            if (this.Orders == null || this.Orders.Count == 0)
                return Constants.ErrorText;

            return this.Orders.Dequeue().ToString();
        }

        public Order<OrderItem> GetOrder()
        {
            if (this.Orders.Count == 0)
                return new Order<OrderItem>() { Items = new List<OrderItem>(), RowOrderData = string.Empty, IsValid = false, OrderNumber = -1 };

            return this.Orders.Peek();
        }
        
        #endregion

        #region Create Methods
        
        private Order<OrderItem> ValidateCreateOrder(string paramOrderInputString)
        {
            TimeOfDayEnum timeOfDay = TimeOfDayEnum.None;

            if (this.ValidteOrderInput(paramOrderInputString))
            {
                var timeOfDayInputString = paramOrderInputString.ToLower().Split(Constants.InputSeparator)[0].Trim();
                if (!timeOfDayInputString.All(char.IsDigit))
                    timeOfDay = this.ValidteTimeOfDayInput(timeOfDayInputString);
            }

            if (timeOfDay == TimeOfDayEnum.None)
                return new Order<OrderItem> { OrderNumber = this.OrderSequence, RowOrderData = paramOrderInputString, Items = new List<OrderItem>(), IsValid = false };

            var currentOrder = this.CreateOrder(timeOfDay, paramOrderInputString.Remove(0, paramOrderInputString.IndexOf(Constants.InputSeparator) + 1));
            currentOrder.RowOrderData = paramOrderInputString;

            return currentOrder;
        }

        private Order<OrderItem> CreateOrder(TimeOfDayEnum paramTimeOfDay, string paramOrderItemsInputString)
        {
            //var currentOrderItemsInputString = paramOrderInputString.Remove(0, paramOrderInputString.IndexOf(Constants.InputSeparator) + 1);
            var currentOrderItemsInputArray = paramOrderItemsInputString.Split(Constants.InputSeparator).ToList();
            var currentOrderItemList = new List<OrderItem>();
            bool isValidOrder = true;

            foreach (var currentDishItemString in currentOrderItemsInputArray)
            {
                if (string.IsNullOrWhiteSpace(currentDishItemString))
                    continue;
                var dishType = DishTypeEnum.None; var dish = DishEnum.None; var updateOnly = false;
                
                dishType = this.ValidateDishTypeInput(currentDishItemString);

                if (dishType == DishTypeEnum.None)
                    isValidOrder = false; //Process for error and abort the order registration
                
                if (isValidOrder)
                    dish = this.ValidateDish(paramTimeOfDay, dishType);
                
                if (isValidOrder && dish == DishEnum.None)
                    isValidOrder = false; //Process for error and abort the order registration

                if (isValidOrder)
                {
                    var existingItem = currentOrderItemList.Find(item => item.MealType == paramTimeOfDay && item.DishType == dishType &&  item.Dish == dish); //Find existing item
                    if (existingItem != null)
                    {
                        var menuItem = this.Menu.Items.Find(item => item.MealType == paramTimeOfDay && item.DishType == dishType &&  item.Dish == dish); //Check if the item allows multiple quantity
                        if (menuItem.MaximumQuantity > 1)
                        {
                            existingItem.Quantity++;
                            updateOnly = true;
                        }
                        else
                            isValidOrder = false; //Process for error and abort the order registration
                    }
                }

                if (!updateOnly)
                    currentOrderItemList.Add(new OrderItem() { MealType = paramTimeOfDay, DishType = dishType, Dish = dish, Quantity = 1, IsValid = isValidOrder });

                if (!isValidOrder)
                    break;
            }

            return new Order<OrderItem> { OrderNumber = this.OrderSequence, Items = currentOrderItemList.ToList(), IsValid = isValidOrder };
        }

        /// <summary>
        /// Validate order details input string and return true or false
        /// </summary>
        /// <param name="paramOrderInputString">Order details input string</param>
        /// <returns>Return true if valid otherwise false</returns>
        public bool ValidteOrderInput(string paramOrderInputString)
        {
            return !string.IsNullOrWhiteSpace(paramOrderInputString) && paramOrderInputString.Trim().Split(Constants.InputSeparator).Length > 1;
        }

        #endregion

        #region Validate Methods
        /// <summary>
        /// Validate and return Time of Day
        /// </summary>
        /// <param name="paramOrderTimeOfDayInputString">Time of Day string value</param>
        /// <returns>Time of Day Enumeration</returns>
        public TimeOfDayEnum ValidteTimeOfDayInput(string paramOrderTimeOfDayInputString)
        {
            TimeOfDayEnum timeOfDay = TimeOfDayEnum.None;
            return !string.IsNullOrWhiteSpace(paramOrderTimeOfDayInputString) && !paramOrderTimeOfDayInputString.All(char.IsDigit) && Enum.TryParse(paramOrderTimeOfDayInputString.Trim().ToLower(), true, out timeOfDay) && Enum.IsDefined(typeof(TimeOfDayEnum), timeOfDay) ? timeOfDay : TimeOfDayEnum.None; //Can be optimized.
        }
        
        /// <summary>
        /// Validate and return Dish Type
        /// </summary>
        /// <param name="paramDishTypeInputString">Dish Type numeric value</param>
        /// <returns>Dish Type Enumeration</returns>
        public DishTypeEnum ValidateDishTypeInput(string paramDishTypeInputString)
        {
            var dishType = DishTypeEnum.None;
            return !string.IsNullOrWhiteSpace(paramDishTypeInputString) && Enum.TryParse(paramDishTypeInputString.Trim(), true, out dishType) && Enum.IsDefined(typeof(DishTypeEnum), dishType) ? dishType : DishTypeEnum.None;
        }

        /// <summary>
        /// Validate and return Dish for specified Time of Day and Dish Type
        /// </summary>
        /// <param name="paramTimeOfDay">Time of Day Enumeration</param>
        /// <param name="paramDishType">Dish Type Enumeration</param>
        /// <returns>Dish Enumeration</returns>
        public DishEnum ValidateDish(TimeOfDayEnum paramTimeOfDay, DishTypeEnum paramDishType)
        {
            DishEnum dish = DishEnum.None;
            var foodItem = this.Menu.Items.Find(item => item.MealType == paramTimeOfDay && item.DishType == paramDishType);
            if (foodItem != null)
                dish = foodItem.Dish;

            return dish;
        }

        #endregion

        #region Initialize Methods

        /// <summary>
        /// Load Food Menu.
        /// </summary>
        private void Initialize()
        {
            this.Menu = new Menu<MenuItem>
            {
                Items = new List<MenuItem> {
                                    //Morning
                                    new MenuItem() { MealType = TimeOfDayEnum.Morning, DishType = DishTypeEnum.Entree, Dish = DishEnum.Eggs, MaximumQuantity = 1 },
                                    new MenuItem() { MealType = TimeOfDayEnum.Morning, DishType = DishTypeEnum.Side, Dish = DishEnum.Toast, MaximumQuantity = 1 },
                                    new MenuItem() { MealType = TimeOfDayEnum.Morning, DishType = DishTypeEnum.Drink, Dish = DishEnum.Coffee, MaximumQuantity = long.MaxValue },
                                    //Night
                                    new MenuItem() { MealType = TimeOfDayEnum.Night, DishType = DishTypeEnum.Entree, Dish = DishEnum.Steak, MaximumQuantity = 1 },
                                    new MenuItem() { MealType = TimeOfDayEnum.Night, DishType = DishTypeEnum.Side, Dish = DishEnum.Potato, MaximumQuantity = long.MaxValue },
                                    new MenuItem() { MealType = TimeOfDayEnum.Night, DishType = DishTypeEnum.Drink, Dish = DishEnum.Wine, MaximumQuantity = 1 },
                                    new MenuItem() { MealType = TimeOfDayEnum.Night, DishType = DishTypeEnum.Desert, Dish = DishEnum.Cake, MaximumQuantity = 1 }
                                }
            };

            this.Orders = new Queue<Order<OrderItem>>();
        }

        #endregion

    }
}


//Check if the order input is in the correct format as expected
//Format - TimeOfDay,{{DishType 1},{DishType 2},{DishType 3},...{DishType n}}
//Time of day must be the first value in the list - This check is performed here
//There must be atleast 1 item in the order. Validity of the item check during the order registration
//Time of day can not be repeated - This is build into the order item processing
