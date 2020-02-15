using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.plaza.api
{
    abstract class Product
    {
        protected long barcode { get; set; }
        protected String name { get; set; }
        protected String manufacturer { get; set; }

        public Product(long barcode, String name, String manufacturer)
        {
            this.barcode = barcode;
            this.name = name;
            this.manufacturer = manufacturer;
        }

        public long GetBarcode()
        {
            return barcode;
        }

        public String GetName()
        {
            return name;
        }

        public String GetManufacturer()
        {
            return manufacturer;
        }

        public override string ToString()
        {
            return $"Barcode: {GetBarcode()}\n" +
                   $"Name: {GetName()}\n" +
                   $"Manufacturer: {GetManufacturer()}";
        }
    }
}
