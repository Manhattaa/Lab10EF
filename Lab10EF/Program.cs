using Lab10EF.Data;
using Lab10EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10EF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Creating a very basic menu!
            Console.WriteLine("1. List all customers in the database");
            Console.WriteLine("2. Add a new customer to the database");

            Console.WriteLine("What will it be: ");
            string menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":

                    // Here we are to create an instance of NorthContext.

                    using (var context = new NorthContext())
                    {
                        // Asks the user if they want the list to be in Ascending or Descending order
                        Console.Clear();
                        Console.WriteLine("List [1]: Ascending or [2]: Descending?");

                        int userChoice = int.Parse(Console.ReadLine());

                        if (userChoice == 1)
                        {
                            // here we're creating an anonymous object that by default is ordered in Ascending order; the company names are being targeted.

                            Console.Clear();
                            var customers = context.Customers
                            .Include(c => c.Orders)
                            .OrderBy(c => c.CompanyName)
                            .ToList();

                            DisplayCustomers(customers);

                            Console.Write("Select customer number to view more info: ");
                            userChoice = int.Parse(Console.ReadLine());

                            DisplayAllCustomerInfo(customers, userChoice);
                        }

                        // here we're creating an anonymous object that is ordered in Descending order; the company names are being targeted.
                        if (userChoice == 2)
                        {
                            Console.Clear();
                            var customers = context.Customers
                            .Include(c => c.Orders)
                            .OrderByDescending(c => c.CompanyName)
                            .ToList();

                            DisplayCustomers(customers);

                            Console.Write("Select customer number to view more info: ");
                            userChoice = int.Parse(Console.ReadLine());

                            DisplayAllCustomerInfo(customers, userChoice);
                        }
                    }
                    break;
                case "2":
                    // Prompt the user to enter customer details
                    Console.Clear();
                    Console.WriteLine("Enter customer details:");

                    Console.Write("Company Name: ");
                    string companyName = Console.ReadLine();

                    Console.Write("Contact Name: ");
                    string contactName = Console.ReadLine();

                    Console.Write("Contact Title: ");
                    string contactTitle = Console.ReadLine();

                    Console.Write("Address: ");
                    string address = Console.ReadLine();

                    Console.Write("City: ");
                    string city = Console.ReadLine();

                    Console.Write("Region: ");
                    string region = Console.ReadLine();

                    Console.Write("Postal Code: ");
                    string postalCode = Console.ReadLine();

                    Console.Write("Country: ");
                    string country = Console.ReadLine();

                    Console.Write("Phone: ");
                    string phone = Console.ReadLine();

                    Console.Write("Fax: ");
                    string fax = Console.ReadLine();

                    using (NorthContext context = new NorthContext())
                    {
                        int customerCount = context.Customers.Count() + 1;
                        // Create a new Customer object with the input details
                        var newCustomer = new Customer
                        {
                            // Use the correct primary key property name
                            CompanyName = companyName,
                            CustomerId = customerCount.ToString("D4"), //D4 makes it so we get 4 decimals within our primary key. 
                            ContactName = contactName,
                            ContactTitle = contactTitle,
                            Address = address,
                            City = city,
                            Region = region,
                            PostalCode = postalCode,
                            Country = country,
                            Phone = phone,
                            Fax = fax
                        };

                        // Add the new customer to the context
                        context.Customers.Add(newCustomer);

                        // Save changes to the database
                        context.SaveChanges();

                        Console.WriteLine("New customer added to the database.");
                    }
                    return;


            }
        }
        //Listing all customers
        static void DisplayCustomers(IEnumerable<Customer> customers)
        {
            for (var i = 0; i < customers.Count(); i++)
            {
                Console.WriteLine($"{i}. {customers.ElementAt(i).CompanyName}\n\n" +
                    $"Country / Region: {customers.ElementAt(i).Country}/{customers.ElementAt(i).Region}\n" +
                    $"Phone Number: {customers.ElementAt(i).Phone}\n" +
                    $"Amount of Orders: {customers.ElementAt(i).Orders.Count()}");
                Console.WriteLine("\n");
            }
        }
        // Displaying all info in regards of a specific customer
        static void DisplayAllCustomerInfo(IEnumerable<Customer> customers, int search)
        {
            bool customerFound = false;
            for (var i = 0; i < customers.Count(); i++)
            {
                if (search == i)
                {
                    Console.Clear();
                    Console.WriteLine($"{i}. {customers.ElementAt(i).CompanyName}\n\n" +
                        $"Address: {customers.ElementAt(i).Address} -  {customers.ElementAt(i).PostalCode} {customers.ElementAt(i).City}\n" +
                        $"Country / Region: {customers.ElementAt(i).Country} / {customers.ElementAt(i).Region}\n" +
                        $"Contact Person: {customers.ElementAt(i).ContactName} - {customers.ElementAt(i).ContactTitle}\n" +
                        $"Phone / FAX: {customers.ElementAt(i).Phone} / {customers.ElementAt(i).Fax}\n" +
                        $"Amount of Orders: {customers.ElementAt(i).Orders.Count()}");

                    customerFound = true;
                }
            }

            if (!customerFound)
            {
                Console.Clear();
                Console.WriteLine("The customer in question does not exist, perhaps consider adding them to the database?");
            }
        }
    }
}