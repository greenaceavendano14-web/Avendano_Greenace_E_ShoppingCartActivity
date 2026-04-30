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
        public double GetItemTotal(int qty) => Price * qty;
    }
    class Cart
    {
        CartItem[] items = new CartItem[20];
        int count = 0;

        public int Count => count;

        public CartItem GetItem(int index) => items[index];

        public bool AddItem(int productId, int qty, double subtotal)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].ProductId == productId)
                {
                    items[i].Quantity += qty;
                    items[i].Subtotal += subtotal;
                    return true;
                }
            }
            if (count >= items.Length) return false;
            items[count] = new CartItem { ProductId = productId, Quantity = qty, Subtotal = subtotal };
            count++;
            return true;
        }

        public void RemoveItem(int index)
        {
            for (int i = index; i < count - 1; i++)
                items[i] = items[i + 1];
            items[count - 1] = null;
            count--;
        }

        public void UpdateItem(int index, int newQty, double newSubtotal)
        {
            items[index].Quantity = newQty;
            items[index].Subtotal = newSubtotal;
        }

        public void Clear()
        {
            for (int i = 0; i < count; i++) items[i] = null;
            count = 0;
        }

        public double GetGrandTotal()
        {
            double total = 0;
            for (int i = 0; i < count; i++) total += items[i].Subtotal;
            return total;
        }
   class OrderHistory
    {
        int[]    receipts = new int[100];
        double[] totals   = new double[100];
        string[] dates    = new string[100];
        int count = 0;

        public void AddOrder(int receiptNo, double total, string date)
        {
            receipts[count] = receiptNo;
            totals[count]   = total;
            dates[count]    = date;
            count++;
        }

        public void Display()
        {
            if (count == 0) { Console.WriteLine("No orders yet."); return; }
            Console.WriteLine("\n=== ORDER HISTORY ===");
            for (int i = 0; i < count; i++)
                Console.WriteLine($"Receipt #{receipts[i].ToString("D4")} | {dates[i]} | PHP {totals[i]}");
        }
    }
    class Program
     {
        Product[]    products;
        Cart         cart;
        OrderHistory history;
        int          receiptCounter;

        public Program()
        {
            products = new Product[]
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
            cart           = new Cart();
            history        = new OrderHistory();
            receiptCounter = 1;
        }
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
            }
        }
        void Shop()
        {
            while (true)
            {
                Console.WriteLine("\n***** STORE MENU *****");
                Console.WriteLine($"{"ID",-5} {"Name",-15} {"Category",-15} {"Price",-10} {"Stock",-10}");
                foreach (var item in products) item.DisplayProduct();

                Console.Write("\nEnter product ID (0 to go back): ");
                if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid input."); continue; }
                if (id == 0) break;
                if (id < 1 || id > products.Length) { Console.WriteLine("Invalid product ID."); continue; }

                Product p = products[id - 1];
                if (p.RemainingStock == 0) { Console.WriteLine("Out of stock!"); continue; }

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0) { Console.WriteLine("Invalid quantity."); continue; }
                if (qty > p.RemainingStock) { Console.WriteLine($"Only {p.RemainingStock} left."); continue; }

                if (!cart.AddItem(id, qty, p.GetItemTotal(qty))) { Console.WriteLine("Cart is full."); continue; }

                p.RemainingStock -= qty;
                Console.WriteLine("Item added to cart!");

                if (YesNo("Add more items? (Y/N): ") == "N") break;
            }
        }

        void ManageCart()
        {
            while (true)
            {
                Console.WriteLine("\n=== CART MANAGEMENT ===");
                Console.WriteLine("[1] View  [2] Remove  [3] Update Qty  [4] Clear  [5] Checkout  [6] Back");
                Console.Write("Choose: ");
                switch (Console.ReadLine())
                {
                    case "1": ViewCart();   break;
                    case "2": RemoveItem(); break;
                    case "3": UpdateQty();  break;
                    case "4": ClearCart();  break;
                    case "5":
                        if (cart.Count == 0) Console.WriteLine("Cart is empty.");
                        else { Checkout(); return; }
                        break;
                    case "6": return;
                    default:  Console.WriteLine("Invalid option."); break;
                }
            }
        }
         void ViewCart()
        {
            if (cart.Count == 0) { Console.WriteLine("Cart is empty."); return; }
            Console.WriteLine($"\n{"#",-5} {"Item",-15} {"Qty",-10} {"Subtotal",-10}");
            for (int i = 0; i < cart.Count; i++)
            {
                CartItem item = cart.GetItem(i);
                Console.WriteLine($"{i+1,-5} {products[item.ProductId-1].Name,-15} {item.Quantity,-10} PHP {item.Subtotal,-10}");
            }
        }

        void RemoveItem()
        {
            ViewCart();
            if (cart.Count == 0) return;

            Console.Write("Item number to remove (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 0 || n > cart.Count) { Console.WriteLine("Invalid."); return; }
            if (n == 0) return;

            int idx = n - 1;
            CartItem item = cart.GetItem(idx);
            products[item.ProductId - 1].RemainingStock += item.Quantity;
            Console.WriteLine($"{products[item.ProductId-1].Name} removed.");
            cart.RemoveItem(idx);
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
