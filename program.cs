using System;
namespace shoppingcart
{
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public string Category;
        public int RemainingStock;

        public void DisplayProduct()
        {
            Console.WriteLine($"{Id,-5} {Name,-15} {Category,-15} {Price,-10} {RemainingStock,-10}");
        }
        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }
         }
    class Program
    {
        static int[]    cartIds  = new int[20];
        static int[]    cartQty  = new int[20];
        static double[] cartSub  = new double[20];
        static int      cartCount = 0;

        static int      receiptCounter = 1;
        static int[]    histReceipt = new int[100];
        static double[] histTotal   = new double[100];
        static string[] histDate    = new string[100];
        static int      histCount   = 0;

        static void Main(string[] args)
        {
           Product[] products =
            {
                new Product { Id=1,  Name="Mouse",        Category="Electronics", Price=400,  RemainingStock=40  },
                new Product { Id=2,  Name="Keyboard",     Category="Electronics", Price=750,  RemainingStock=25  },
                new Product { Id=3,  Name="Headset",      Category="Electronics", Price=1000, RemainingStock=15  },
                new Product { Id=4,  Name="Flash Drive",  Category="Electronics", Price=350,  RemainingStock=25  },
                new Product { Id=5,  Name="Monitor",      Category="Electronics", Price=4500, RemainingStock=15  },
                new Product { Id=6,  Name="Webcam",       Category="Electronics", Price=1150, RemainingStock=20  },
                new Product { Id=7,  Name="USB Hub",      Category="Electronics", Price=400,  RemainingStock=27  },
                new Product { Id=8,  Name="T-Shirt",      Category="Clothing",    Price=210,  RemainingStock=23  },
                new Product { Id=9,  Name="Jacket",       Category="Clothing",    Price=799,  RemainingStock=25  },
                new Product { Id=10, Name="Jogger Pants", Category="Clothing",    Price=499,  RemainingStock=30  },
                new Product { Id=11, Name="Notebook",     Category="School",      Price=35,   RemainingStock=70 },
                new Product { Id=12, Name="Ballpen",      Category="School",      Price=10,   RemainingStock=115 },
                new Product { Id=13, Name="Drill",        Category="Hardware",    Price=2399, RemainingStock=13  },
                new Product { Id=14, Name="Wrench Set",   Category="Hardware",    Price=799,  RemainingStock=14  },
                new Product { Id=15, Name="Tape Measure", Category="Hardware",    Price=110,  RemainingStock=23  },
            };

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== MAIN MENU ===");
                Console.WriteLine("[1] Shop  [2] Manage Cart  [3] Search");
                Console.WriteLine("[4] Filter by Category  [5] Order History  [6] Exit");
                Console.Write("Choose: ");

                switch (Console.ReadLine())
                {
                    case "1": Shop(products);           break;
                    case "2": ManageCart(products);     break;
                    case "3": Search(products);         break;
                    case "4": FilterCategory(products); break;
                    case "5": ShowHistory();            break;
                    case "6": running = false; Console.WriteLine("Goodbye!"); break;
                    default:  Console.WriteLine("Invalid option."); break;
                }
                Console.Write("\nEnter product ID: ");
                int id;
                if (!int.TryParse(Console.ReadLine(), out id) || id < 1 || id > product.Length)
                {
                    Console.WriteLine("Invalid product number!");
                    continue;
                }

                 Product selected = product[id - 1];

                if (selected.RemainingStock == 0)
                {
                    Console.WriteLine("Out of stock!");
                    continue;
                }

                Console.Write("Enter quantity: ");
                int qty;
                if (!int.TryParse(Console.ReadLine(), out qty) || qty <= 0)
                {
                    Console.WriteLine("Invalid quantity!");
                    continue;
                }

                if (qty > selected.RemainingStock)
                {
                    Console.WriteLine("Not enough stock available.");
                    continue;
                }

                bool found = false;

                for (int i = 0; i < cartCount; i++)
                {
                    if (cartIds[i] == id)
                    {
                        cartQty[i] += qty;
                        cartSub[i] = product[id - 1].GetItemTotal(cartQty[i]);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    if (cartCount >= cartIds.Length)
                    {
                        Console.WriteLine("Cart is full.");
                        continue;
                    }

                    cartIds[cartCount] = id;
                    cartQty[cartCount] = qty;
                    cartSub[cartCount] = selected.GetItemTotal(qty);
                    cartCount++;
                }

                selected.RemainingStock -= qty;

                Console.WriteLine("Item added to cart!");

                Console.Write("Add more? (Y/N): ");
                choice = Console.ReadLine().ToUpper();

            } while (choice == "Y");

            double grandTotal = 0;

            Console.WriteLine("\n***** RECEIPT *****");
            Console.WriteLine("Item            Quantity   Subtotal");

            for (int i = 0; i < cartCount; i++)
            {
                string name = product[cartIds[i] - 1].Name;
                Console.WriteLine($"{name,-15} {cartQty[i],-10} {cartSub[i],-10}");
                grandTotal += cartSub[i];
            }

            Console.WriteLine($"\nGrand Total: {grandTotal}");

            double discount = 0;

            if (grandTotal >= 5000)
            {
                discount = grandTotal * 0.10;
                Console.WriteLine($"Discount (10%): {discount}");
            }

            double finalTotal = grandTotal - discount;

            Console.WriteLine($"Final Total: {finalTotal}");

            Console.WriteLine("\n<<<<< UPDATED STOCK >>>>>");
            Console.WriteLine("ID    Name            Price      Stock");

            for (int i = 0; i < product.Length; i++)
            {
                product[i].DisplayProduct();
            }
        }
    }
}
