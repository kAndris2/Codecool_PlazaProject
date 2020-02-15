using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.plaza.api
{
    class FoodProduct : Product
    {
        private int calories { get; set; }
        private DateTime bestBefore { get; set; }

        public FoodProduct(long barcode, String name, String manufacturer, int calories, DateTime bestBefore) : base(barcode, name, manufacturer)
        {
            this.calories = calories;
            this.bestBefore = bestBefore;
        }

        public Boolean IsStillConsumable()
        {
            if (DateTime.Compare(bestBefore, DateTime.Now) < 0)
                return false;
            return true;
        }

        public int GetCalories()
        {
            return calories;
        }

        public override string ToString()
        {
            return $"Barcode: {GetBarcode()}\n" +
                   $"Name: {GetName()}\n" +
                   $"Manufacturer: {GetManufacturer()}\n" +
                   $"Calories: {GetCalories()}\n" +
                   $"{(IsStillConsumable() == true ? "This product is still consumable." : "This product has already deteriorated.")}";
        }
    }
}
