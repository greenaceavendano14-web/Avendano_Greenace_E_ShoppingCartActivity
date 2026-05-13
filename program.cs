using System;

namespace shoppingcart
{
    class Product
    {
        private int id;
        private string name;
        private string category;
        private double price;
        private int remainingStock;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0) throw new ArgumentException("Price cannot be negative.");
                price = value;
            }
        }

        public int RemainingStock
        {
            get { return remainingStock; }
            set
            {
                if (value < 0) throw new ArgumentException("Stock cannot be negative.");
                remainingStock = value;
            }
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"{id,-5} {name,-15} {category,-15} {price,-10} {remainingStock,-10}");
        }

        public double GetItemTotal(int qty) => price * qty;
    }

    class CartItem
    {
        private int productId;
        private int quantity;
        private double subtotal;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value < 0) throw new ArgumentException("Quantity cannot be negative.");
                quantity = value;
            }
        }

        public double Subtotal
        {
            get { return subtotal; }
            set
            {
                if (value < 0) throw new ArgumentException("Subtotal cannot be negative.");
                subtotal = value;
            }
        }
    }

    class Cart
    {
        private CartItem[] items = new CartItem[20];
        private int count = 0;

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
    }

    class OrderHistory
    {
        private int[]    receipts = new int[100];
        private double[] totals   = new double[100];
        private string[] dates    = new string[100];
        private int count = 0;

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

    class Shop
    {
        private Product[]    products;
        private Cart         cart;
        private OrderHistory history;
        private int          receiptCounter;

        public Shop()
        {
            products = new Product[]
            {
                new Product { Id=1,  Name="Mouse",        Category="Electronics", Price=400,  RemainingStock=40  },
                new Product { Id=2,  Name="Keyboard",     Category="Electronics", Price=750,  RemainingStock=25  },
                new Product { Id=3,  Name="Headset",      Category="Electronics", Price=1000, RemainingStock=15  },
                new Product { Id=4,  Name="Flash Drive",  Category="Electronics", Price=350,  RemainingStock=25  },
                new Product { Id=5,  Name="Monitor",      Category="Electronics", Price=4500, RemainingStock=15  },
                new Product { Id=6,  Name="Webcam",       Category="Electronics", Price=1200, RemainingStock=20  },
                new Product { Id=7,  Name="USB Hub",      Category="Electronics", Price=450,  RemainingStock=30  },
                new Product { Id=8,  Name="T-Shirt",      Category="Clothing",    Price=250,  RemainingStock=30  },
                new Product { Id=9,  Name="Jacket",       Category="Clothing",    Price=899,  RemainingStock=20  },
                new Product { Id=10, Name="Jogger Pants", Category="Clothing",    Price=599,  RemainingStock=25  },
                new Product { Id=11, Name="Notebook",     Category="School",      Price=50,   RemainingStock=100 },
                new Product { Id=12, Name="Ballpen",      Category="School",      Price=15,   RemainingStock=200 },
                new Product { Id=13, Name="Drill",        Category="Hardware",    Price=2500, RemainingStock=10  },
                new Product { Id=14, Name="Wrench Set",   Category="Hardware",    Price=850,  RemainingStock=15  },
                new Product { Id=15, Name="Tape Measure", Category="Hardware",    Price=120,  RemainingStock=40  },
            };
            cart           = new Cart();
            history        = new OrderHistory();
            receiptCounter = 1;
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== MAIN MENU ===");
                Console.WriteLine("[1] Shop             [2] Manage Cart \n[3] Search           [4] Filter by Category  \n[5] Order History    [6] Exit");
                Console.Write("Choose: ");
                switch (Console.ReadLine())
                {
                    case "1": AddToCart();            break;
                    case "2": ManageCart();      break;
                    case "3": Search();          break;
                    case "4": FilterCategory();  break;
                    case "5": history.Display(); break;
                    case "6": running = false; Console.WriteLine("Goodbye!"); break;
                    default:  Console.WriteLine("Invalid option."); break;
                }
            }
        }

        private void AddToCart()
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

        private void ManageCart()
        {
            while (true)
            {
                Console.WriteLine("\n=== CART MANAGEMENT ===");
                Console.WriteLine("[1] View          [2] Remove  \n[3] Update Qty    [4] Clear  \n[5] Checkout      [6] Back");
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

        private void ViewCart()
        {
            if (cart.Count == 0) { Console.WriteLine("Cart is empty."); return; }
            Console.WriteLine($"\n{"#",-5} {"Item",-15} {"Qty",-10} {"Subtotal",-10}");
            for (int i = 0; i < cart.Count; i++)
            {
                CartItem item = cart.GetItem(i);
                Console.WriteLine($"{i+1,-5} {products[item.ProductId-1].Name,-15} {item.Quantity,-10} PHP {item.Subtotal,-10}");
            }
        }

        private void RemoveItem()
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

        private void UpdateQty()
        {
            ViewCart();
            if (cart.Count == 0) return;

            Console.Write("Item number to update (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 0 || n > cart.Count) { Console.WriteLine("Invalid."); return; }
            if (n == 0) return;

            int idx = n - 1;
            CartItem item  = cart.GetItem(idx);
            Product prod   = products[item.ProductId - 1];
            int available  = prod.RemainingStock + item.Quantity;

            Console.Write($"New quantity (available: {available}): ");
            if (!int.TryParse(Console.ReadLine(), out int newQty) || newQty <= 0 || newQty > available)
            {
                Console.WriteLine("Invalid quantity. No changes made.");
                return;
            }

            prod.RemainingStock = available - newQty;
            cart.UpdateItem(idx, newQty, prod.GetItemTotal(newQty));
            Console.WriteLine("Quantity updated.");
        }

        private void ClearCart()
        {
            if (cart.Count == 0) { Console.WriteLine("Cart is already empty."); return; }
            if (YesNo("Clear cart? (Y/N): ") == "N") return;

            for (int i = 0; i < cart.Count; i++)
            {
                CartItem item = cart.GetItem(i);
                products[item.ProductId - 1].RemainingStock += item.Quantity;
            }
            cart.Clear();
            Console.WriteLine("Cart cleared.");
        }

        private void Checkout()
        {
            double grand     = cart.GetGrandTotal();
            double discount  = grand >= 5000 ? grand * 0.10 : 0;
            double finalTotal = grand - discount;

            double payment;
            while (true)
            {
                Console.Write($"Final Total: PHP {finalTotal}\nEnter payment: PHP ");
                if (!double.TryParse(Console.ReadLine(), out payment) || payment < finalTotal)
                { Console.WriteLine("Insufficient or invalid payment."); continue; }
                break;
            }

            string receiptNo = receiptCounter.ToString("D4");
            string now       = DateTime.Now.ToString("MMMM dd, yyyy h:mm tt");

            Console.WriteLine($"\n{'=',44}");
            Console.WriteLine($"  RECEIPT No: {receiptNo}     Date: {now}");
            Console.WriteLine($"{'=',44}");
            Console.WriteLine($"{"Item",-15} {"Qty",-10} {"Subtotal",-10}");
            Console.WriteLine($"{'-',44}");
            for (int i = 0; i < cart.Count; i++)
            {
                CartItem item = cart.GetItem(i);
                Console.WriteLine($"{products[item.ProductId-1].Name,-15} {item.Quantity,-10} PHP {item.Subtotal,-10}");
            }
            Console.WriteLine($"{'-',44}");
            Console.WriteLine($"Grand Total:  PHP {grand}");
            if (discount > 0) Console.WriteLine($"Discount 10%: PHP {discount}");
            Console.WriteLine($"Final Total:  PHP {finalTotal}");
            Console.WriteLine($"Payment:      PHP {payment}");
            Console.WriteLine($"Change:       PHP {payment - finalTotal}");
            Console.WriteLine($"{'=',44}");

            history.AddOrder(receiptCounter, finalTotal, now);
            receiptCounter++;
            cart.Clear();

            Console.WriteLine("\n=== LOW STOCK ALERT ===");
            bool anyLow = false;
            for (int i = 0; i < products.Length; i++)
                if (products[i].RemainingStock <= 5) { Console.WriteLine($"WARNING: {products[i].Name} - only {products[i].RemainingStock} left!"); anyLow = true; }
            if (!anyLow) Console.WriteLine("All products have sufficient stock.");

            Console.WriteLine("\n=== UPDATED STOCK ===");
            Console.WriteLine($"{"ID",-5} {"Name",-15} {"Category",-15} {"Price",-10} {"Stock",-10}");
            foreach (var item in products) item.DisplayProduct();
        }

        private void Search()
        {
            Console.Write("Search product name: ");
            string key = Console.ReadLine().ToLower();
            bool found = false;
            Console.WriteLine($"\n{"ID",-5} {"Name",-15} {"Category",-15} {"Price",-10} {"Stock",-10}");
            foreach (var item in products)
                if (item.Name.ToLower().Contains(key)) { item.DisplayProduct(); found = true; }
            if (!found) Console.WriteLine("No products found.");
        }

        private void FilterCategory()
        {
            string[] cats = { "Electronics", "School", "Clothing", "Hardware" };
            Console.WriteLine("\n[1] Electronics  [2] School  [3] Clothing  [4] Hardware");
            Console.Write("Choose: ");
            if (!int.TryParse(Console.ReadLine(), out int c) || c < 1 || c > 4) { Console.WriteLine("Invalid."); return; }
            string cat = cats[c - 1];
            Console.WriteLine($"\n--- {cat} ---");
            Console.WriteLine($"{"ID",-5} {"Name",-15} {"Category",-15} {"Price",-10} {"Stock",-10}");
            bool found = false;
            foreach (var item in products)
                if (item.Category == cat) { item.DisplayProduct(); found = true; }
            if (!found) Console.WriteLine("No products found.");
        }

        private string YesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim().ToUpper();
                if (input == "Y" || input == "N") return input;
                Console.WriteLine("Invalid. Enter Y or N only.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
           new Shop().Run();
        }
    }
}
