using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.plaza.api
{
    class ClothingProduct : Product
    {
        private String material { get; set; }
        private String type { get; set; }

        public ClothingProduct(long barcode, String name, String manufacturer, String material, String type) : base(barcode, name, manufacturer)
        {
            this.material = material;
            this.type = type;
        }

        public String GetMaterial()
        {
            return material;
        }

        public String GetCPType()
        {
            return type;
        }

        public override string ToString()
        {
            return $"Barcode: {GetBarcode()}\n" +
                   $"Name: {GetName()}\n" +
                   $"Manufacturer: {GetManufacturer()}\n" +
                   $"Material: {GetMaterial()}\n" +
                   $"Type: {GetCPType()}";
        }
    }
}
