using HawkNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OwinHawkNetSample.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentials = new HawkCredential
            {
                Id = "dh37fgj492je",
                Key = "werxhqb98rpaxn39848xrunpaw3489ruxnpa98w4rxn",
                Algorithm = "hmacsha256",
                User = "steve"
            };

            var request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:5000/api/cars");
            request.SignRequest(credentials);
            request.Method = "GET";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Console.WriteLine("Call made. Status Code " + response.StatusCode);
            }

            Console.ReadLine();
        }
    }
}
