using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Geez.Data;

namespace Geez.Business
{
    public class ServiceProviders
    {
        private GeezEntities context;

        public ServiceProviders()
        {
            context = new GeezEntities();
        }

        public List<ServiceProvider> GetAllServiceProviders()
        {
            return context.ServiceProvider.ToList();
        }

        public ServiceProvider GetServiceProviderById(int id)
        {
            return context.ServiceProvider.FirstOrDefault(s => s.Id == id);
        }


        public bool AddServiceProvider(ServiceProvider provider)
        {
            try
            {
                var reference = context.ServiceProvider.OrderByDescending(s => s.Id).FirstOrDefault();
                provider.Id = reference!=null?reference.Id+1:1;
                context.ServiceProvider.Add(provider);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddEvent(Event eEvent)
        {
            try
            {
                var reference = context.Event.OrderByDescending(e => e.Id).FirstOrDefault();
                eEvent.Id = reference != null ? reference.Id + 1 : 1;
                context.Event.Add(eEvent);
                context.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public List<Event> GetAllEvents()
        {
            return context.Event.OrderBy(e => e.StartDate).ToList();
        }

        public IQueryable<Event> GetUpcomingEvents()
        {
            return context.Event.Where(e => EntityFunctions.TruncateTime(e.StartDate) >= EntityFunctions.TruncateTime(DateTime.Now)).OrderBy(e => e.StartDate);
        }

        public IQueryable<Event> GetTodaysEvents()
        {
            return context.Event.Where(e => EntityFunctions.TruncateTime(e.StartDate) <= EntityFunctions.TruncateTime(DateTime.Now)).Where(e => EntityFunctions.TruncateTime(e.EndDate) >= EntityFunctions.TruncateTime(DateTime.Now)).OrderBy(e => e.StartDate);
        }

        public IQueryable<Event> GetActiveEventsAt(int serviceProviderId)
        {
            return
                context.Event.Where(e => e.ServiceProviderId == serviceProviderId)
                       .Where(e => EntityFunctions.TruncateTime(e.StartDate) <= EntityFunctions.TruncateTime(DateTime.Now))
                       .Where(e => EntityFunctions.TruncateTime(e.EndDate) >= EntityFunctions.TruncateTime(DateTime.Now)).OrderBy(e => e.StartDate)
                       ;
        }
        public IQueryable<Event> GetUpcomingEventsAt(int serviceProviderId)
        {
            return
                context.Event.Where(e => e.ServiceProviderId == serviceProviderId)
                       .Where(e => EntityFunctions.TruncateTime(e.StartDate) >= EntityFunctions.TruncateTime(DateTime.Now))
                       .Where(e => EntityFunctions.TruncateTime(e.EndDate) >= EntityFunctions.TruncateTime(DateTime.Now)).OrderBy(e => e.StartDate)
                       ;
        }

        public int GetThisMonthEvents()
        {
            return
                context.Event.Where(e => e.StartDate.Year == DateTime.Now.Year)
                       .Count(e => e.StartDate.Month == DateTime.Now.Month);
        }
    }
}
