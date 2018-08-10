using System;

namespace NetPriorityQueue.Examples
{
    /// <summary>
    /// Simple program to show how to use NetPrioerityQueue
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            FastPriorityQueueExample.RunExample();

            Console.WriteLine("------------------------------");

            SimplePriorityQueueExample.RunExample();

            Console.ReadKey();
        }
    }
}
