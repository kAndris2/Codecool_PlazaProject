using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.plaza.api
{
    interface Shop
    {
        public String GetName();
        public String GetOwner();
        public Boolean IsOpen();
        public void Open();
        public void Close();
        public List<Product> GetProducts();
        public Product FindByName(String name);
        public float GetPrice(long barcode);
        public Boolean HasProduct(long barcode);
        public void AddNewProduct(Product product, int quantity, float price);
        public void AddProduct(long barcode, int quantity);
        public Product BuyProduct(long barcode);
        public List<Product> BuyProducts(long barcode, int quantity);
        public String ToString();
    }
}
