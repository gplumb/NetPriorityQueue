using System;

namespace NetPriorityQueue.Benchmarks
{
    /// <summary>
    /// Simple class for benchmarking FastPriorityQueue
    /// </summary>
    /// <remarks>
    /// At the time of porting, the BenchmarkDotNet.Core Nuget package actually imported .net 4.x assemblies!
    /// This code is faithful to the originally forked content and is just executed slightly differently in Program.cs
    /// </remarks>
    public class Benchmarks
    {
        public int QueueSize;

        public FastPriorityQueueNode[] Nodes;
        public int[] RandomPriorities;
        public int[] RandomUpdatePriorities;

        private FastPriorityQueue<FastPriorityQueueNode> Queue;

        
        /// <summary>
        /// GlobalSetup
        /// </summary>
        public void GlobalSetup()
        {
            Queue = new FastPriorityQueue<FastPriorityQueueNode>(QueueSize);
            Nodes = new FastPriorityQueueNode[QueueSize];
            RandomPriorities = new int[QueueSize];
            RandomUpdatePriorities = new int[QueueSize];
            Random rand = new Random(34829061);
            for (int i = 0; i < QueueSize; i++)
            {
                Nodes[i] = new FastPriorityQueueNode();
                RandomPriorities[i] = rand.Next(16777216); // constrain to range float can hold with no rounding
                RandomUpdatePriorities[i] = rand.Next(16777216); // constrain to range float can hold with no rounding
            }
        }


        /// <summary>
        /// Iteration Cleanup
        /// </summary>
        public void IterationCleanup()
        {
            Queue.Clear();
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public void Enqueue()
        {
            Queue.Clear();
            for (int i = 0; i < QueueSize; i++)
            {
                Queue.Enqueue(Nodes[i], i);
            }
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public void EnqueueBackwards()
        {
            Queue.Clear();
            for (int i = QueueSize - 1; i >= 0; i--)
            {
                Queue.Enqueue(Nodes[i], i);
            }
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public void EnqueueRandom()
        {
            Queue.Clear();
            for (int i = 0; i < QueueSize; i++)
            {
                Queue.Enqueue(Nodes[i], RandomPriorities[i]);
            }
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public void EnqueueDequeue()
        {
            Enqueue();

            for (int i = 0; i < QueueSize; i++)
            {
                Queue.Dequeue();
            }
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public void EnqueueBackwardsDequeue()
        {
            EnqueueBackwards();

            for (int i = 0; i < QueueSize; i++)
            {
                Queue.Dequeue();
            }
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public void EnqueueRandomDequeue()
        {
            EnqueueRandom();

            for (int i = 0; i < QueueSize; i++)
            {
                Queue.Dequeue();
            }
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public void EnqueueUpdatePriority()
        {
            Enqueue();

            for (int i = 0; i < QueueSize; i++)
            {
                Queue.UpdatePriority(Nodes[i], QueueSize - i);
            }
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public void EnqueueBackwardsUpdatePriority()
        {
            EnqueueBackwards();

            for (int i = 0; i < QueueSize; i++)
            {
                Queue.UpdatePriority(Nodes[i], QueueSize - i);
            }
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public void EnqueueRandomUpdatePriority()
        {
            EnqueueRandom();

            for (int i = 0; i < QueueSize; i++)
            {
                Queue.UpdatePriority(Nodes[i], RandomUpdatePriorities[i]);
            }
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public bool EnqueueContains()
        {
            Enqueue();
            bool ret = true; // to ensure the compiler doesn't optimize the contains calls out of existence

            for (int i = 0; i < QueueSize; i++)
            {
                ret &= Queue.Contains(Nodes[i]);
            }
            return ret;
        }


        /// <summary>
        /// Benchmark
        /// </summary>
        public float EnqueueEnumerator()
        {
            Enqueue();
            float prioritySum = 0;

            foreach (FastPriorityQueueNode node in Queue)
            {
                prioritySum += node.Priority;
            }

            return prioritySum;
        }
    }
}
