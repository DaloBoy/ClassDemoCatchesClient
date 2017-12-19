using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoCatchesClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RestClient client = new RestClient();
            client.Start();

            Console.ReadLine();
        }
    }
}
