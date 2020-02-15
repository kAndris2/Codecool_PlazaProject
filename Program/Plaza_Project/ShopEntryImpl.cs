using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.plaza.api
{
    class ShopEntryImpl
    {
        private Product product { get; set; }
        private int quantity { get; set; }
        private float price { get; set; }

        public ShopEntryImpl(Product product, int quantity, float price)
        {
            this.product = product;
            this.quantity = quantity;
            this.price = price;
        }

        public Product GetProduct()
        {
            return product;
        }

        public void SetProduct(Product product)
        {
            this.product = product;
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public void SetQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public void IncreaseQuantity(int amount)
        {
            quantity += amount;
        }

        public void DecreaseQuantity(int amount)
        {
            quantity -= amount;
        }

        public float GetPrice()
        {
            return price;
        }

        public void SetPrice(int price)
        {
            this.price = price;
        }

        public override string ToString()
        {
            return $"Product name: {product.GetName()}\n" +
                   $"Quantity: {GetQuantity()}\n" +
                   $"Price: {GetPrice()}";
        }
    }
}
