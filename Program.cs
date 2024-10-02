using System.Security.Cryptography.X509Certificates;

namespace productList;

class Program
{
    static void Main(string[] args)
    {
        List<Item> products = new List<Item>();
        
        while(true)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.WriteLine("To enter a new product - follow the instructions");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine("To print the list of all prducts, enter 'P' | To enter a ne product, enter 'E' | To search for a product, enter 'S' | To quit, enter 'Q'");
            Console.ForegroundColor = ConsoleColor.Gray;
            
            System.Console.Write("Please enter the product category: ");
            string category = Console.ReadLine();
            category = char.ToUpper(category[0]) + category.Substring(1).ToLower();
            if(category == "Q")
            {
                break;
            }           

            System.Console.Write("Please enter the product name: ");
            string name = Console.ReadLine();
            name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
            if(name == "Q")
            {
                break;
            }         

            double price = 0;
            while(true)
            {
                System.Console.Write("Please enter the product price: ");
                string priceString = Console.ReadLine();                
                
                if(priceString == "Q" || priceString == "q")
                {
                    break;
                }
                else
                {
                    try {
                        price = Convert.ToDouble(priceString);
                        break;
                    }
                    catch (FormatException) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter a number\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }                     
            }
            products.Add(new Item(category, name, price));

            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("The product was succesfully added!");
            System.Console.WriteLine("----------------------------------\n");
        } 

        List<Item> productsSorted = products.OrderBy(product => product.Price).ToList();

        System.Console.WriteLine("\n----------------------------------");
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Category".PadRight(20) + "Name".PadRight(20) + "Price".PadRight(20));
        
        Console.ForegroundColor = ConsoleColor.Gray;
        foreach(Item product in productsSorted)    
        {
            product.Print();
        }
        double totalPrice = products.Sum(product => product.Price);
        System.Console.WriteLine("\n ".PadRight(21) + "Total amount:".PadRight(20) + totalPrice.ToString().PadRight(20));      
        System.Console.WriteLine("----------------------------------");
        
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
            System.Console.WriteLine(Category.PadRight(20) + Name.PadRight(20) + Price.ToString().PadRight(20));
        }

        public string Category { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
