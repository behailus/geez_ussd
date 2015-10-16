using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Geez.Business;
using Geez.Data;
using Ussd.Api;

namespace Ussd.Gateway
{
    public class Gate
    {
        public UssdResponseMessage GetResponse(UssdRequestMessage request)
        {
            new Helper().SaveRequest(request);//Make it async
            string message = request.USSDRequestString.Trim();
            string menuItems = "";
            var response = new UssdResponseMessage();
            if (!request.response && "*" + request.USSDServiceCode + "#" == message)
            {
                menuItems = new Review().SelectService();
                new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.ROOT.ToString(), MenuId = 0, LoggedMenu = 0 });
                response = new UssdResponseMessage { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
            }
            else if (request.response)
            {
                //The user was on some other state, check state, check menu and process response
                var lastStateObj = new Logger().LastState(request.TransactionId);
                var lastState = lastStateObj.State.TrimEnd(' ');
                var respon = request.USSDRequestString;
                try
                {
                    if (lastState == State.ROOT.ToString())
                    {
                        menuItems = new Review().MainMenu(Convert.ToInt32(respon));
                        new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.TRUNK.ToString(), MenuId = 0, LoggedMenu = 0 });
                        response = new UssdResponseMessage { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
                    }
                    else if (lastState == State.TRUNK.ToString())
                    {
                        var review = new Review();
                        int id = Convert.ToInt32(respon);
                        var method = review.GetMethodForMenu(id);
                        var methodInfo = typeof(Review).GetMethod(method);
                        menuItems = methodInfo.Invoke(review, new object[] { }).ToString();
                        new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.BRANCH.ToString(), MenuId = id, LoggedMenu = 0 });
                        response = new UssdResponseMessage { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
                    }
                    else if (lastState == State.BRANCH.ToString())
                    {
                        #region Branch
                        int selected = Convert.ToInt32(respon);
                        var review = new Review();
                        switch (lastStateObj.MenuId)
                        {
                            case 1://the response is a place from top ten
                                var serviceProvider = review.TopPlaces(10)[selected - 1];
                                new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.STEM.ToString(), MenuId = serviceProvider.Id, LoggedMenu = 1 });
                                menuItems = review.ProceedWithOption() + serviceProvider.OrganizationName;
                                response = new UssdResponseMessage { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
                                break;
                            case 2://the response is an event
                                var eventt = review.IqTodaysEvents()[selected - 1];
                                new Logger().Log(new TransactionLog()
                                {
                                    TransactionId = request.TransactionId,
                                    State = State.LEAF.ToString(),
                                    MenuId = eventt.Id,
                                    LoggedMenu = 2
                                });
                                menuItems = "Today at " + eventt.ServiceProvider.OrganizationName + ":\n Located: " +
                                            eventt.ServiceProvider.Address.Details + "\n Details: " +
                                            eventt.Details + ", " + eventt.ServiceProvider.Details;
                                response = new UssdResponseMessage { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                break;
                            case 3://the response is a category
                                var category = new Helper().GetCategories()[selected - 1];
                                new Logger().Log(new TransactionLog()
                                {
                                    TransactionId = request.TransactionId,
                                    State = State.STEM.ToString(),
                                    MenuId = category.Id,
                                    LoggedMenu = 3
                                });
                                menuItems = review.ServiceProvidersInCategory(category.Id);
                                response = new UssdResponseMessage { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
                                break;
                            case 4://the response is a service
                                var service = new Helper().GetServices()[selected - 1];
                                new Logger().Log(new TransactionLog()
                                {
                                    TransactionId = request.TransactionId,
                                    State = State.STEM.ToString(),
                                    MenuId = service.Id,
                                    LoggedMenu = 4
                                });
                                menuItems = review.ServiceProviderByService(service.Id);
                                response = new UssdResponseMessage { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
                                break;
                            case 5://the response is a region/from address
                                var region = new Helper().GetRegions()[selected - 1];
                                var address = new Helper().GetAddressByRegion(region);
                                new Logger().Log(new TransactionLog()
                                {
                                    TransactionId = request.TransactionId,
                                    State = State.STEM.ToString(),
                                    MenuId = address.Id,
                                    LoggedMenu = 5
                                });
                                menuItems = review.GetServiceProviderByRegion(region);
                                response = new UssdResponseMessage { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
                                break;
                            default://invalid selection
                                menuItems = "Invalid choice. Please select only from the menu.";
                                response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                break;
                        }
                        #endregion
                    }
                    else if (lastState == State.STEM.ToString())
                    {
                        int selected = Convert.ToInt32(respon);
                        var review = new Review();
                        var serviceProvider = new ServiceProvider();
                        switch (lastStateObj.LoggedMenu)
                        {
                            case 1://I have service provider
                                #region first line
                                switch (selected)
                                {
                                    case 1://Receive rating for the service provider
                                        new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.LEAF.ToString(), MenuId = lastStateObj.MenuId, LoggedMenu = 10 });
                                        menuItems = review.GetRatings();
                                        response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
                                        break;
                                    case 2://Receive phone number of a friend--specify format only to 0911734365
                                        new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.LEAF.ToString(), MenuId = lastStateObj.MenuId, LoggedMenu = 11 });
                                        menuItems = "Please put the phone number of your friend in the format '09########'";
                                        response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
                                        break;
                                    case 3://Show events and end
                                        new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.LEAF.ToString(), MenuId = 0, LoggedMenu = 0 });
                                        menuItems = review.TodaysEventsAt((int)lastStateObj.MenuId);
                                        response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                        break;
                                    case 4://display location and end
                                        new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.LEAF.ToString(), MenuId = 0, LoggedMenu = 0 });
                                        menuItems = review.DetailServiceProvider((int)lastStateObj.MenuId);
                                        response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                        break;
                                    default://Show error
                                        menuItems = "Invalid choice. Please select only from the menu.";
                                        response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                        break;
                                }
                                #endregion
                                break;
                            //case 2://I have event but this can't happen
                            //break;
                            case 3://I have category, log service provider to follow NO1
                                serviceProvider =
                                    review.ServiceProvidersByCategory((int)lastStateObj.MenuId)[selected - 1];
                                new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.STEM.ToString(), MenuId = serviceProvider.Id, LoggedMenu = 30 });
                                menuItems = review.ProceedWithOption() + serviceProvider.OrganizationName;
                                response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                break;
                            case 4://I have service, log service provider to follow NO1
                                serviceProvider =
                                    new Helper().ServiceProviderWithService((int)lastStateObj.MenuId)[selected - 1];
                                new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.STEM.ToString(), MenuId = serviceProvider.Id, LoggedMenu = 30 });
                                menuItems = review.ProceedWithOption() + serviceProvider.OrganizationName;
                                response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                break;
                            case 5://I have address, log service provider to follow NO1
                                serviceProvider =
                                    review.ServiceProvidersIn(lastStateObj.MenuId)[selected - 1];
                                new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.STEM.ToString(), MenuId = serviceProvider.Id, LoggedMenu = 30 });
                                menuItems = review.ProceedWithOption() + serviceProvider.OrganizationName;
                                response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                break;
                            case 30://coming back from 3,4 and 5
                                #region final line
                                switch (selected)
                                {
                                    case 1://Receive rating for the service provider
                                        new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.LEAF.ToString(), MenuId = lastStateObj.MenuId, LoggedMenu = 10 });
                                        menuItems = review.GetRatings();
                                        response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
                                        break;
                                    case 2://Receive phone number of a friend--specify format only to 0911734365
                                        new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.LEAF.ToString(), MenuId = lastStateObj.MenuId, LoggedMenu = 11 });
                                        menuItems = "Please put the phone number of your friend in the format '09########'";
                                        response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "request", USSDResponseString = menuItems };
                                        break;
                                    case 3://Show events and end
                                        new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.LEAF.ToString(), MenuId = 0, LoggedMenu = 0 });
                                        menuItems = review.TodaysEventsAt((int)lastStateObj.MenuId);
                                        response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                        break;
                                    case 4://display location and end
                                        new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.LEAF.ToString(), MenuId = 0, LoggedMenu = 0 });
                                        menuItems = review.DetailServiceProvider((int)lastStateObj.MenuId);
                                        response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                        break;
                                    default://Show error
                                        menuItems = "Invalid choice. Please select only from the menu.";
                                        response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                        break;
                                }
                                #endregion
                                break;
                            default:
                                menuItems = "Invalid choice. Please select only from the menu.";
                                response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                break;
                        }
                    }
                    else if (lastState == State.LEAF.ToString())
                    {
                        var review = new Review();
                        var serviceProvider = new ServiceProvider();
                        switch (lastStateObj.LoggedMenu)
                        {
                            case 10://rating is coming
                                int selected = Convert.ToInt32(respon);
                                new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.LEAF.ToString(), MenuId = 0, LoggedMenu = 0 });
                                menuItems = review.SaveRating(selected, (int)lastStateObj.MenuId);
                                response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                break;
                            case 11://phone number is coming
                                new Logger().Log(new TransactionLog() { TransactionId = request.TransactionId, State = State.LEAF.ToString(), MenuId = 0, LoggedMenu = 0 });
                                menuItems = review.RecommendToFriend(request.MSISDN, respon, (int)lastStateObj.MenuId);
                                response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                break;
                            default:
                                menuItems = "Invalid choice. Please select only from the menu.";
                                response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                                break;
                        }
                    }

                }
                catch
                {
                    menuItems = "Invalid choice. Please select only from the menu.";
                    response = new UssdResponseMessage() { TransactionId = request.TransactionId, TransactionTime = DateTime.UtcNow.ToString("o"), action = "end", USSDResponseString = menuItems };
                }



            }
            new Helper().SaveResponse(response);//Make it async
            return response;
        }
    }
}