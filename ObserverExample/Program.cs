using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Book newbook = new Book("C# Fundamentals");
            BookManager ob = new BookManager();
            ob.NewArrivals(newbook);
            ob.NewArrivals(new Book("10 Days With Sony Alpha 58"));
            ob.NewArrivals(new Book("10 Instagram Tip make your Post Popular"));
            Board board = new Board("C# Ninja Tricks");
            board.Subscribe(ob);
            Console.ReadLine();
        }
    }
}
