using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldOwin.Host {

    class Program {

        static void Main(string[] args) {

            using (IDisposable server = WebApp.Start<Startup>()) {

                Console.WriteLine("Started...");
                Console.ReadKey();
            }
        }
    }
}
