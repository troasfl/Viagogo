using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewAssignment
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var emailCampaignService = new EmailCampaignService();

            var events = new List<Event>
            {
                new Event { Name = "Phantom of the Opera", City = "New York" },
                new Event { Name = "Metallica", City = "Los Angeles" },
                new Event { Name = "Metallica", City = "New York" },
                new Event { Name = "Metallica", City = "Boston" },
                new Event { Name = "LadyGaGa", City = "New York" },
                new Event { Name = "LadyGaGa", City = "Boston" },
                new Event { Name = "LadyGaGa", City = "Chicago" },
                new Event { Name = "LadyGaGa", City = "San Francisco" },
                new Event { Name = "LadyGaGa", City = "Washington" }
            };

            var mrFake = new Customer { Name = "Mr. Fake", City = "New York" };
            DisplayScenariosForCustomer(mrFake, events, emailCampaignService);

            var johnSmith = new Customer { Name = "John Smith" };
            DisplayScenariosForCustomer(johnSmith, events, emailCampaignService);


            var customers = new List<Customer>
            {
                new Customer { Name = "Nathan", City = "New York" },
                new Customer { Name = "Bob", City = "Boston" },
                new Customer { Name = "Cindy", City = "Chicago" },
                new Customer { Name = "Lisa", City = "Los Angeles" }
            };

            foreach (var customer in customers) DisplayScenariosForCustomer(customer, events, emailCampaignService);

            Console.ReadLine();
        }

        private static void DisplayScenariosForCustomer(Customer customer, IReadOnlyCollection<Event> events,
            EmailCampaignService emailCampaignService)
        {
            Console.Out.WriteLine($"\n - Upcoming Events for {customer.Name} in {customer.City}");
            var cityEvents = emailCampaignService.GetCityEvents(customer, events);
            emailCampaignService.SendEmail(customer, cityEvents);

            var price = 200;
            Console.Out.WriteLine(
                $"\n - Upcoming Events Less or Equal To {price} for {customer.Name} in {customer.City}");
            var priceEvents = emailCampaignService.GetCityEvents(customer, events);
            emailCampaignService.SendEmail(customer, priceEvents, price);

            Console.Out.WriteLine($"\n - Top 5 Closest Upcoming Events for {customer.Name} in {customer.City}");
            var closestEvents = emailCampaignService.GetClosestEvents(customer, events).ToList();
            emailCampaignService.SendEmail(customer, closestEvents);

            Console.Out.WriteLine(
                $"\n - Top 5 Closest Upcoming Events Less or Equal To {price} for {customer.Name} in {customer.City}");
            emailCampaignService.SendEmail(customer, closestEvents, price);

            price = 150;
            Console.Out.WriteLine(
                $"\n - Top 5 Closest Upcoming Events Less or Equal To {price} for {customer.Name} in {customer.City}");
            emailCampaignService.SendEmail(customer, closestEvents, price);

            Console.Out.WriteLine("\n________________________________________________________________________________");
        }
    }
}