using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace CloudServices.PubNubChat
{
    public class Program
    {
        static void Main(string[] args)
        {
            var myIp = GetExternalIp();
            // Start the HTML5 Pubnub client
            Process.Start(@"..\..\..\PubNub-HTML5-Client.html");

            System.Threading.Thread.Sleep(2000);

            Pubnub pubnub = new Pubnub(
                "pub-c-4331b990-8629-4f47-9669-51f0e2ee9c9d",               // PUBLISH_KEY
                "sub-c-bfd2fbba-0428-11e3-91de-02ee2ddab7fe",               // SUBSCRIBE_KEY
                "sec-c-NDExYTBlYjUtM2QyYS00YTJiLWExNDItM2Y5NDQ2ZjA1N2Uy",   // SECRET_KEY
                "",                                                         // CIPHER_KEY
                true                                                        // SSL_ON?
            );
            string channel = "ninja-channel";

            // Publish a sample message to Pubnub
            pubnub.Publish<string>(channel, "", DisplayReturnMessage);

            // Show PubNub server time
            pubnub.Time<string>(DisplayReturnMessage);
            //Console.WriteLine("Server Time: " + serverTime.ToString());

            // Subscribe for receiving messages (in a background task to avoid blocking)
            System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(
                () =>
                pubnub.Subscribe<string>(
                    channel,
                    DisplayReturnMessage,
                    DisplayConnectStatusMessage
                )
            );
            t.Start();

            // Read messages from the console and publish them to Pubnub
            while (true)
            {
                Console.Write("Enter a message to be sent to Pubnub: ");
                string msg = Console.ReadLine();
                pubnub.Publish<string>(channel, myIp + " : " + msg, DisplayReturnMessage);
                Console.WriteLine("Message {0} sent.", msg);
            }
        }


        /// <summary>
        /// Callback method captures the response in JSON string format for all operations
        /// </summary>
        /// <param name="result"></param>
        static void DisplayReturnMessage(string result)
        {
            Console.WriteLine("");
        }

        /// <summary>
        /// Callback method to provide the connect status of Subscribe call
        /// </summary>
        /// <param name="result"></param>
        static void DisplayConnectStatusMessage(string result)
        {
            Console.WriteLine(result);
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
    }
}
