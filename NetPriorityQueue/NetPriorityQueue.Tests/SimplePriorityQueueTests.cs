using System;
using Xunit;

namespace NetPriorityQueue.Tests
{
    public class SimplePriorityQueueTests : SharedPriorityQueueTests<SimplePriorityQueue<Node>>
    {
        protected override SimplePriorityQueue<Node> CreateQueue()
        {
            return new SimplePriorityQueue<Node>();
        }

        protected override bool IsValidQueue()
        {
            return Queue.IsValidQueue();
        }

        protected void EnqueueWithoutDuplicates(Node node)
        {
            Queue.EnqueueWithoutDuplicates(node, node.Priority);
            Assert.True(IsValidQueue());
        }

        [Fact]
        public void TestOrderedQueue()
        {
            SharedStablePriorityQueueTests.TestOrderedQueue(Enqueue, Dequeue);
        }

        [Fact]
        public void TestMoreComplicatedOrderedQueue()
        {
            SharedStablePriorityQueueTests.TestMoreComplicatedOrderedQueue(Enqueue, Dequeue);
        }

        [Fact]
        public void TestQueueAutomaticallyResizes()
        {
            for (int i = 0; i < 1000; i++)
            {
                Enqueue(new Node(i));
                Assert.Equal(i + 1, Queue.Count);
            }

            for (int i = 0; i < 1000; i++)
            {
                Node node = Dequeue();
                Assert.Equal(i, node.Priority);
            }
        }

        [Fact]
        public void TestDequeueThrowsOnEmptyQueue()
        {
            Assert.Throws<InvalidOperationException>(() => Queue.Dequeue());
        }

        [Fact]
        public void TestDequeueThrowsOnEmptyQueue2()
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
        public void TestClearWithDuplicates()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node1);
            Enqueue(node2);
            Queue.Clear();
            Enqueue(node2);
            Enqueue(node3);
            Enqueue(node3);

            Assert.Equal(3, Queue.Count);
            Assert.False(Queue.Contains(node1));
            Assert.True(Queue.Contains(node2));
            Assert.True(Queue.Contains(node3));

            Assert.Equal(node2, Dequeue());
            Assert.Equal(node3, Dequeue());
            Assert.Equal(node3, Dequeue());

            Assert.Equal(0, Queue.Count);
            Assert.False(Queue.Contains(node1));
            Assert.False(Queue.Contains(node2));
            Assert.False(Queue.Contains(node3));
        }

        [Fact]
        public void TestClearWithNull()
        {
            Queue.Enqueue(null, 1);
            Queue.Clear();

            Assert.Equal(0, Queue.Count);
            Assert.False(Queue.Contains(null));
        }

        [Fact]
        public void TestClearWithNullDuplicates()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Enqueue(node1);
            Enqueue(node1);
            Enqueue(node2);
            Queue.Clear();
            Enqueue(node2);
            Queue.Enqueue(null, 3);
            Queue.Enqueue(null, 3);

            Assert.Equal(3, Queue.Count);
            Assert.False(Queue.Contains(node1));
            Assert.True(Queue.Contains(node2));
            Assert.True(Queue.Contains(null));

            Assert.Equal(node2, Dequeue());
            Assert.Null(Dequeue());
            Assert.Null(Dequeue());

            Assert.Equal(0, Queue.Count);
            Assert.False(Queue.Contains(node1));
            Assert.False(Queue.Contains(node2));
            Assert.False(Queue.Contains(null));
        }

        [Fact]
        public void TestClearWithNullDuplicates2()
        {
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Queue.Enqueue(null, 1);
            Queue.Enqueue(null, 1);
            Enqueue(node2);
            Queue.Clear();
            Enqueue(node2);
            Enqueue(node3);
            Enqueue(node3);

            Assert.Equal(3, Queue.Count);
            Assert.False(Queue.Contains(null));
            Assert.True(Queue.Contains(node2));
            Assert.True(Queue.Contains(node3));

            Assert.Equal(node2, Dequeue());
            Assert.Equal(node3, Dequeue());
            Assert.Equal(node3, Dequeue());

            Assert.Equal(0, Queue.Count);
            Assert.False(Queue.Contains(null));
            Assert.False(Queue.Contains(node2));
            Assert.False(Queue.Contains(node3));
        }

        [Fact]
        public void TestFirstThrowsOnEmptyQueue()
        {
            Assert.Throws<InvalidOperationException>(() => { var a = Queue.First; });
        }

        [Fact]
        public void TestFirstThrowsOnEmptyQueue2()
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
        public void TestEnqueueRemovesOneCopyOfItem()
        {
            Node node = new Node(1);

            Enqueue(node);
            Enqueue(node);

            Assert.Equal(2, Queue.Count);
            Assert.True(Queue.Contains(node));

            Queue.Remove(node);

            Assert.Equal(1, Queue.Count);
            Assert.True(Queue.Contains(node));

            Queue.Remove(node);

            Assert.Equal(0, Queue.Count);
            Assert.False(Queue.Contains(node));
        }

        [Fact]
        public void TestEnqueueRemovesFirstCopyOfItem()
        {
            Node node11 = new Node(1);
            Node node12 = new Node(1);

            Enqueue(node11);
            Enqueue(node12);
            Enqueue(node11);

            Assert.Equal(node11, Queue.First);

            Queue.Remove(node11);

            Assert.Equal(node12, Dequeue());
            Assert.Equal(node11, Dequeue());
            Assert.Equal(0, Queue.Count);
        }

        [Fact]
        public void TestMultipleCopiesOfSameItem()
        {
            Node node1 = new Node(1);
            Node node21 = new Node(2);
            Node node22 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node21);
            Enqueue(node22);
            Enqueue(node21);
            Enqueue(node22);
            Enqueue(node3);
            Enqueue(node3);
            Enqueue(node1);

            Assert.Equal(node1, Dequeue());
            Assert.Equal(node1, Dequeue());
            Assert.Equal(node21, Dequeue());
            Assert.Equal(node22, Dequeue());
            Assert.Equal(node21, Dequeue());
            Assert.Equal(node22, Dequeue());
            Assert.Equal(node3, Dequeue());
            Assert.Equal(node3, Dequeue());
        }

        [Fact]
        public void TestEnqueuingNull()
        {
            Queue.Enqueue(null, 1);
            Assert.Equal(1, Queue.Count);
            Assert.Null(Queue.First);
            Assert.True(Queue.Contains(null));
            Assert.False(Queue.Contains(new Node(1)));

            Assert.Null(Dequeue());

            Assert.Equal(0, Queue.Count);
            Assert.False(Queue.Contains(null));
        }

        [Fact]
        public void TestEnqueuingDuplicateNulls()
        {
            Node node = new Node(2);
            Queue.Enqueue(null, 1);
            Queue.Enqueue(node, 2);
            Queue.Enqueue(null, 3);

            Assert.Equal(3, Queue.Count);
            Assert.True(Queue.Contains(null));

            Assert.Null(Dequeue());
            Assert.Equal(node, Dequeue());

            Assert.True(Queue.Contains(null));

            Assert.Null(Dequeue());
            Assert.Equal(0, Queue.Count);

            Assert.False(Queue.Contains(node));
            Assert.False(Queue.Contains(null));
        }

        [Fact]
        public void TestRemoveThrowsOnNodeNotInQueue()
        {
            Node node = new Node(1);

            Assert.Throws<InvalidOperationException>(() => Queue.Remove(node));
        }

        [Fact]
        public void TestUpdatePriorityThrowsOnNodeNotInQueue()
        {
            Node node = new Node(1);

            Assert.Throws<InvalidOperationException>(() => Queue.UpdatePriority(node, 2));
        }

        [Fact]
        public void TestCanSortInOppositeDirection()
        {
            Queue = new SimplePriorityQueue<Node>((x, y) => y.CompareTo(x));

            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.Equal(node3, Dequeue());
            Assert.Equal(node2, Dequeue());
            Assert.Equal(node1, Dequeue());
        }

        [Fact]
        public void TestGetPriority()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.Equal(1, Queue.GetPriority(node1));
            Assert.Equal(2, Queue.GetPriority(node2));
            Assert.Equal(3, Queue.GetPriority(node3));
        }

        [Fact]
        public void TestEnqueueWithoutDuplicatesNormal()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            EnqueueWithoutDuplicates(node1);
            EnqueueWithoutDuplicates(node2);
            EnqueueWithoutDuplicates(node3);

            Assert.Equal(node1, Dequeue());
            Assert.Equal(node2, Dequeue());
            Assert.Equal(node3, Dequeue());
        }

        [Fact]
        public void TestEnqueueWithoutDuplicatesWithDuplicates()
        {
            Node node = new Node(1);

            EnqueueWithoutDuplicates(node);
            EnqueueWithoutDuplicates(node);
            EnqueueWithoutDuplicates(node);

            Assert.Equal(1, Queue.Count);
            Assert.Equal(node, Dequeue());
        }

        [Fact]
        public void TestEnqueueWithoutDuplicatesWithDuplicatesMoreComplicated()
        {
            Node node11 = new Node(1);
            Node node12 = new Node(1);
            Node node2 = new Node(2);

            EnqueueWithoutDuplicates(node11);
            EnqueueWithoutDuplicates(node12);
            EnqueueWithoutDuplicates(node12);
            EnqueueWithoutDuplicates(node2);
            EnqueueWithoutDuplicates(node11);

            Assert.Equal(3, Queue.Count);
            Assert.Equal(node11, Dequeue());
            Assert.Equal(node12, Dequeue());
            Assert.Equal(node2, Dequeue());
        }

        [Fact]
        public void TestTryFirstEmptyQueue()
        {
            Node first;
            Assert.False(Queue.TryFirst(out first));
            Assert.Null(first);
        }

        [Fact]
        public void TestTryFirstWithItems()
        {
            Node node = new Node(1);
            Node first;

            Enqueue(node);

            Assert.True(Queue.TryFirst(out first));
            Assert.Equal(node, first);
            Assert.Equal(1, Queue.Count);
        }

        [Fact]
        public void TestTryDequeueEmptyQueue()
        {
            Node first;
            Assert.False(Queue.TryDequeue(out first));
            Assert.Null(first);
        }

        [Fact]
        public void TestTryDequeueWithItems()
        {
            Node node = new Node(1);
            Node first;

            Enqueue(node);

            Assert.True(Queue.TryDequeue(out first));
            Assert.Equal(node, first);
            Assert.Equal(0, Queue.Count);
        }

        [Fact]
        public void TestTryRemoveEmptyQueue()
        {
            Node node = new Node(1);

            Assert.False(Queue.TryRemove(node));
        }

        [Fact]
        public void TestTryRemoveItemInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.True(Queue.Contains(node2));
            Assert.True(Queue.TryRemove(node2));
            Assert.False(Queue.Contains(node2));
            Assert.False(Queue.TryRemove(node2));

            Assert.True(Queue.Contains(node3));
            Assert.True(Queue.TryRemove(node3));
            Assert.False(Queue.Contains(node3));
            Assert.False(Queue.TryRemove(node3));

            Assert.True(Queue.Contains(node1));
            Assert.True(Queue.TryRemove(node1));
            Assert.False(Queue.Contains(node1));
            Assert.False(Queue.TryRemove(node1));
        }

        [Fact]
        public void TestTryRemoveItemNotInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);

            Assert.False(Queue.TryRemove(node3));
        }

        [Fact]
        public void TestTryUpdatePriorityEmptyQueue()
        {
            Node node = new Node(1);

            Assert.False(Queue.TryUpdatePriority(node, 2));
        }

        [Fact]
        public void TestTryUpdatePriorityItemInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.True(Queue.TryUpdatePriority(node2, 0));

            Assert.Equal(3, Queue.Count);
            Assert.Equal(node2, Dequeue());
        }

        [Fact]
        public void TestTryUpdatePriorityItemNotInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);

            Enqueue(node1);
            Enqueue(node2);

            Assert.False(Queue.TryUpdatePriority(node3, 0));
        }

        [Fact]
        public void TestTryGetPriorityEmptyQueue()
        {
            Node node = new Node(1);
            float priority;

            Assert.False(Queue.TryGetPriority(node, out priority));
            Assert.Equal(0, priority);
        }

        [Fact]
        public void TestTryGetPriorityItemInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            float priority;

            Enqueue(node1);
            Enqueue(node2);
            Enqueue(node3);

            Assert.True(Queue.TryGetPriority(node2, out priority));
            Assert.Equal(2, priority);
        }

        [Fact]
        public void TestTryGetPriorityItemNotInQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            float priority;

            Enqueue(node1);
            Enqueue(node2);

            Assert.False(Queue.TryGetPriority(node3, out priority));
            Assert.Equal(0, priority);
        }
    }
}
