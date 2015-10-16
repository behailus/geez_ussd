using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Geez.Data;

namespace Geez.Business
{
    public class Review
    {
        private GeezEntities context;
        public Review()
        {
            context = new GeezEntities();
        }
        public List<ServiceProvider> TopPlaces(int number)
        {
            var topTen = (from ss in context.ServiceProviderRating
                          orderby ss.AverageRating descending
                          select ss.ServiceProvider).Take(number);

            return topTen.ToList();
        }

        public string MainMenu(int appId)
        {
            var menus = from m in context.Menu
                        where m.ParentId == 0
                        where m.Id!=0
                        where m.ApplicationId==appId
                        select m.Title;
            int no = 1;
            var builder = new StringBuilder("Please select from menu:©");
            if (menus.Any())
            {
                foreach (var menu in menus)
                {
                    builder.Append(no.ToString(CultureInfo.InvariantCulture) + ":" + menu + "©");
                    no++;
                }
                return builder.ToString();
            }
            else
            {
                return "There is no service configured for this choice.";
            }
        }

        public string TopTen()
        {
            int i = 1;
            var topTen = new Review().TopPlaces(10);
            StringBuilder builder = new StringBuilder();
            foreach (var single in topTen)
            {
                builder.Append(i.ToString(CultureInfo.InvariantCulture) + ":" + single.OrganizationName + "©");
                i++;
            }
            return builder.ToString();
        }

        public string CheckinToRandom()
        {
            int i = 20;
            var places = context.ServiceProvider.ToList();
            StringBuilder builder = new StringBuilder("Select a place:©");
            foreach (var serviceProvider in places)
            {
                builder.Append(i.ToString(CultureInfo.InvariantCulture) + ":" + serviceProvider.OrganizationName + "©");
                i++;
            }
            return builder.ToString();
        }

        public List<Event> IqTodaysEvents()
        {
            return new ServiceProviders().GetTodaysEvents().ToList();
        }

        public string ServiceProvidersInCategory(int categoryId)
        {
            var serviceProviders = context.ServiceProvider.Where(s => s.CategoryId == categoryId).ToList();
            var builder = new StringBuilder("Please Select One:©");
            int no = 1;
            foreach (var serviceProvider in serviceProviders)
            {
                builder.Append(no.ToString(CultureInfo.InvariantCulture)+":"+serviceProvider.OrganizationName + "©");
                no++;
            }
            return builder.ToString();
        }

        public List<ServiceProvider> ServiceProvidersByCategory(int categoryId)
        {
            return context.ServiceProvider.Where(s => s.CategoryId == categoryId).ToList();
        }
        public List<ServiceProvider> ServiceProvidersIn(long addressId)
        {
            var addressRegion = context.Address.FirstOrDefault(a => a.Id == addressId).Region;
            return context.ServiceProvider.Where(s => s.Address.Region == addressRegion).ToList();
        }
        public string ServiceProviderByService(int serviceId)
        {
            var serviceProviders = new Helper().ServiceProviderWithService(serviceId);
            var builder = new StringBuilder("Please Select One:©");
            int no = 1;
            foreach (var serviceProvider in serviceProviders)
            {
                builder.Append(no.ToString(CultureInfo.InvariantCulture) + ":" + serviceProvider.OrganizationName + "©");
                no++;
            }
            return builder.ToString();
        }

        public string TodaysEvents()
        {
            var events = new ServiceProviders().GetTodaysEvents();
            int no = 1;
            StringBuilder builder = new StringBuilder(DateTime.Now.Date.ToShortDateString()+ " Events©");
            foreach (var eevent in events)
            {
                builder.Append(no.ToString()+":"+eevent.Title+" At "+eevent.ServiceProvider.OrganizationName+"©");
                no++;
            }
            return builder.ToString();
        }

        public string BrowseByCategory()
        {
            var builder = new StringBuilder("Select Category:©");
            var categories = new Helper().GetCategories();
            int no = 1;
            foreach (var category in categories)
            {
                builder.Append(no.ToString() + ":" + category.Description + "©");
                no++;
            }

            return builder.ToString();
        }
        public string BrowseByService()
        {
            var builder = new StringBuilder("Select Service Type:©");
            var services = new Helper().GetServices();
            int no = 1;
            foreach (var service in services)
            {
                builder.Append(no.ToString() + ":" + service.ServiceName + "©");
                no++;
            }

            return builder.ToString();
        }

        public string BrowseByRegion()
        {
            var builder = new StringBuilder("Select Region:©");
            var vals = new Helper().GetRegions();
            int no = 1;
            foreach (var val in vals)
            {
                builder.Append(no.ToString() + ":" + val + "©");
                no++;
            }

            return builder.ToString();
        }

        public string GetMethodForMenu(int id)
        {
            var firstOrDefault = context.Menu.FirstOrDefault(m => m.Id == id);
            return firstOrDefault != null?firstOrDefault.MethodName:"Default";
        }

        public string SelectService()
        {
            var apps = context.Application.ToList();
            var builder = new StringBuilder("Please select service:©");
            foreach (var application in apps)
            {
                builder.Append(application.Id.ToString() + ":" + application.AssemblyName + "©");
            }
            return builder.ToString();
        }

        public string ProceedWithOption()
        {
            var builder = new StringBuilder("Please:©");
            builder.Append("1.Rate it!©");
            builder.Append("2.Recommend to a friend©");
            builder.Append("3.See upcoming events©");
            builder.Append("4.Go to ");
            return builder.ToString();
        }

        public string GetServiceProviderByRegion(string region)
        {
            var serviceProviders= context.ServiceProvider.Where(s => s.Address.Region == region).ToList();
            var builder = new StringBuilder("Please select one:©");
            int no = 1;
            foreach (var serviceProvider in serviceProviders)
            {
                builder.Append(no.ToString(CultureInfo.InvariantCulture) + ":" + serviceProvider.OrganizationName + "©");
                no++;
            }
            return builder.ToString();
        }

        public string GetRatings()
        {
            var ratings = new Helper().GetRatings();
            var builder = new StringBuilder("Please choose a rating:©");
            foreach (var rating in ratings)
            {
                builder.Append(rating.Id.ToString() + ":" + rating.Description + "©");
            }
            return builder.ToString();
        }

        public string SaveRating(int ratingId,int serviceProviderId)
        {
            try
            {
                var rating = new Helper().GetRating(ratingId);
                var previousRating =context.ServiceProviderRating.FirstOrDefault(s => s.ServiceProviderId == serviceProviderId);
                var lastRating = context.ServiceProviderRating.OrderByDescending(s => s.Id).FirstOrDefault();
                if (previousRating == null)
                {
                    var id = lastRating == null ? 1 : lastRating.Id + 1;
                    context.ServiceProviderRating.Add(new ServiceProviderRating()
                        {
                            Id = id,
                            AverageRating = rating.Value,
                            RatingCount = 1,
                            ServiceId = 1,
                            ServiceProviderId = serviceProviderId
                        });
                }
                else
                {
                    var averageRating = ((previousRating.AverageRating + rating.Value) / (previousRating.RatingCount + 1));
                    previousRating.AverageRating = averageRating;
                    previousRating.RatingCount++;
                }
                context.SaveChanges();
                return "Your rating is submitted.© Thank You for your time.";
            }
            catch
            {
                return "Error! Unable to save rating.";
            }
        }

        public string RecommendToFriend(string recommenderPhone,string recommendedPhone,int serviceProviderId)
        {
            try
            {
                if (ValidatePhone(recommendedPhone))
                {
                    context.Recommendation.Add(new Recommendation()
                        {
                            RecommendedBy = recommenderPhone,
                            RecommendedTo = recommendedPhone,
                            ServiceProviderId = serviceProviderId,
                            RecommendedOn = DateTime.Now
                        });
                    context.SaveChanges();
                    return "Thank you for your recommendation. It will be forwarded to your friend immediately.";
                }
                else
                {
                    return "Phone Number is not in correct format. \nPlease try again latter.";
                }
            }
            catch
            {
                return "Error! Please try again latter";
            }
        }

        private bool ValidatePhone(string recommendedPhone)
        {
            if (recommendedPhone.Length==10 & recommendedPhone.StartsWith("09"))
            {
                return recommendedPhone.ToCharArray().All(Char.IsDigit);
            }
            else
            {
                return false;
            }
        }

        public string TodaysEventsAt(int serviceProviderId)
        {
            var events = new ServiceProviders().GetUpcomingEventsAt(serviceProviderId);
            var serviceProviderName =
                context.ServiceProvider.FirstOrDefault(s => s.Id == serviceProviderId).OrganizationName;
            var builder = new StringBuilder("Upcoming events at " + serviceProviderName + "©");
            int no = 1;
            foreach (var @event in events)
            {
                builder.Append(no.ToString(CultureInfo.InvariantCulture) + ":" + @event.Title + "-" + @event.Details + "©");
                no++;
            }
            return builder.ToString();
        }

        public string DetailServiceProvider(int serviceProviderId)
        {
            var serviceProvider = context.ServiceProvider.FirstOrDefault(s => s.Id == serviceProviderId);
            return "Located in " + serviceProvider.Address.City + ", " + serviceProvider.Address.Street + ", " +
                   serviceProvider.Address.Details + ". \nMore info: " + serviceProvider.Details;
        }
    }
}
