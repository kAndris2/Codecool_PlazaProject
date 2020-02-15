using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.plaza.api
{
    interface Plaza
    {
        public List<Shop> GetShops();
        public void AddShop(Shop shop);
        public void RemoveShop(Shop shop);
        public Shop FindShopByName(String name);
        public Boolean IsOpen();
        public void Open();
        public void Close();
        public String ToString();
    }
}
