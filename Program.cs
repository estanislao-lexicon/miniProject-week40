using System;
using System.Collections.Generic;
using System.Linq;

namespace productList
{
    static class Run
        {
            public static bool Execute = true;
        }
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> products = new List<Item>();
            Run.Execute = true;
            Menu(products);
        }
            
        static void Menu(List<Item> products)
        {
            // Entering any letter, should restart the process
            while(Run.Execute)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("To enter a new product - follow the instructions");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("To print the list of all prducts, enter 'P' | To quit, enter 'Q'");
                Console.ForegroundColor = ConsoleColor.Gray;
                
                Console.Write("Please enter the product category: ");
                string category = InputTextAnalizer(products, Console.ReadLine());
                if (!Run.Execute) return;

                category = Capitalize(category);
                                
                Console.Write("Please enter the product name: ");
                string name = InputTextAnalizer(products, Console.ReadLine());                
                if (!Run.Execute) return;

                name = Capitalize(name);                

                double price = 0;
                while(true)
                {
                    Console.Write("Please enter the product price: ");
                    string priceString = InputTextAnalizer(products, Console.ReadLine());
                    if (!Run.Execute) return;

                    if(double.TryParse(priceString, out price))
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter a number\n");
                        Console.ForegroundColor = ConsoleColor.Gray;                                                             
                    }
                }   
                
                
                products.Add(new Item(category, name, price));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The product was succesfully added!");
                Console.WriteLine("----------------------------------\n");
            } 
        }

        static void PrintProductList(List<Item> products)
        {
            if(!products.Any())
            {
                Console.WriteLine("No products in the list. Please add a product and try again.\n");                
                return;
            }

            var productsSorted = products.OrderBy(product => product.Price).ToList();

            Console.WriteLine("\n----------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Category".PadRight(20) + "Name".PadRight(20) + "Price".PadRight(20));
            
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach(Item product in productsSorted)    
            {
                product.Print();
            }
            double totalPrice = products.Sum(product => product.Price);
            Console.WriteLine("\n ".PadRight(21) + "Total amount:".PadRight(20) + totalPrice.ToString().PadRight(20));      
            Console.WriteLine("----------------------------------\n");

            // Console.ForegroundColor = ConsoleColor.DarkBlue;
            // Console.WriteLine("To enter a new product, enter 'E' | To search for a product, enter 'S' | To quit, enter 'Q'");
            // Console.ForegroundColor = ConsoleColor.Gray;

            // string input = Console.ReadLine();
            // InputTextAnalizer(products, input);            
        }            
        

        static string InputTextAnalizer(List<Item> products, string inputText)
        {            
            string text = inputText.ToUpper();
            switch (text)
            {
                case "Q":
                    Run.Execute = false;
                    break;
                case "P":
                    PrintProductList(products);
                    break;
                case "S":
                    SearchProduct(products);
                    break;
                default:
                    return inputText;
            }

            return inputText;
        }    

        static void SearchProduct(List<Item> products)
        {
            // Should return all list, with search match in color!
            Console.Write("Enter product name: ");
            string textToSearch = Console.ReadLine()?.ToLower();

            var foundProducts = products.Where(product => product.Name.ToLower() == textToSearch).ToList();

            Console.WriteLine("\n----------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Category".PadRight(20) + "Name".PadRight(20) + "Price".PadRight(20));
            Console.ForegroundColor = ConsoleColor.Gray;

            if (foundProducts.Any())
            {
                foreach (Item product in foundProducts)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    product.Print();
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            else
            {
                Console.WriteLine("No product found with that name.");
            }
            
            Console.WriteLine("----------------------------------\n");
        }

        static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }    
    }

    class Item
    {
        public Item(string category, string name, double price)
        {
            Category = category;
            Name = name;
            Price = price;
        }

        public void Print()
        {
            Console.WriteLine(Category.PadRight(20) + Name.PadRight(20) + Price.ToString().PadRight(20));
        }

        public string Category { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
