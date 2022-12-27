using GreetingsLib;

using static System.Console;

Greetings greetings = new();
var user = "Asif";

WriteLine($"Greetings: {greetings.SayHello(user)}");

ReadLine();