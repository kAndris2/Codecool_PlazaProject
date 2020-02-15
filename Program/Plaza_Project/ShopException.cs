using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.plaza.api
{
    class ShopException : Exception
    {
        public ShopException(string message) : base(message) { }
    }

    class PlazaIsClosedException : ShopException
    {
        public PlazaIsClosedException(string message) : base(message) { }
    }

    class ShopAlreadyExistException : ShopException
    {
        public ShopAlreadyExistException(string message) : base(message) { }
    }

    class NoSuchShopException : ShopException
    {
        public NoSuchShopException(string message) : base(message) { }
    }

    class ShopIsClosedException : ShopException
    {
        public ShopIsClosedException(string message) : base(message) { }
    }

    class NoSuchProductException : ShopException
    {
        public NoSuchProductException(string message) : base(message) { }
    }

    class OutOfStockException : ShopException
    {
        public OutOfStockException(string message) : base(message) { }
    }

    class ProductAlreadyExistsException : ShopException
    {
        public ProductAlreadyExistsException(string message) : base(message) { }
    }

    class AlreadyInStatus : ShopException
    {
        public AlreadyInStatus(string message) : base(message) { }
    }
}
