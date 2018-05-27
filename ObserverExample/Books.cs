using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverExample
{
    class Book
    {

        private string mtitle;
        public Book(string title)
        {
            this.mtitle = title;
        }
        public string GetTtitle
        {
            get { return this.mtitle; }
        }
    }


    class BookManager : IObservable<Book>
    {
        private List<IObserver<Book>> observers;
        private List<Book> blist;  
        public BookManager()
        {
            observers = new List<IObserver<Book>>();
            Console.WriteLine("I am book manager");
            blist = new List<Book>();
        }
        public void NewArrivals(Book b)
        {
            blist.Add(b);
        }
        public IDisposable Subscribe(IObserver<Book> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                Console.WriteLine("Subscribed Book manager");
                
                foreach (Book n in blist)
                {
                    observer.OnNext(n);
                }
            }
            return new Unsubscriber<Book>(observers, observer);
        }
    }


    class Board : IObserver<Book>
    {
        private IDisposable newarrivals;
        private string name;
        public Board(string mname)
        {
            this.name = mname;
            Console.WriteLine("I am the Book Board!");
        }
        public virtual void Subscribe(BookManager provider)
        {
            newarrivals = provider.Subscribe(this);
            Console.WriteLine("Subscribed the Board @");
        }

        public virtual void Unsubscribe()
        {
            newarrivals.Dispose();
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine("On complete  " + this);
        }
        public virtual void OnNext(Book info)
        {
            Console.WriteLine("On board:" + info.GetTtitle);
        }

        // No implementation needed: Method is not called by the BaggageHandler class.
        public virtual void OnError(Exception e)
        {
            // No implementation.
        }
    }

    internal class Unsubscriber<Book> : IDisposable
    {
        private List<IObserver<Book>> _observers;
        private IObserver<Book> _observer;

        internal Unsubscriber(List<IObserver<Book>> observers, IObserver<Book> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }


}
