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
