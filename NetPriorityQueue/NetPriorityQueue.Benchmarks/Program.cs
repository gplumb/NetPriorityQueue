using System;
using System.Text;

namespace NetPriorityQueue.Benchmarks
{
    /// <summary>
    /// Program for benchmarking the FastPriorityQueue
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        public static void Main(string[] args)
        {
            RunSuite(1);
            RunSuite(100);
            RunSuite(10000);

            Console.WriteLine("Finished. Press [Enter] key to exit.");
            Console.ReadLine();
        }


        /// <summary>
        /// Run the suite of benchmark tests for the given number of iterations
        /// </summary>
        public static void RunSuite(int iterations)
        {
            // Set up the queue
            var suite = new Benchmarks();
            suite.QueueSize = iterations;
            suite.GlobalSetup();

            // Set up output capture
            var results = new StringBuilder();
            results.AppendLine($"Results for {iterations} iteration(s):");

            // Enqueue
            suite.IterationCleanup();
            var time = Measure(() => { suite.Enqueue(); });
            results.AppendLine($" - Enqueue: {time.TotalMilliseconds} ms");

            // EnqueueBackwards
            suite.IterationCleanup();
            time = Measure(() => { suite.EnqueueBackwards(); });
            results.AppendLine($" - EnqueueBackwards: {time.TotalMilliseconds} ms");

            // EnqueueRandom
            suite.IterationCleanup();
            time = Measure(() => { suite.EnqueueRandom(); });
            results.AppendLine($" - EnqueueRandom: {time.TotalMilliseconds} ms");

            // EnqueueDequeue
            suite.IterationCleanup();
            time = Measure(() => { suite.EnqueueDequeue(); });
            results.AppendLine($" - EnqueueDequeue: {time.TotalMilliseconds} ms");

            // EnqueueBackwardsDequeue
            suite.IterationCleanup();
            time = Measure(() => { suite.EnqueueBackwardsDequeue(); });
            results.AppendLine($" - EnqueueBackwardsDequeue: {time.TotalMilliseconds} ms");

            // EnqueueRandomDequeue
            suite.IterationCleanup();
            time = Measure(() => { suite.EnqueueRandomDequeue(); });
            results.AppendLine($" - EnqueueRandomDequeue: {time.TotalMilliseconds} ms");

            // EnqueueUpdatePriority
            suite.IterationCleanup();
            time = Measure(() => { suite.EnqueueUpdatePriority(); });
            results.AppendLine($" - EnqueueUpdatePriority: {time.TotalMilliseconds} ms");

            // EnqueueBackwardsUpdatePriority
            suite.IterationCleanup();
            time = Measure(() => { suite.EnqueueBackwardsUpdatePriority(); });
            results.AppendLine($" - EnqueueBackwardsUpdatePriority: {time.TotalMilliseconds} ms");

            // EnqueueRandomUpdatePriority
            suite.IterationCleanup();
            time = Measure(() => { suite.EnqueueRandomUpdatePriority(); });
            results.AppendLine($" - EnqueueRandomUpdatePriority: {time.TotalMilliseconds} ms");

            // EnqueueContains
            suite.IterationCleanup();
            time = Measure(() => { suite.EnqueueContains(); });
            results.AppendLine($" - EnqueueContains: {time.TotalMilliseconds} ms");

            // EnqueueEnumerator
            suite.IterationCleanup();
            time = Measure(() => { suite.EnqueueEnumerator(); });
            results.AppendLine($" - EnqueueEnumerator: {time.TotalMilliseconds} ms");

            // Output for this run
            Console.WriteLine(results);
        }


        /// <summary>
        /// Measures the execution time of the given action
        /// </summary>
        public static TimeSpan Measure(Action action)
        {
            var started = DateTime.UtcNow;

            action.Invoke();
            return DateTime.UtcNow.Subtract(started);
        }
    }
}
