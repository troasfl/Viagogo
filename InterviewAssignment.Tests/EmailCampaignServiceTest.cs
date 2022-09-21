using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace InterviewAssignment.Tests
{
    public class EmailCampaignServiceTest
    {
        private EmailCampaignService _emailCampaignService;
        private List<Event> _events;

        [SetUp]
        public void Setup()
        {
            _events = new List<Event>
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
            _emailCampaignService = new EmailCampaignService();
        }

        #region MrFakeTests

        [Test]
        public void GetUpcomingEventsForMrFakeInNewYork_ShouldReturn_ThreeEvents()
        {
            var expected = 3;
            var customer = new Customer { Name = "Mr. Fake", City = "New York" };
            var events = _emailCampaignService.GetCityEvents(customer, _events).ToList();
            var actual = events.Count;

            Assert.IsTrue(expected.Equals(actual));
        }

        [Test]
        public void Top5ClosestUpcomingEventsForMrFakeInNewYork_ShouldReturn_FiveEvents()
        {
            var expected = 5;
            var customer = new Customer { Name = "Mr. Fake", City = "New York" };
            var closestEvents = _emailCampaignService.GetClosestEvents(customer, _events).ToList();
            var actual = closestEvents.Count;

            Assert.IsTrue(expected.Equals(actual));
        }

        #endregion

        #region JohnSmithTests

        [Test]
        public void GetUpcomingEventsForJohnSmithWithNoCity_ShouldReturn_ZeroEvents()
        {
            var expected = 0;
            var customer = new Customer { Name = "John Smith" };
            var events = _emailCampaignService.GetCityEvents(customer, _events).ToList();
            var actual = events.Count;

            Assert.IsTrue(expected.Equals(actual));
        }

        [Test]
        public void Top5ClosestUpcomingEventsForJohnSmithWithNoCity_ShouldReturn_ZeroEvents()
        {
            var expected = 0;
            var customer = new Customer { Name = "John Smith" };
            var closestEvents = _emailCampaignService.GetClosestEvents(customer, _events).ToList();
            var actual = closestEvents.Count;

            Assert.IsTrue(expected.Equals(actual));
        }

        #endregion

        #region LisaTests

        [Test]
        public void GetUpcomingEventsForLisaInLosAngeles_ShouldReturn_OneEvent()
        {
            var expected = 1;
            var customer = new Customer { Name = "Lisa", City = "Los Angeles" };
            var events = _emailCampaignService.GetCityEvents(customer, _events).ToList();
            var actual = events.Count;

            Assert.IsTrue(expected.Equals(actual));
        }

        [Test]
        public void Top3ClosestUpcomingEventsForLisaInLosAngeles_ShouldReturn_ThreeEvents()
        {
            var expected = 3;
            var customer = new Customer { Name = "Lisa", City = "Los Angeles" };
            var closestEvents = _emailCampaignService.GetClosestEvents(customer, _events, 3).ToList();
            var actual = closestEvents.Count;

            Assert.IsTrue(expected.Equals(actual));
        }

        #endregion
    }
}