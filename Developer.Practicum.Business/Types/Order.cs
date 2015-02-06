using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grosvenor.Developer.Practicum.Common;

namespace Grosvenor.Developer.Practicum.Types
{
    public class Order<T> where T : OrderItem
    {
        public int OrderNumber { get; set; }
        public string RowOrderData { get; set; }
        public List<T> Items { get; set; }
        public bool IsValid { get; set; }

        public override string ToString()
        {
            if (this.Items.Count == 0)
                return Constants.ErrorText;

            int totalItems = this.Items.Count;
            int itemCounter = 0;

            StringBuilder orderDetails = new StringBuilder();
            if (this.IsValid)
            {
                this.Items.OrderBy(item => item.DishType).ToList().ForEach(currentItem =>
                {
                    itemCounter++;
                    orderDetails.Append(string.Format("{0}{1} ", currentItem.ToString(), itemCounter == totalItems ? string.Empty : Constants.InputSeparator.ToString()));
                });
            }
            else
            {
                this.Items.ForEach(currentItem =>
                {
                    itemCounter++;
                    orderDetails.Append(string.Format("{0}{1} ", currentItem.ToString(), itemCounter == totalItems ? string.Empty : Constants.InputSeparator.ToString()));
                });
            }


            return orderDetails.ToString();
        }
    }
}
