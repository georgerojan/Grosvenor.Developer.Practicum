using Grosvenor.Developer.Practicum.Enums;

namespace Grosvenor.Developer.Practicum.Types
{
    public class MenuBase
    {
        public TimeOfDayEnum MealType { get; set; }
        public DishTypeEnum DishType { get; set; }
        public DishEnum Dish { get; set; }
    }
}
