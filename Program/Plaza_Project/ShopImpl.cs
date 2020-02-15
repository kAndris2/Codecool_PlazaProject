using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.plaza.api
{
    class ShopImpl : Shop
    {
        private String name { get; set; }
        private String owner { get; set; }
        private Dictionary<long, ShopEntryImpl> products { get; set; } = new Dictionary<long, ShopEntryImpl>();
        private Boolean status { get; set; }

        public ShopImpl(String name, String owner)
        {
            this.name = name;
            this.owner = owner;
        }

        public void AddNewProduct(Product product, int quantity, float price)
        {
            if (!status)
                throw new ShopIsClosedException($"'{name}' store is still closed!");

            foreach (KeyValuePair<long, ShopEntryImpl> item in products)
            {
                if (item.Value.GetProduct().Equals(product))
                    throw new ProductAlreadyExistsException($"This product is already exist! - ('{product.GetName()}')");
            }

            products.Add(product.GetBarcode(), new ShopEntryImpl(product, quantity, price));
        }

        public void AddProduct(long barcode, int quantity)
        {
            if (!status)
                throw new ShopIsClosedException($"'{name}' store is still closed!");

            bool check = false;
            foreach (KeyValuePair<long, ShopEntryImpl> item in products)
            {
                if (item.Key == barcode)
                {
                    item.Value.IncreaseQuantity(quantity);
                    check = true;
                    break;
                }
            }

            if (!check)
                throw new NoSuchProductException($"No such product exist! - ('{barcode}')");
        }

        public Product BuyProduct(long barcode)
        {
            if (!status)
                throw new ShopIsClosedException($"'{name}' store is still closed!");

            foreach (KeyValuePair<long, ShopEntryImpl> item in products)
            {
                if (item.Key == barcode)
                {
                    if (item.Value.GetQuantity() >= 1)
                    {
                        item.Value.DecreaseQuantity(1);
                        return item.Value.GetProduct();
                    }
                    else
                        throw new OutOfStockException($"This product is currently out of stock! - ('{barcode}')");
                }
            }
            throw new NoSuchProductException($"No such product exist! - ('{barcode}')");
        }

        public List<Product> BuyProducts(long barcode, int quantity)
        {
            if (!status)
                throw new ShopIsClosedException($"'{name}' store is still closed!");

            List<Product> list = new List<Product>();

            foreach (KeyValuePair<long, ShopEntryImpl> item in products)
            {
                if (item.Key == barcode)
                {
                    if (item.Value.GetQuantity() >= quantity)
                    {
                        for (int i = 0; i < quantity; i++)
                            list.Add(item.Value.GetProduct());
                        return list;
                    }
                    else
                        throw new OutOfStockException($"This product is currently out of stock! - ('{barcode}')");
                }
            }
            throw new NoSuchProductException($"No such product exist! - ('{barcode}')");
        }

        public void Close()
        {
            if (!IsOpen())
                throw new AlreadyInStatus($"{name} store is already closed!");

            status = false;
        }

        public Product FindByName(string name)
        {
            if (!status)
                throw new ShopIsClosedException($"'{this.name}' store is still closed!");

            foreach (KeyValuePair<long, ShopEntryImpl> item in products)
            {
                if (item.Value.GetProduct().GetName() == name)
                    return item.Value.GetProduct();
            }
            throw new NoSuchProductException($"No such product exist! - ('{name}')");
        }

        public string GetName()
        {
            return name;
        }

        public string GetOwner()
        {
            return owner;
        }

        public float GetPrice(long barcode)
        {
            if (!status)
                throw new ShopIsClosedException($"'{name}' store is still closed!");

            float result = products[barcode].GetPrice();

            if (result > 0)
                return result;
            else
                throw new NoSuchProductException($"No such product exist! - ('{barcode}')");
        }

        public List<Product> GetProducts()
        {
            if (!status)
                throw new ShopIsClosedException($"'{name}' store is still closed!");

            List<Product> list = new List<Product>();

            foreach(KeyValuePair<long, ShopEntryImpl> item in products)
            {
                list.Add(item.Value.GetProduct());
            }
            return list;
        }

        public bool HasProduct(long barcode)
        {
            if (!status)
                throw new ShopIsClosedException($"'{name}' store is still closed!");

            foreach (KeyValuePair<long, ShopEntryImpl> item in products)
            {
                if (item.Key == barcode)
                    return true;
            }
            return false;
        }

        public bool IsOpen()
        {
            return status;
        }

        public void Open()
        {
            if (IsOpen())
                throw new AlreadyInStatus($"{name} store is already open!");

            status = true;
        }

        public override string ToString()
        {
            return $"Name: {name}\n" +
                   $"Owner: {owner}\n" +
                   $"Status: {(status == true ? "Open" : "Closed")}";
        }
    }
}
