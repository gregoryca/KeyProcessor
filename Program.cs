using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.VisualBasic;
using static System.Net.WebRequestMethods;

namespace KeyProcessor
{
    class Program
    {
        //Creating the string varaible to be used for the Response.
        public static string str;
        public static void Main(string[] args)
        {
            #region Login
            getCookie();
            #endregion

            #region Log
            string Path = @"C:\Users\gcraane\Desktop\cookieHeaders.txt";
            #endregion
        }

        #region Cookie_Creator
        public static void getCookie()
        {
            //HTTP webrequest address
            WebRequest request = WebRequest.Create("http://10.230.60.170:6060/Webcontrols/xmlsql");
            var response = (HttpWebResponse)request.GetResponse();

            //Getting the response, and substracting the data i need to an string
            HttpWebResponse TheRespone = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(TheRespone.GetResponseStream()).ReadToEnd();
            string cookieHeader = TheRespone.Headers[HttpResponseHeader.SetCookie];
            //str = cookieHeader.Substring(0, 32);
            str = cookieHeader.Substring(11, 32);
            Console.WriteLine(str);
            LogIn();

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            //HttpWebRequest Post = WebRequest.Create(uri);
            //Post.Method = "POST";
            //Post.ContentType = "application/x-www-form-urlencoded";
            //Post.Headers.Add("Cookie", str);

            //var postresponse = (HttpWebResponse)Post.GetResponse();

            //HttpResponseMessage ThepostRespone = (HttpResponseMessage)Post.GetResponse();
            //var postresponseString = new StreamReader(postresponse.GetResponseStream()).ReadToEnd();
            //Console.WriteLine(postresponse);
            //Console.WriteLine(postresponseString);
            //Console.ReadKey();
        }
        #endregion

        #region Login
        public static void LogIn()
        {
            //Creating the login data variable
            var postData = "Cookie=" + str;
            var byteArray = Encoding.ASCII.GetBytes(postData);

            //Writing the response to the command prompt
            Console.WriteLine();

            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create("http://10.230.60.170:6060/Webcontrols/j_security_check?j_username=root&j_password=Pr0t3cti0n&JSESSIONID=");

            //WebRequest request = WebRequest.Create("http://10.230.60.170:6060/Webcontrols/j_security_check?j_username=root&j_password=Pr0t3cti0n");

            //WebRequest request = WebRequest.CreateHttp("http://10.230.1.70/form/");

            // Set the Method property of the request to POST.  
            request.Method = "POST";
            request.Timeout = 60000;
            request.Headers.Add("Cookie", str);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            Console.WriteLine(responseFromServer);
            // Clean up the streams.  
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        #endregion
    }
}