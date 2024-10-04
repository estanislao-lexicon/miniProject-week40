using System;
using System.Collections.Generic;
using System.Linq;

namespace productList
{
    static class Run
    {            
        public static bool Menu = true;
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> products = new List<Item>();                       
            Menu(products);
        }
            
        static void Menu(List<Item> products)
        {
            Run.Menu = true;

            while(Run.Menu)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("To enter a new product - follow the instructions | To quit, enter 'Q'");                                
                Console.ForegroundColor = ConsoleColor.Gray;
                
                string category, name;

                while(true)
                {
                    Console.Write("Please enter the product category: ");
                    category = Console.ReadLine();
                    if(HandleCommand(products, category))
                    {
                        if (!Run.Menu) return;
                        continue;
                    }
                    else
                    {
                        category = Capitalize(category);
                        break;
                    }
                }

                while(true)
                {                
                    Console.Write("Please enter the product name: ");
                    name = Console.ReadLine();
                    if(HandleCommand(products, name))
                    {
                        if (!Run.Menu) return;
                        continue;
                    }                    
                    else
                    {
                        name = Capitalize(name);
                        break;
                    }                
                }    

                double price = 0;
                while(true)
                {
                    Console.Write("Please enter the product price: ");
                    string priceString = Console.ReadLine();
                    if(double.TryParse(priceString, out price))
                    {
                        break;
                    }

                    if(HandleCommand(products, priceString))
                    {
                        if (!Run.Menu) return;
                        continue;
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

        static bool HandleCommand(List<Item> products, string inputText)
        {            
            string text = inputText.ToUpper();
            List<Item> productsSorted = products.OrderBy(product => product.Price).ToList();

            if (text == "P")
            {
                Menu(products);
                return true;  
            }
            else if (text == "S")
            {
                SearchProduct(products);
                return true;  
            }            
            else if (text == "Q")
            {
                Run.Menu = false; 
                PrintProductList(products);                 
                return true;  
            }

            return false;  
        }    

        static void WaitForCommand(List<Item> products)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("To enter a new product, enter 'P' | To search for a product, enter 'S' | To quit, enter 'Q'");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write("> ");
            string input = Console.ReadLine();
            if(input.ToUpper() != "Q")
            {
                HandleCommand(products, input);
            }            
        }

        static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }   

        static void PrintProductList(List<Item> products)
        {
            if(!products.Any())
            {
                Console.WriteLine("No products in the list\n");                                
            }
            else 
            {
                Console.WriteLine("\n----------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Category".PadRight(20) + "Name".PadRight(20) + "Price".PadRight(20));
                
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach(Item product in products)    
                {
                    product.Print();
                }
                double totalPrice = products.Sum(product => product.Price);
                Console.WriteLine("\n ".PadRight(21) + "Total amount:".PadRight(20) + totalPrice.ToString().PadRight(20));      
                Console.WriteLine("----------------------------------\n");
            }
            WaitForCommand(products);
        }    

        static void SearchProduct(List<Item> products)
        {
            // Should return all list, with search match in color!
            Console.Write("Enter product name: ");
            string textToSearch = Console.ReadLine()?.ToLower();
            textToSearch = Capitalize(textToSearch);

            bool foundProducts = products.Any(product => product.Name.Contains(textToSearch));

            Console.WriteLine("\n----------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Category".PadRight(20) + "Name".PadRight(20) + "Price".PadRight(20));
            Console.ForegroundColor = ConsoleColor.Gray;

            if (foundProducts == true)
            {
                foreach (Item product in products)
                {
                    if(textToSearch == product.Name)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        product.Print();
                    }   
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray; 
                        product.Print();
                    }
                }
            }
            else
            {
                Console.WriteLine("No product found with that name.");
            }
            
            Console.WriteLine("----------------------------------\n");

            WaitForCommand(products);
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
