using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreetingsLib;

namespace UseGreetingsLibFW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Greetings greetings = new Greetings();
            var user = "Asif";

           Console.WriteLine($"Greetings: {greetings.SayHello(user)}");
            Console.ReadLine();
        }
    }
}
