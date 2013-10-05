using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using IronMQ;
using IronMQ.Data;

namespace CloudServices.IronMQChatter
{
    public class Program
    {
        static void Main(string[] args)
        {
            var myIp = GetExternalIp();
            Client client = new Client(
                "520a3b7de68cce0009000005",
                "QgYqrjF8oIVFv_G8UsZOQcLjDBA");
            var queue = client.Queue("GigaSimpleChat");
            Console.WriteLine("Enter messages to be sent to the IronMQ server:");
            while (true)
            {
                Message msg = queue.Get();
                if (msg != null)
                {
                    Console.WriteLine(msg.Body);
                    //Thread.Sleep(100);
                    //queue.DeleteMessage(msg);
                }

                Thread.Sleep(100);
                while (Console.KeyAvailable)
                {
                    string message = Console.ReadLine();
                    queue.Push(myIp + " : " + message);
                    Console.WriteLine("Message sent to the IronMQ server.");
                }
            }
        }

        private static string GetExternalIp()
        {
            try
            {
                // Use a web page that displays the IP of the request.  In this case,
                // I use network-tools.com.  This page has been around for years
                // and is always up when I have tried it.  You could use others or
                // your own.  
                WebRequest myRequest = WebRequest.Create("http://network-tools.com");
                // Send request, get response, and parse out the IP address on the page. 
                using (WebResponse res = myRequest.GetResponse())
                {
                    using (Stream s = res.GetResponseStream())
                    using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
                    {
                        string html = sr.ReadToEnd();
                        Regex regex = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                        string ipString = regex.Match(html).Value;
                        return ipString;
                    }
                }
            }
            catch (Exception ex)
            {
                return "No IP";
            }
        }

        private static string GetInternalIP()
        {
            var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            var ip = (
                      from addr in hostEntry.AddressList
                      where addr.AddressFamily.ToString() == "InterNetwork"
                      select addr.ToString()).FirstOrDefault();
            return ip;
        }
    }
}
