using System;
using System.Collections.Generic;
using System.Formats.Asn1;
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
            List<Item> productsList = new List<Item>();                       
            Menu(productsList);
        }
            
        static void Menu(List<Item> productsList)
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
                    if(HandleCommand(productsList, category))
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
                    if(HandleCommand(productsList, name))
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

                    if(HandleCommand(productsList, priceString))
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
                                
                productsList.Add(new Item(category, name, price));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The product was succesfully added!");
                Console.WriteLine("----------------------------------\n");
            } 
        }

        static bool HandleCommand(List<Item> productsList, string commandInput)
        {            
            string commandToHandle = commandInput.ToUpper();
            List<Item> productsSorted = productsList.OrderBy(product => product.Price).ToList();
            
            if (commandToHandle == "Q")
            {
                Run.Menu = false; 
                PrintProductList(productsList);                 
                return true;  
            }
            else if(Run.Menu == true)
            {
                return false;
            }
            else if (commandToHandle == "P")
            {
                Menu(productsList);                
                return true;  
            }
            else if (commandToHandle == "S")
            {
                SearchProduct(productsList);
                return true;  
            }                        

            return false;  
        }    

        static void WaitForCommand(List<Item> productsList)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("To enter a new product, enter 'P' | To search for a product, enter 'S' | To quit, enter 'Q'");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write("> ");
            string newCommand = Console.ReadLine().ToUpper();
            if(newCommand != "Q")
            {
                HandleCommand(productsList, newCommand);
            }            
        }

        static string Capitalize(string textToCapitalize)
        {
            if (string.IsNullOrEmpty(textToCapitalize)) return textToCapitalize;
            return char.ToUpper(textToCapitalize[0]) + textToCapitalize.Substring(1).ToLower();
        }   

        static void PrintProductList(List<Item> productsList)
        {
            if(!productsList.Any())
            {
                Console.WriteLine("No products in the list\n");                                
            }
            else 
            {
                Console.WriteLine("\n----------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Category".PadRight(20) + "Name".PadRight(20) + "Price".PadRight(20));
                
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach(Item product in productsList)    
                {
                    product.Print();
                }
                double totalPrice = productsList.Sum(product => product.Price);
                Console.WriteLine("\n ".PadRight(21) + "Total amount:".PadRight(20) + totalPrice.ToString().PadRight(20));      
                Console.WriteLine("----------------------------------\n");
            }
            WaitForCommand(productsList);
        }    

        static void SearchProduct(List<Item> productsList)
        {            
            Console.Write("Enter product name: ");
            string textToSearch = Capitalize(Console.ReadLine());            

            bool foundProducts = productsList.Any(product => product.Name.Contains(textToSearch));

            Console.WriteLine("\n----------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Category".PadRight(20) + "Name".PadRight(20) + "Price".PadRight(20));
            Console.ForegroundColor = ConsoleColor.Gray;

            if (foundProducts == true)
            {
                foreach (Item product in productsList)
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

            WaitForCommand(productsList);
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
