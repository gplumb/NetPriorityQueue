namespace NetPriorityQueue.Tests
{
    public class FastPriorityQueueTests : SharedFastPriorityQueueTests<FastPriorityQueue<Node>>
    {
        protected override FastPriorityQueue<Node> CreateQueue()
        {
            return new FastPriorityQueue<Node>(100);
        }

        protected override bool IsValidQueue()
        {
            return Queue.IsValidQueue();
        }
    }
}
