using ClassDemoF26BoundedBufferLib.model;
using ClassDemoF26BoundedBufferLib.threadSafe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoF26BoundedBufferApp
{
    internal class Eksperiment1
    {
        private const int MaxWait = 900;
        private const int MaxValue = 12000;
        private const int NoProducers = 10;
        private const int NoConsumers = 5;

        private readonly BoundedBuffer<Item> _buffer;
        //private readonly Queue<Item> _queue;
        private readonly Random _rnd;
        private readonly ConsoleColor[] _colours;



        public Eksperiment1()
        {
            //_queue = new Queue<Item>();
            _buffer = new BoundedBuffer<Item>(40);
            _rnd = new Random(DateTime.Now.Millisecond);
            _colours = new ConsoleColor[] { 
                ConsoleColor.Red, 
                ConsoleColor.Green, 
                ConsoleColor.Blue,
                ConsoleColor.Yellow,
                ConsoleColor.Cyan
            };  

        }

        internal void Start()
        {
            Task waitTask = null;
            for (int i = 0; i < NoProducers; i++)
            {
                int no = i;
                waitTask = Task.Run(() => {
                    //Producer(_queue, no);
                    Producer(_buffer, no);
                    });
            }
            Thread.Sleep(500);
            for (int i = 0; i < NoConsumers; i++)
            {
                int no = i;
                Task.Run(() =>
                {
                    //Consumer(_queue, no);
                    Consumer(_buffer, no);
                    });
            }

            waitTask?.Wait();
        }

        internal void Producer(Queue<Item> queue, int no)
        {
            Console.WriteLine("Producer no " + no + " is startet");
            while (true)
            {
                Thread.Sleep(100 + _rnd.Next(MaxWait));
                Item item = new Item(_rnd.Next(MaxValue));

                queue.Enqueue(item);
            }
        }

        internal void Producer(BoundedBuffer<Item> buffer, int no)
        {
            Console.WriteLine("Producer no " + no + " is startet");
            while (true)
            {
                Thread.Sleep(100 + _rnd.Next(MaxWait));
                Item item = new Item(_rnd.Next(MaxValue));
                Console.WriteLine(item);

                buffer.Insert(item);
            }
        }

        internal void Consumer(Queue<Item> queue, int no)
        {
            ConsoleColor myColor = _colours[no];
            Console.WriteLine("Consumer no " + no + " is startet");
            

            while (true)
            {
                Item item = queue.Dequeue();
                Thread.Sleep(100 + _rnd.Next(MaxWait));
                Console.ForegroundColor = myColor;
                Console.WriteLine("Modtaget " + item);
            }
        }

        internal void Consumer(BoundedBuffer<Item> buffer, int no)
        {
            ConsoleColor myColor = _colours[no];
            Console.WriteLine("Consumer no " + no + " is startet");

            while (true)
            {
                Item item = buffer.Take();
                Thread.Sleep(100 + _rnd.Next(MaxWait));
                Console.ForegroundColor = myColor;
                Console.WriteLine("Modtaget " + item);
            }
        }

    }
}
