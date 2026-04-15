using System;
namespace shoppingcart
{
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;

        public void DisplayProduct()
        {
            Console.WriteLine($"{Id, -5} {Name, -15} {Price, -10} {RemainingStock, -10}");
        }
        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }
         }
    class Program
    {
        static void Main(string[] args)
        {
            Product[] product = new Product[] {
            new Product
            {
                Id=1,
                Name="Mouse",
                Price=350,
                RemainingStock=50
            },
            new Product
            {
                Id=2,
                Name="Keyboard",
                Price=800,
                RemainingStock=30
            },
            new Product
            {
                Id=3,
                Name="Headset",
                Price=1200,
                RemainingStock=20
            },
            new Product
            {
                Id=4,
                Name="Flash Drive",
                Price=500,
                RemainingStock=15
            },
            new Product
            {
                Id=5,
                Name="Monitor",
                Price=6500,
                RemainingStock=10
            }
            };

            int[] cartIds = new int[10];
            int[] cartQty = new int[10];
            double[] cartSub = new double[10];
            int cartCount = 0;

            
            string choice = "Y";

            do
            {
                Console.WriteLine("\n----- STORE MENU -----");
                Console.WriteLine("ID    Name            Price      Stock");

                for (int i = 0; i < product.Length; i++)
                {
                    product[i].DisplayProduct();
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

            Console.WriteLine("\n----- RECEIPT -----");
            Console.WriteLine("Item            Quantity   Subtotal");

            for (int i = 0; i < cartCount; i++)
            {
                string name = product[cartIds[i] - 1].Name;
                Console.WriteLine($"{name,-15} {cartQty[i],-10} {cartSub[i],-10}");
                grandTotal += cartSub[i];
            }
