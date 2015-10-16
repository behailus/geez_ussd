using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geez.Data;
using Ussd.Api;

namespace Geez.Business
{
    public class Helper
    {
        private readonly GeezEntities _context = new GeezEntities();
        
        public Address GetAddress(long id)
        {
            return _context.Address.FirstOrDefault(a => a.Id == id);
        }

        public List<string> GetRegions()
        {
            var regions = new List<string>();
            regions = (from r in _context.Address
                       select r.Region).Distinct().ToList();
            return regions;
        }

        public List<Category> GetCategories()
        {
            return _context.Category.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Category.FirstOrDefault(c => c.Id == id);
        }

        public bool SaveRequest(Request request)
        {
            try
            {
                _context.Request.Add(request);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveResponse(Response response)
        {
            try
            {
                _context.Response.Add(response);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveRequest(UssdRequestMessage _request)
        {
            try
            {
                var request=new Request()
                    {
                        TransactionId = _request.TransactionId,
                        MSISDN = _request.MSISDN,
                        TransactionTime = DateTime.Now,
                        USSDRequestString = _request.USSDRequestString,
                        USSDServiceCode = _request.USSDServiceCode,
                        Response = _request.response,
                        ChargingFlag = _request.ChargingFlag,
                        ChargeCode = _request.ChargeCode
                    };
                _context.Request.Add(request);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveResponse(UssdResponseMessage  _response)
        {
            try
            {
                var response=new Response()
                    {
                        TransactionId = _response.TransactionId,
                        USSDResponseString = _response.USSDResponseString,
                        TransactionTime = _response.TransactionTime,
                        Action = _response.action,
                        ResponseCode = _response.ResponseCode
                    };
                _context.Response.Add(response);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool SaveFault(Fault fault)
        {
            try
            {
                _context.Fault.Add(fault);
                _context.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public List<Rating> GetRatings()
        {
            return _context.Rating.ToList();
        }

        public Rating GetRating(int id)
        {
            return _context.Rating.FirstOrDefault(r => r.Id == id);
        }

        public bool SaveCategory(Category category)
        {
            try
            {
                _context.Category.Add(category);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Application> GetApplication()
        {
            return _context.Application.ToList();
        }

        public List<Service> GetServices()
        {
            return _context.Service.ToList();
        }

        public List<ServiceProvider> ServiceProviderWithService(int serviceId)
        {
            var service = _context.Service.FirstOrDefault(s => s.Id == serviceId);
            return service.ServiceProvider.ToList();
        }

        public Address GetAddressByRegion(string region)
        {
            return _context.Address.FirstOrDefault(a => a.Region == region);
        }

        public List<Message> GetLatestMessages()
        {
            return _context.Message.OrderByDescending(m => m.Id).Take(100).ToList();
        }
        public List<User> GetLatestUsers()
        {
            return _context.User.OrderByDescending(u => u.Id).Take(100).ToList();
        }
        public List<Menu> GetMenus()
        {
            return _context.Menu.ToList();
        }

        public List<Recommendation> GetRecommendations()
        {
            return _context.Recommendation.OrderByDescending(r=>r.Id).Take(100).ToList();
        }

        public List<Setting> GetSettings()
        {
            return _context.Setting.ToList();
        }

        public bool CutThings()
        {
            try
            {
                foreach (var application in _context.Application)
                {
                    _context.Application.Remove(application);
                }
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
