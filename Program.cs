namespace productList;

class Program
{
    static void Main(string[] args)
    {
        while(true)
        {
            System.Console.WriteLine("To enter a new product - follow the steps | To quit enter 'Q'");
            System.Console.WriteLine("Please enter the product category: ");
            string category = Console.ReadLine();
            category = char.ToUpper(category[0]) + category.Substring(1);
            if(category == "Q")
            {
                break;
            }

            System.Console.WriteLine("Please enter the product name: ");
            string name = Console.ReadLine();

            System.Console.WriteLine("Please enter the product price: ");
            int price = Convert.ToInt32(Console.ReadLine());                

            List<Product> products = new List<Product>();
            products.Add(new Product(category, name, price));

            System.Console.WriteLine("The product was succesfully added!");
        }    
    }
    class Product
    {
        public Product(string category, string name, int price)
        {
            Category = category;
            Name = name;
            Price = price;
        }

        public void Print()
        {
            System.Console.WriteLine($"");
        }

        public string Category { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
