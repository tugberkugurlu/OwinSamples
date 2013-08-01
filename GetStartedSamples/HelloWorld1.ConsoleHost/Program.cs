using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld1.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>())
            {
                Console.WriteLine("Started!");
                Console.ReadKey();
                Console.WriteLine("Stoping...");
            }
        }
    }
}
