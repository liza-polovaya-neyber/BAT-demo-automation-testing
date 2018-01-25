using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Base Directory: " + AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine("Current Directory: " + Environment.CurrentDirectory);
            Console.ReadLine();
        }
    }
}
