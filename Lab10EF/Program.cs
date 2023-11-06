using Lab10EF.Data;
using Lab10EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
// Fady Hatta (.NET23)
namespace Lab10EF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. List all customers in the database");
            Console.WriteLine("2. Add a new customer to the database");

            Console.WriteLine("What will it be: ");
            string menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    using (var context = new NorthContext())
                    {
                        Console.Clear();
                        Console.WriteLine("List [1]: Ascending or [2]: Descending?");

                        int userChoice = int.Parse(Console.ReadLine());

                        if (userChoice == 1)
                        {
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
                        var newCustomer = new Customer
                        {
                            CompanyName = companyName,
                            CustomerId = customerCount.ToString("D4"),
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

                        context.Customers.Add(newCustomer);
                        context.SaveChanges();

                        Console.WriteLine("New customer added to the database.");
                    }
                    return;
            }
        }

        static void DisplayCustomers(IEnumerable<Customer> customers)
        {
            for (var i = 0; i < customers.Count(); i++)
            {
                var customer = customers.ElementAt(i);
                int shippedOrdersCount = customer.Orders.Count(o => o.ShippedDate != null);
                int pendingOrdersCount = customer.Orders.Count(o => o.ShippedDate == null);

                Console.WriteLine($"{i}. {customer.CompanyName}\n\n" +
                    $"Country / Region: {customer.Country}/{customer.Region}\n" +
                    $"Phone Number: {customer.Phone}\n" +
                    $"Shipped Orders: {shippedOrdersCount}\n" +
                    $"Pending Orders: {pendingOrdersCount}\n");
                Console.WriteLine("\n");
            }
        }

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
