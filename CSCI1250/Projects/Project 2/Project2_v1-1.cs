using MyDLL;

namespace Shamazon;

class Shamazon
{
    /// <summary>
    /// Method takes an integer List and populates it with 5 random and distinct integers between 0 and 9.
    /// 
    /// Integers in the List operate in a way similar to Product IDs
    /// in that they are tied to the index of the product in the PRODUCTS array
    /// </summary>
    /// <param name="availableProducts"></param>
    static void InitializeProducts(List<int> availableProducts)
    {
        Random rNum = new Random();

        int prodNum;

        while(availableProducts.Count() < 5)
        {
            prodNum = rNum.Next(0, 10);

            if(!availableProducts.Contains(prodNum))
            {
                availableProducts.Add(prodNum);
            }
        }
    }

    /// <summary>
    /// Iterates through the MENU_OPTIONS array and displays the numbered option to the user
    /// Uses a string[] for Menu Options for easier customization in the future (e.g. adding an option or removing one)
    /// </summary>
    static void DisplayMenu()
    {
        string[] MENU_OPTIONS = new string[]
        {
            "Browse Products",
            "Add To Cart",
            "View Cart",
            "Complete Purchase",
            "Exit"
        };

        for (int i = 0; i < MENU_OPTIONS.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {MENU_OPTIONS[i]}");
        }
    }


    /// <summary>
    /// Method that prompts a user to enter a choice for one of the Menu options or Items available.
    /// Performs input validation to ensure the user inputs an integer, further validation is
    /// performed by a do-while loop to ensure the value is also between 1 and 5 (inclusive)
    /// Displays error message to user if input is invalid.
    /// </summary>
    /// <returns>Integer value between 1 and 5 (inclusive)</returns>
    static int GetChoice()
    {
        int choice;

        do
        {
            choice = CSCI1250.Validate<int>("Select an option (1/2/3/4/5): ", "Invalid Input. Select an option (1/2/3/4/5): ");
            if(choice < 1 || choice > 5)
            {
                Console.Write("Invalid Input. ");
            }
        }while(choice < 1 || choice > 5);

        return choice;
    }

    /// <summary>
    /// Takes a list representing the products that are available for purchase
    /// Displays the shelf items and their price and corresponding number choice
    /// </summary>
    /// <param name="availableProducts"></param>
    static void DisplayProducts(List<int> availableProducts, string[] products, double[] prices)
    {
        Console.WriteLine("\nAvailable Products: ");

        for(int i = 0; i < availableProducts.Count(); i++)
        {
            Console.WriteLine($"{i + 1}. {products[availableProducts[i]]} - {prices[availableProducts[i]]:C}");
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Takes the available product integer list, and the array containing product names. 
    /// Prompts user to choose an item from the list of products available (there is a call to DisplayProducts() in the Main method)
    /// Subtracts 1 from user choice due to 0 indexing
    /// Returns the corresponding integer chosen from the available products list to be added to the user's shopping cart List
    /// </summary>
    /// <param name="availableProducts">Products in stock/for sale</param>
    /// <param name="shelf">Array containing product names</param>
    static int AddToCart(List<int> availableProducts, string[] shelf)
    {
        int itemChoice;

        itemChoice = GetChoice() - 1;

        Console.WriteLine($"{shelf[availableProducts[itemChoice]]} has been added to your cart\n");

        return availableProducts[itemChoice];
    }

    /// <summary>
    /// Takes the List representing the user's shopping cart, the product name array, and the product price array
    /// Displays each item in the list and it's associated price formatted as currency
    /// </summary>
    /// <param name="userCart">User's shopping cart containing 'product IDs'</param>
    /// <param name="shelf">Array containing product names</param>
    /// <param name="prices">Array containing product prices</param>
    static void ViewCart(List<int> userCart, string[] shelf, double[] prices)
    {
        Console.WriteLine("\nYour Shopping Cart: ");

        foreach(int item in userCart)
        {
            Console.WriteLine($"- {shelf[item]} - {prices[item]:C}");
        }

        CalcTotal(userCart, prices);
    }

    /// <summary>
    /// Method works as above however instead of displaying each product and price
    /// Iterates through the list and calculates a total of all items in the cart
    /// Displays the total formatted as currency
    /// </summary>
    /// <param name="userCart">User's shopping cart containing 'product IDs'</param>
    /// <param name="prices">Array containing product prices</param>
    static void CalcTotal(List<int> userCart, double[] prices)
    {
        double total = 0.0;

        foreach(int item in userCart)
        {
            total += prices[item];
        }

        Console.WriteLine($"\nTotal cost of your purchase: {total:C}");
    }

    static void Main(string[] args)
    {
        //List of potential shelf to be sold
        string[] PRODUCT_NAMES = new string[]
        {
        "Laptop",
        "Headphones",
        "Phone",
        "GPU",
        "Monitor",
        "Couch",
        "Shoes",
        "Purse",
        "Sheets",
        "Keyboard"
        };

        //Prices of above mentioned shelf
        double[] PRODUCT_PRICES = new double[]
        {
        2499.99,
        399.99,
        1299.99,
        3999.99,
        435.99,
        2250.00,
        275.00,
        450.00,
        85.99,
        179.99
        };

        List<int> stock = new List<int>();
        List<int> shoppingCart = new List<int>();

        Console.WriteLine("Welcome to the online shopping app!\n");

        InitializeProducts(stock); //Fill the stock List with distinct shelf

        int menuChoice;

        //Loop to display menu and perform relevant actions to user
        //When user selects the option to complete their purchase menuChoice is set to 5 to exit loop without prompting user for input again
        do
        {
            DisplayMenu();

            menuChoice = GetChoice();

            switch (menuChoice)
            {
                case 1:
                    DisplayProducts(stock, PRODUCT_NAMES, PRODUCT_PRICES);
                    break;
                case 2:
                    DisplayProducts(stock, PRODUCT_NAMES, PRODUCT_PRICES);  
                    int item = AddToCart(stock, PRODUCT_NAMES);             
                    shoppingCart.Add(item);                                 
                    break;
                case 3:
                    ViewCart(shoppingCart, PRODUCT_NAMES, PRODUCT_PRICES);
                    break;
                case 4:
                    CalcTotal(shoppingCart, PRODUCT_PRICES);
                    Console.WriteLine("Purchase completed. Thank you!");
                    menuChoice = 5;                                         
                    break;
                case 5:
                    Console.WriteLine("Goodbye!");
                    break;
            }
        } while (menuChoice != 5);
        
    }
}
