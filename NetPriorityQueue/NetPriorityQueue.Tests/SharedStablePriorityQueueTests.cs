using System;
using Xunit;

namespace NetPriorityQueue.Tests
{
    /// <summary>
    /// Crude way to share these tests between StablePriorityQueueTests and SimplePriorityQueueTests
    /// (Using inheritance like the other tests would require multiple inheritance!)
    /// </summary>
    public static class SharedStablePriorityQueueTests
    {
        public static void TestOrderedQueue(Action<Node> enqueue, Func<Node> dequeue)
        {
            Node node1 = new Node(1);
            Node node2 = new Node(1);
            Node node3 = new Node(1);
            Node node4 = new Node(1);
            Node node5 = new Node(1);

            enqueue(node1);
            enqueue(node2);
            enqueue(node3);
            enqueue(node4);
            enqueue(node5);

            Assert.Equal(node1, dequeue());
            Assert.Equal(node2, dequeue());
            Assert.Equal(node3, dequeue());
            Assert.Equal(node4, dequeue());
            Assert.Equal(node5, dequeue());
        }

        public static void TestMoreComplicatedOrderedQueue(Action<Node> enqueue, Func<Node> dequeue)
        {
            Node node11 = new Node(1);
            Node node12 = new Node(1);
            Node node13 = new Node(1);
            Node node14 = new Node(1);
            Node node15 = new Node(1);
            Node node21 = new Node(2);
            Node node22 = new Node(2);
            Node node23 = new Node(2);
            Node node24 = new Node(2);
            Node node25 = new Node(2);
            Node node31 = new Node(3);
            Node node32 = new Node(3);
            Node node33 = new Node(3);
            Node node34 = new Node(3);
            Node node35 = new Node(3);
            Node node41 = new Node(4);
            Node node42 = new Node(4);
            Node node43 = new Node(4);
            Node node44 = new Node(4);
            Node node45 = new Node(4);
            Node node51 = new Node(5);
            Node node52 = new Node(5);
            Node node53 = new Node(5);
            Node node54 = new Node(5);
            Node node55 = new Node(5);

            enqueue(node31);
            enqueue(node51);
            enqueue(node52);
            enqueue(node11);
            enqueue(node21);
            enqueue(node22);
            enqueue(node53);
            enqueue(node41);
            enqueue(node12);
            enqueue(node32);
            enqueue(node13);
            enqueue(node42);
            enqueue(node43);
            enqueue(node44);
            enqueue(node45);
            enqueue(node54);
            enqueue(node14);
            enqueue(node23);
            enqueue(node24);
            enqueue(node33);
            enqueue(node34);
            enqueue(node55);
            enqueue(node35);
            enqueue(node25);
            enqueue(node15);

            Assert.Equal(node11, dequeue());
            Assert.Equal(node12, dequeue());
            Assert.Equal(node13, dequeue());
            Assert.Equal(node14, dequeue());
            Assert.Equal(node15, dequeue());
            Assert.Equal(node21, dequeue());
            Assert.Equal(node22, dequeue());
            Assert.Equal(node23, dequeue());
            Assert.Equal(node24, dequeue());
            Assert.Equal(node25, dequeue());
            Assert.Equal(node31, dequeue());
            Assert.Equal(node32, dequeue());
            Assert.Equal(node33, dequeue());
            Assert.Equal(node34, dequeue());
            Assert.Equal(node35, dequeue());
            Assert.Equal(node41, dequeue());
            Assert.Equal(node42, dequeue());
            Assert.Equal(node43, dequeue());
            Assert.Equal(node44, dequeue());
            Assert.Equal(node45, dequeue());
            Assert.Equal(node51, dequeue());
            Assert.Equal(node52, dequeue());
            Assert.Equal(node53, dequeue());
            Assert.Equal(node54, dequeue());
            Assert.Equal(node55, dequeue());
        }
    }
}
