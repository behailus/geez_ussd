using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ussd.Api;
namespace Geez.Business
{
    public class LifeStyleRequestHandler:IRequestHandler
    {
        public UssdResponseMessage ProcessRequest(UssdRequestMessage request)
        {
            string assembName = "ServiceTwo.dll";//assembly
            string clasName = "Geez.ServiceTwo.Handler";
            string methName = "ReqstHandler";
            Assembly assembly = Assembly.LoadFile(@"D:\Projects\Geez\"+assembName);
            Type type = assembly.GetType(clasName);
            var serviceInstance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod(methName);
            string result = methodInfo.Invoke(serviceInstance, new object[]{""}).ToString();
            //string result = type.InvokeMember(methName, BindingFlags.InvokeMethod|BindingFlags.Instance|BindingFlags.Public,null, serviceInstance, null).ToString();
            return new UssdResponseMessage() {TransactionId = "0",action = "end",TransactionTime = DateTime.Now.ToString(),USSDResponseString =result };
        }
    }
}
