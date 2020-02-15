using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.plaza.api
{
    class PlazaImpl : Plaza
    {
        private List<Shop> shops { get; set; }
        private Boolean status { get; set; }
        private string name { get; set; }

        public PlazaImpl(string name)
        {
            this.name = name;
            shops = new List<Shop>();
        }

        public void AddShop(Shop shop)
        {
            if (!status)
                throw new PlazaIsClosedException("The plaza is still closed!");

            foreach (Shop item in shops)
            {
                if (item.Equals(shop))
                    throw new ShopAlreadyExistException($"This shop is already exist! - ('{shop.GetName()}')");
            }

            shops.Add(shop);
        }

        public void Close()
        {
            if (!IsOpen())
                throw new AlreadyInStatus($"{name} plaza is already closed!");

            status = false;
        }

        public List<Shop> GetShops()
        {
            if (!status)
                throw new PlazaIsClosedException("The plaza is still closed!");

            return shops;
        }

        public bool IsOpen()
        {
            return status;
        }

        public void Open()
        {
            if (IsOpen())
                throw new AlreadyInStatus($"{name} plaza is already open!");

            status = true;
        }

        public void RemoveShop(Shop shop)
        {
            if (!status)
                throw new PlazaIsClosedException("The plaza is still closed!");

            bool check = false;
            foreach(Shop item in shops)
            {
                if (item.Equals(shop))
                {
                    shops.Remove(shop);
                    check = true;
                    break;
                }
            }

            if (!check)
                throw new NoSuchShopException($"No such shop exist!");
        }

        public Shop FindShopByName(string name)
        {
            if (!status)
                throw new PlazaIsClosedException("The plaza is still closed!");

            foreach(Shop shop in shops)
            {
                if (shop.GetName() == name)
                    return shop;
            }
            throw new NoSuchShopException($"No such shop exist! - ('{name}')");
        }

        public string GetName()
        {
            return name;
        }
    }
}
