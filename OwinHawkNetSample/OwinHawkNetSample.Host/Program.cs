using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinHawkNetSample.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>())
            {
                Console.WriteLine("Started");
                Console.ReadLine();
                Console.WriteLine("Stopping");
            }
        }
    }
}
