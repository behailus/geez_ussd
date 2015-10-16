using System;
using System.Web;
using System.IO;
namespace Ussd.Api
{
    public abstract class UssdHttpHandler : IHttpHandler
    {

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string message = ProcessRequest(context.Request);
            onMessage(message);
        }

        public string ProcessRequest(HttpRequest request)
        {
            StreamReader reader = new StreamReader(request.InputStream);
            string messageContent = reader.ReadToEnd();//This is the request XML from the USSD Gateway
            //UssdRequestMessage message = GenerateObject(messageContent);//Map the attributes on to the object
            return messageContent;
        }

        protected abstract void onMessage(string message);

    }
}
