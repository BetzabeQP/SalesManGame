using System;

namespace MyApp 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            using var game = new SalesManGame.Game1();
            game.Run();
            Console.WriteLine("Hello World!");
        }
    }
}

