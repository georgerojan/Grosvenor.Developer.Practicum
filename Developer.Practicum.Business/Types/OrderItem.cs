using System.Text;
using Grosvenor.Developer.Practicum.Common;

namespace Grosvenor.Developer.Practicum.Types
{
    public class OrderItem : MenuBase
    {
        public long Quantity { get; set; }
        public bool IsValid { get; internal set; }

        public override string ToString()
        {
            return this.IsValid ? string.Format("{0}{1}", this.Dish.ToString().ToLower(), this.Quantity > 1 ? new StringBuilder().AppendFormat("(x{0})", this.Quantity).ToString() : string.Empty) : Constants.ErrorText;
        }
    }
}
