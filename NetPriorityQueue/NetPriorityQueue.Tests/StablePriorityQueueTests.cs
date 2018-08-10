using Xunit;

namespace NetPriorityQueue.Tests
{
    public class StablePriorityQueueTests : SharedFastPriorityQueueTests<StablePriorityQueue<Node>>
    {
        protected override StablePriorityQueue<Node> CreateQueue()
        {
            return new StablePriorityQueue<Node>(100);
        }

        protected override bool IsValidQueue()
        {
            return Queue.IsValidQueue();
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
    }
}
