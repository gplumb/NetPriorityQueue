using System;
using System.Collections.Generic;
using Xunit;

namespace NetPriorityQueue.Tests
{
    public abstract class SharedFastPriorityQueueTests<TQueue> : SharedPriorityQueueTests<TQueue>
        where TQueue : IFixedSizePriorityQueue<Node, float>
    {
        internal SharedFastPriorityQueueTests()
            : base()
        {
        }

        [Fact]
        public void TestMaxNodes()
        {
            Assert.Equal(100, Queue.MaxSize);
        }

        [Fact]
        public void TestResizeEmptyQueue()
        {
            Queue.Resize(10);
            Assert.Equal(0, Queue.Count);
            Assert.Equal(10, Queue.MaxSize);

            Queue.Resize(3);
            Assert.Equal(0, Queue.Count);
            Assert.Equal(3, Queue.MaxSize);
        }

        [Fact]
        public void TestResizeCopiesNodes()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);
            Enqueue(node2);

            Queue.Resize(10);
            Assert.Equal(2, Queue.Count);
            Assert.Equal(node1, Dequeue());
            Assert.Equal(1, Queue.Count);
            Assert.Equal(node2, Dequeue());
            Assert.Equal(0, Queue.Count);
        }

        [Fact]
        public void TestResizeQueueWasFull()
        {
            List<Node> nodes = new List<Node>(Queue.MaxSize);
            for (int i = 0; i < Queue.MaxSize; i++)
            {
                Node node = new Node(i);
                Enqueue(node);
                nodes.Insert(i, node);
            }

            Queue.Resize(Queue.MaxSize * 5);

            for (int i = 0; i < nodes.Count; i++)
            {
                Assert.Equal(100 - i, Queue.Count);
                Assert.Equal(nodes[i], Dequeue());
            }
        }

        [Fact]
        public void TestResizeQueueBecomesFull()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Queue.Resize(3);
            Assert.Equal(3, Queue.MaxSize);
            Assert.Equal(3, Queue.Count);
            Assert.Equal(node1, Dequeue());
            Assert.Equal(2, Queue.Count);
            Assert.Equal(node2, Dequeue());
            Assert.Equal(1, Queue.Count);
            Assert.Equal(node3, Dequeue());
            Assert.Equal(0, Queue.Count);
        }

        #region Debug build only tests
#if DEBUG
        [Fact]
        public void TestDebugEnqueueThrowsOnFullQueue()
        {
            for (int i = 0; i < Queue.MaxSize; i++)
            {
                Node node = new Node(i);
                Enqueue(node);
            }

            Assert.Throws<InvalidOperationException>(() => Queue.Enqueue(new Node(999), 999));
        }

        [Fact]
        public void TestDebugEnqueueThrowsOnAlreadyEnqueuedNode()
        {
            Node node = new Node(1);

            Enqueue(node);

            Assert.Throws<InvalidOperationException>(() => Queue.Enqueue(node, 1));
        }

        [Fact]
        public void TestDebugDequeueThrowsOnEmptyQueue()
        {
            Assert.Throws<InvalidOperationException>(() => Queue.Dequeue());
        }

        [Fact]
        public void TestDebugDequeueThrowsOnEmptyQueue2()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);
            Enqueue(node2);

            Dequeue();
            Dequeue();

            Assert.Throws<InvalidOperationException>(() => Queue.Dequeue());
        }

        [Fact]
        public void TestDebugFirstThrowsOnEmptyQueue()
        {
            Assert.Throws<InvalidOperationException>(() => { var a = Queue.First; });
        }

        [Fact]
        public void TestDebugFirstThrowsOnEmptyQueue2()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);
            Enqueue(node2);

            Dequeue();
            Dequeue();

            Assert.Throws<InvalidOperationException>(() => { var a = Queue.First; });
        }

        [Fact]
        public void TestDebugDequeueThrowsOnCorruptedQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);
            Enqueue(node2);

            node1.Priority = 3; //Don't ever do this! (use Queue.UpdatePriority(node1, 3) instead)

            Assert.Throws<InvalidOperationException>(() => Queue.Dequeue());
        }

        [Fact]
        public void TestDebugRemoveThrowsOnNodeNotInQueue()
        {
            Node node = new Node(1);

            Assert.Throws<InvalidOperationException>(() => Queue.Remove(node));
        }

        [Fact]
        public void TestDebugRemoveThrowsOnNodeNotInQueue2()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);

            Assert.Throws<InvalidOperationException>(() => Queue.Remove(node2));
        }

        [Fact]
        public void TestDebugRemoveThrowsOnNodeNotInQueue3()
        {
            Node node = new Node(1);

            Enqueue(node);

            Dequeue();

            Assert.Throws<InvalidOperationException>(() => Queue.Remove(node));
        }

        [Fact]
        public void TestDebugUpdatePriorityThrowsOnNodeNotInQueue()
        {
            Node node = new Node(1);

            Assert.Throws<InvalidOperationException>(() => Queue.UpdatePriority(node, 2));
        }

        [Fact]
        public void TestDebugUpdatePriorityThrowsOnNodeNotInQueue2()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);

            Assert.Throws<InvalidOperationException>(() => Queue.UpdatePriority(node2, 3));
        }

        [Fact]
        public void TestDebugUpdatePriorityThrowsOnNodeNotInQueue3()
        {
            Node node = new Node(1);

            Enqueue(node);
            Dequeue();

            Assert.Throws<InvalidOperationException>(() => Queue.UpdatePriority(node, 2));
        }

        [Fact]
        public void TestDebugResizeThrowsOn0SizeQueue()
        {
            Assert.Throws<InvalidOperationException>(() => Queue.Resize(0));
        }

        [Fact]
        public void TestDebugResizeSizeTooSmall()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.Throws<InvalidOperationException>(() => Queue.Resize(2));
        }

        [Fact]
        public void TestDebugNullParametersThrow()
        {
            Assert.Throws<ArgumentNullException>(() => Queue.Contains(null));
            Assert.Throws<ArgumentNullException>(() => Queue.Enqueue(null, 1));
            Assert.Throws<ArgumentNullException>(() => Queue.Remove(null));
            Assert.Throws<ArgumentNullException>(() => Queue.UpdatePriority(null, 1));
        }

        [Fact]
        public void TestDebugContainsOutOfBoundsCloseTo0()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);

            node1.QueueIndex = node2.QueueIndex = -1;
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node1));
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node2));
        }

        [Fact]
        public void TestDebugContainsOutOfBoundsVeryNegative()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);

            node1.QueueIndex = node2.QueueIndex = int.MinValue;
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node1));
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node2));
        }

        [Fact]
        public void TestDebugContainsOutOfBoundsAboveMaxSize()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);

            node1.QueueIndex = node2.QueueIndex = Queue.MaxSize + 1;
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node1));
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node2));
        }

        [Fact]
        public void TestDebugContainsOutOfBoundsVeryLarge()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);

            node1.QueueIndex = node2.QueueIndex = int.MaxValue;
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node1));
            Assert.Throws<InvalidOperationException>(() => Queue.Contains(node2));
        }
#endif
        #endregion
    }
}
