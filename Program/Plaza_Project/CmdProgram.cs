using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.plaza.cmdprog
{
    class CmdProgram
    {
        private List<api.Product> cart { get; set; } = new List<api.Product>();
        private List<float> prices { get; set; } = new List<float>();
        private api.PlazaImpl plaza;

        public CmdProgram(String[] args)
        {

        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("There are no plaza created yet!\n\n" +
                                "1) to create a new plaza.\n" +
                                "2) to exit.");
                Console.WriteLine("\nPlease type a hotkey) to choose menu items.");

                try
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        Console.Clear();

                        Console.WriteLine("What will be the plaza name?");
                        plaza = new api.PlazaImpl(Console.ReadLine());

                        Console.Clear();
                        StartPlaza();
                        break;
                    }
                    else if (input == "2")
                    {
                        Environment.Exit(-1);
                    }
                    else
                        throw new KeyNotFoundException($"There is no such option! - ('{input}')");
                }
                catch (KeyNotFoundException e) { ManageEx(e.Message); }
            }
        }

        public void StartPlaza()
        {
            while (true)
            {
                PlazaMenu();
                try
                {
                    if (!PlazaOptions())
                        break;
                    else
                    {
                        Console.WriteLine("\n--->[Press enter to continue.]");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                catch (KeyNotFoundException e) { ManageEx(e.Message); }
                catch (api.ShopException e) { ManageEx(e.Message); }
            }
        }

            public void PlazaMenu()
        {
            Console.WriteLine($"Welcome to {plaza.GetName()} plaza!\n");
            List<string> options = new List<string>
                {
                    "to list all shops.",
                    "to add a new shop.",
                    "to remove an existing shop.",
                    "enter a shop by name.",
                    "to open the plaza.",
                    "to close the plaza.",
                    "to check if the plaza is open or not."
                };

            for (int i = 0; i < options.Count; i++)
                Console.WriteLine($"{i + 1}) {options[i]}");
            Console.WriteLine("\nN) leave plaza.");
        }

        public Boolean PlazaOptions()
        {
            Console.WriteLine("\nPlease type a hotkey) to choose menu items.");
            string input = Console.ReadLine();

            if (input == "1")
            {
                if (plaza.GetShops().Count == 0)
                    throw new api.NoSuchShopException("You haven't created any shop yet!");

                Console.Clear();

                int count = -1;
                foreach(api.Shop shop in plaza.GetShops())
                {
                    count++;
                    Console.WriteLine($"{count + 1}.\n" +
                                      $"{shop.ToString()}");
                }

                return true;
            }
            else if (input == "2")
            {
                Console.Clear();

                string[] questions = new string[2] {
                                                        "Enter a shop name.",
                                                        "Enter a shop owner."
                                                    };
                string[] answers = new string[questions.Length];
                for (int i = 0; i < questions.Length; i++)
                {
                    Console.WriteLine(questions[i]);
                    answers[i] = Console.ReadLine();
                }

                plaza.AddShop(new api.ShopImpl(answers[0], answers[1]));
                Console.WriteLine($"\nYou have successfully added a new shop to the {plaza.GetName()} plaza. - ('{answers[0]}') ");

                return true;
            }
            else if (input == "3")
            {
                if (plaza.GetShops().Count == 0)
                    throw new api.NoSuchShopException("You haven't created any shop yet!");

                Console.Clear();

                Console.WriteLine("Which shop do you want to delete?");
                string shopname = Console.ReadLine();

                foreach (api.Shop shop in plaza.GetShops())
                {
                    if (shop.GetName().Equals(shopname))
                    {
                        plaza.RemoveShop(shop);
                        Console.WriteLine($"You have successfully removed '{shopname}' from {plaza.GetName()} plaza.");
                        break;
                    }
                }

                return true;
            }
            else if (input == "4")
            {
                if (plaza.GetShops().Count == 0)
                    throw new api.NoSuchShopException("You haven't created any shop yet!");

                Console.Clear();

                Console.WriteLine("Which shop would you like to go?");
                StartShop(plaza.FindShopByName(Console.ReadLine()));

                return true;
            }
            else if (input == "5")
            {
                Console.Clear();

                plaza.Open();
                Console.WriteLine($"You have successfully opened {plaza.GetName()} plaza.");

                return true;
            }
            else if (input == "6")
            {
                Console.Clear();

                plaza.Close();
                Console.WriteLine($"You have successfully closed {plaza.GetName()} plaza.");

                return true;
            }
            else if (input == "7")
            {
                Console.Clear();

                Console.WriteLine($"{plaza.GetName()} plaza status is: {(plaza.IsOpen() == true ? "Open" : "Closed")}");

                return true;
            }
            else if (input == "N")
            {
                Console.Clear();

                Console.WriteLine("[The contents of your cart]\n");
                for (int i = 0; i < cart.Count; i++)
                {
                    Console.WriteLine($"{cart[i].ToString()}\n" +
                                      $"Price: {prices[i]}$\n");
                }

                Console.WriteLine($"\nThank you for shopping with us!");
                Environment.Exit(-1);
                return true;
            }
            else
                throw new KeyNotFoundException($"There is no such option! - ('{input}')");
        }

        public void StartShop(api.Shop shop)
        {
            Console.Clear();
            while (true)
            {
                ShopMenu(shop);
                try
                {
                    if (!ShopOptions(shop))
                        break;
                    else
                    {
                        Console.WriteLine("\n--->[Press enter to continue.]");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                catch (KeyNotFoundException e) { ManageEx(e.Message); }
                catch (api.ShopException e) { ManageEx(e.Message); }
            }
        }

        public void ShopMenu(api.Shop shop)
        {

            Console.WriteLine($"Hi! This is the {shop.GetName()} store, welcome!\n");
            List<string> options = new List<string>
                {
                    "to list available products.",
                    "to find products by name.",
                    "to display the shop's owner.",
                    "to open the shop.",
                    "to close the shop.",
                    "to add new product to the shop.",
                    "to add existing products to the shop.",
                    "to buy a product by barcode",
                    "check price by barcode"
                };

            for (int i = 0; i < options.Count; i++)
                Console.WriteLine($"{i + 1}) {options[i]}");
            Console.WriteLine("\nN) go back to plaza.");
        }

        public Boolean ShopOptions(api.Shop shop)
        {
            Console.WriteLine("\nPlease type a hotkey) to choose menu items.");
            string input = Console.ReadLine();

            if (input == "1")
            {
                if (shop.GetProducts().Count == 0)
                    throw new api.NoSuchProductException("You haven't created any product yet!");

                Console.Clear();

                int count = -1;
                foreach (api.Product product in shop.GetProducts())
                {
                    count++;
                    Console.WriteLine($"{count + 1}.\n" +
                                      $"{product.ToString()}\n");
                }

                return true;
            }
            else if (input == "2")
            {
                if (shop.GetProducts().Count == 0)
                    throw new api.NoSuchProductException("You haven't created any product yet!");

                Console.Clear();

                Console.WriteLine("Which product are you looking for?");
                string pName = Console.ReadLine();
                Console.WriteLine(shop.FindByName(pName).ToString());

                return true;
            }
            else if (input == "3")
            {
                Console.Clear();

                Console.WriteLine($"{shop.GetOwner()} is the owner of {shop.GetName()}.");

                return true;
            }
            else if (input == "4")
            {
                Console.Clear();

                shop.Open();
                Console.WriteLine($"You have successfully opened {shop.GetName()} store.");

                return true;
            }
            else if (input == "5")
            {
                Console.Clear();

                shop.Close();
                Console.WriteLine($"You have successfully closed {shop.GetName()} store.");

                return true;
            }
            else if (input == "6")
            {
                Console.Clear();

                Console.WriteLine("Which type of product do you want to create? (Food/Clothing)");
                string type = Console.ReadLine();

                string[] q = new string[]
                {
                    "Enter the product name.",
                    "Enter the product manufacturer.",
                    $"{(type.ToLower() == "clothing" ? "Enter the product material." : "Enter the calorie content of the product.")}",
                    $"{(type.ToLower() == "clothing" ? "Enter the product type." : "Best before? (dd/mm/yy)")}",
                    "How many products do you want to create?",
                    "How much does the product cost?"
                };
                string[] a = new string[q.Length];

                for (int i = 0; i < q.Length; i++)
                {
                    Console.WriteLine(q[i]);
                    a[i] = Console.ReadLine();
                }

                api.Product product = null;
                if (type.ToLower() == "food")
                    product = new api.FoodProduct(GenerateBarcode(), a[0], a[1], int.Parse(a[2]), Convert.ToDateTime(a[3]));
                else if (type.ToLower() == "clothing")
                    product = new api.ClothingProduct(GenerateBarcode(), a[0], a[1], a[2], a[3]);

                shop.AddNewProduct(product, int.Parse(a[4]), float.Parse(a[5]));
                Console.WriteLine($"\nYou have successfully added new product to the shop. - ('{product.GetName()}')");

                return true;
            }
            else if (input == "7")
            {
                if (shop.GetProducts().Count == 0)
                    throw new api.NoSuchProductException("You haven't created any product yet!");

                Console.Clear();

                string[] questions = new string[]
                {
                    "Enter the product barcode.",
                    "How many products do you want to add?"
                };
                string[] answers = new string[questions.Length];

                for (int i = 0; i < questions.Length; i++)
                {
                    Console.WriteLine(questions[i]);
                    answers[i] = Console.ReadLine();
                }
                shop.AddProduct(Convert.ToInt64(answers[0]), int.Parse(answers[1]));
                Console.WriteLine($"You have successfully added {answers[1]} new product.");

                return true;
            }
            else if (input == "8")
            {
                if (shop.GetProducts().Count == 0)
                    throw new api.NoSuchProductException("You haven't created any product yet!");

                Console.Clear();

                Console.WriteLine("Enter the product barcode.");
                long barcode = Convert.ToInt64(Console.ReadLine());

                if (shop.HasProduct(barcode))
                {
                    cart.Add(shop.BuyProduct(barcode));
                    prices.Add(shop.GetPrice(barcode));
                    Console.WriteLine("You have successfully placed the product in your cart.");
                }

                return true;
            }
            else if (input == "9")
            {
                if (shop.GetProducts().Count == 0)
                    throw new api.NoSuchProductException("You haven't created any product yet!");

                Console.Clear();

                Console.WriteLine("Enter the product barcode.");
                long barcode = Convert.ToInt64(Console.ReadLine());

                if (shop.HasProduct(barcode))
                    Console.WriteLine($"The product price is {shop.GetPrice(barcode)}$.");

                return true;
            }
            else if (input == "N")
            {
                return false;
            }
            else
                throw new KeyNotFoundException($"There is no such option! - ('{input}')");
        }

        public void ManageEx(string message)
        {
            Console.Clear();
            Console.WriteLine($"[ERROR]: {message}\n");
        }

        private long GenerateBarcode()
        {
            Random random = new Random();
            byte[] buf = new byte[8];
            random.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return Math.Abs(longRand % (1 - 9999)) + 1;
        }
    }
}
