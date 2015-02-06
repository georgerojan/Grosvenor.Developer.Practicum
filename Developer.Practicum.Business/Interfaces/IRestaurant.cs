using System;
using Grosvenor.Developer.Practicum.Types;
namespace Grosvenor.Developer.Practicum.Interfaces
{
    public interface IRestaurant
    {
        /// <summary>
        /// Validate order details input string and return order id
        /// </summary>
        /// <returns>Return order id.</returns>
        int PlaceOrder(string paramOrderInputString);

        /// <summary>
        /// Process the next order in order queue
        /// </summary>
        /// <returns>Returns the order details as string</returns>
        string ProcessOrder();

        /// <summary>
        /// Get next Order details
        /// </summary>
        /// <returns>Order Details</returns>
        Order<OrderItem> GetOrder();
    }
}
