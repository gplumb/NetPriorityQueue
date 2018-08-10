namespace NetPriorityQueue
{
    /// <summary>
    /// Base class for the FastPriorityQueue's nodes. Must be sub-classed before use
    /// </summary>
    public class FastPriorityQueueNode
    {
        /// <summary>
        /// The Priority to insert this node at. Must be set BEFORE adding a node to the queue (ideally just once, in the node's constructor).
        /// Should not be manually edited once the node has been enqueued - use queue.UpdatePriority() instead
        /// </summary>

#if DEBUG
        public float Priority { get; set; }
#else
        public float Priority { get; protected internal set; }
#endif

        /// <summary>
        /// Represents the current position in the queue
        /// </summary>
#if DEBUG
        public int QueueIndex { get; set; }
#else
        public int QueueIndex { get; internal set; }
#endif
    }
}
