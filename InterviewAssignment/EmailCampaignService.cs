using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewAssignment
{
    public class EmailCampaignService
    {
        private readonly Dictionary<string, int> _distanceDictionary = new Dictionary<string, int>();

        // You do not need to know how these methods work
        private void AddToEmail(Customer c, Event e, int? price = null)
        {
            var distance = GetDistance(c.City, e.City);
            Console.Out.WriteLine($"{c.Name}: {e.Name} in {e.City}" +
                                  (distance > 0 ? $" ({distance} miles away)" : "") +
                                  (price.HasValue ? $" for ${price}" : ""));
        }

        private int GetPrice(Event e)
        {
            return (AlphebiticalDistance(e.City, "") + AlphebiticalDistance(e.Name, "")) / 10;
        }

        private int GetDistance(string fromCity, string toCity)
        {
            try
            {
                // lets cache some result here to improve performance
                var key = $"{fromCity}-{toCity}";
                var alreadyComputed = _distanceDictionary.TryGetValue(key, out var distance);
                if (!alreadyComputed)
                {
                    distance = AlphebiticalDistance(fromCity, toCity);
                    _distanceDictionary[key] = distance;
                }

                return distance;
            }
            catch (Exception e)
            {
                return GetDistance(fromCity, toCity);
            }
        }

        private int AlphebiticalDistance(string s, string t)
        {
            var result = 0;
            var i = 0;
            for (i = 0; i < Math.Min(s.Length, t.Length); i++)
                // Console.Out.WriteLine($"loop 1 i={i} {s.Length} {t.Length}");
                result += Math.Abs(s[i] - t[i]);
            for (; i < Math.Max(s.Length, t.Length); i++)
                // Console.Out.WriteLine($"loop 2 i={i} {s.Length} {t.Length}");
                result += s.Length > t.Length ? s[i] : t[i];
            return result;
        }

        public void SendEmail(Customer customer, IEnumerable<Event> upComingCustomerEvents, int? defaultPrice = null)
        {
            foreach (var upComingCustomerEvent in upComingCustomerEvents)
            {
                var price = GetPrice(upComingCustomerEvent);
                if (defaultPrice.HasValue)
                {
                    // assumption here is that we want prices that are cheap
                    if (price <= defaultPrice.Value) AddToEmail(customer, upComingCustomerEvent, price);
                }
                else
                {
                    AddToEmail(customer, upComingCustomerEvent, price);
                }
            }
        }

        public IEnumerable<Event> GetCityEvents(Customer customer, IEnumerable<Event> upComingEvents)
        {
            // assumption here is if you are like John Smith and you decided not to set your location before using our API or Interface
            // we cannot get you any events happening
            return string.IsNullOrEmpty(customer.City)
                ? Enumerable.Empty<Event>()
                : upComingEvents.Where(e => e.City.Contains(customer.City));
        }

        public IEnumerable<Event> GetClosestEvents(Customer customer, IEnumerable<Event> upComingEvents,
            int numberOfEvents = 5)
        {
            if (string.IsNullOrEmpty(customer.City)) return Enumerable.Empty<Event>();
            var dictionary = new Dictionary<string, Tuple<Event, int>>();
            foreach (var upComingEvent in upComingEvents)
            {
                var distance = GetDistance(customer.City, upComingEvent.City);
                dictionary[$"{upComingEvent.Name}-{upComingEvent.City}"] =
                    new Tuple<Event, int>(upComingEvent, distance);
            }

            return dictionary.Values.OrderBy(x => x.Item2).Take(numberOfEvents).Select(x => x.Item1);
        }
    }
}