using System;
using Xunit;

namespace NetPriorityQueue.Tests
{
    public class Node : StablePriorityQueueNode
    {
        public Node(int priority)
        {
            Priority = priority;
        }

        public override string ToString()
        {
            return String.Format("Priority: {0}, InsertionIndex: {1}, QueueIndex: {2}", Priority, InsertionIndex, QueueIndex);
        }
    }

    public abstract class SharedPriorityQueueTests<TQueue> where TQueue : IPriorityQueue<Node, float>
    {
        protected TQueue Queue { get; set; }

        protected abstract TQueue CreateQueue();
        protected abstract bool IsValidQueue();

        protected void Enqueue(Node node)
        {
            Queue.Enqueue(node, node.Priority);
            Assert.True(IsValidQueue());
        }

        protected Node Dequeue()
        {
            Node returnMe = Queue.Dequeue();
            Assert.True(IsValidQueue());
            return returnMe;
        }

        public SharedPriorityQueueTests()
        {
            Queue = CreateQueue();
        }

        [Fact]
        public void TestSanity()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Assert.Equal(node1, node1);
            Assert.Equal(node2, node2);
            Assert.NotEqual(node1, node2);
        }

        [Fact]
        public void TestCount()
        {
            Assert.Equal(0, Queue.Count);

            Enqueue(new Node(1));
            Assert.Equal(1, Queue.Count);

            Dequeue();
            Assert.Equal(0, Queue.Count);
        }

        [Fact]
        public void TestFirst()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);
            Enqueue(node2);

            Assert.Equal(node1, Queue.First);
            Assert.Equal(node1, Dequeue());
            Assert.Equal(node2, Queue.First);
        }

        [Fact]
        public void TestEnqueueWorksWithTwoNodesWithSamePriority()
        {
            Node node11 = new Node(1);
            Node node12 = new Node(1);

            Enqueue(node11);
            Enqueue(node12);

            Node firstNode = Dequeue();
            Node secondNode = Dequeue();

            // Assert we got the correct nodes, but since the queue might not be stable, the order doesn't matter
            Assert.True((firstNode == node11 && secondNode == node12) || (firstNode == node12 && secondNode == node11));
        }

        [Fact]
        public void TestSimpleQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node2);
            Enqueue(node5);
            Enqueue(node1);
            Enqueue(node3);
            Enqueue(node4);

            Assert.Equal(node1, Dequeue());
            Assert.Equal(node2, Dequeue());
            Assert.Equal(node3, Dequeue());
            Assert.Equal(node4, Dequeue());
            Assert.Equal(node5, Dequeue());
        }

        [Fact]
        public void TestForwardOrder()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);
            Enqueue(node4);
            Enqueue(node5);

            Assert.Equal(node1, Dequeue());
            Assert.Equal(node2, Dequeue());
            Assert.Equal(node3, Dequeue());
            Assert.Equal(node4, Dequeue());
            Assert.Equal(node5, Dequeue());
        }

        [Fact]
        public void TestBackwardOrder()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node5);
            Enqueue(node4);
            Enqueue(node3);
            Enqueue(node2);
            Enqueue(node1);

            Assert.Equal(node1, Dequeue());
            Assert.Equal(node2, Dequeue());
            Assert.Equal(node3, Dequeue());
            Assert.Equal(node4, Dequeue());
            Assert.Equal(node5, Dequeue());
        }

        [Fact]
        public void TestAddingSameNodesLater()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node2);
            Enqueue(node5);
            Enqueue(node1);
            Enqueue(node3);
            Enqueue(node4);

            Assert.Equal(node1, Dequeue());
            Assert.Equal(node2, Dequeue());
            Assert.Equal(node3, Dequeue());
            Assert.Equal(node4, Dequeue());
            Assert.Equal(node5, Dequeue());

            Enqueue(node5);
            Enqueue(node3);
            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node4);

            Assert.Equal(node1, Dequeue());
            Assert.Equal(node2, Dequeue());
            Assert.Equal(node3, Dequeue());
            Assert.Equal(node4, Dequeue());
            Assert.Equal(node5, Dequeue());
        }

        [Fact]
        public void TestAddingDifferentNodesLater()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node2);
            Enqueue(node5);
            Enqueue(node1);
            Enqueue(node3);
            Enqueue(node4);

            Assert.Equal(node1, Dequeue());
            Assert.Equal(node2, Dequeue());
            Assert.Equal(node3, Dequeue());
            Assert.Equal(node4, Dequeue());
            Assert.Equal(node5, Dequeue());

            Node node6 = new Node(6);
            Node node7 = new Node(7);
            Node node8 = new Node(8);
            Node node9 = new Node(9);
            Node node10 = new Node(10);

            Enqueue(node6);
            Enqueue(node7);
            Enqueue(node8);
            Enqueue(node10);
            Enqueue(node9);

            Assert.Equal(node6, Dequeue());
            Assert.Equal(node7, Dequeue());
            Assert.Equal(node8, Dequeue());
            Assert.Equal(node9, Dequeue());
            Assert.Equal(node10, Dequeue());
        }

        [Fact]
        public void TestClear()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            Enqueue(node2);
            Enqueue(node5);
            Queue.Clear();
            Enqueue(node1);
            Enqueue(node3);
            Enqueue(node4);

            Assert.Equal(3, Queue.Count);
            Assert.True(Queue.Contains(node1));
            Assert.False(Queue.Contains(node2));
            Assert.True(Queue.Contains(node3));
            Assert.True(Queue.Contains(node4));
            Assert.False(Queue.Contains(node5));

            Assert.Equal(node1, Dequeue());
            Assert.Equal(node3, Dequeue());
            Assert.Equal(node4, Dequeue());

            Assert.Equal(0, Queue.Count);
            Assert.False(Queue.Contains(node1));
            Assert.False(Queue.Contains(node2));
            Assert.False(Queue.Contains(node3));
            Assert.False(Queue.Contains(node4));
            Assert.False(Queue.Contains(node5));
        }
    }
}
