using Microsoft.Owin.Hosting;
using System;

namespace OwinIoCContainerSample.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            StartOptions startOptions = new StartOptions("http://localhost:5000/") 
            {
            };

            using (WebApp.Start<Startup>(startOptions))
            {
                Console.WriteLine("Started!");
                Console.ReadLine();
                Console.WriteLine("Stopping...");
            }
        }
    }
}