using System;

namespace NetPriorityQueue
{
    /// <summary>
    /// A helper-interface only needed to make writing unit tests a bit easier
    /// </summary>
    public interface IFixedSizePriorityQueue<TItem, in TPriority> : IPriorityQueue<TItem, TPriority>
        where TPriority : IComparable<TPriority>
    {
        /// <summary>
        /// Resize the queue so it can accept more nodes. All currently enqueued nodes remain.
        /// Attempting to decrease below the existing node count will result in undefined behavior.
        /// </summary>
        void Resize(int maxNodes);


        /// <summary>
        /// Returns the maximum number of items that can be enqueued at once. Once you hit this number (ie. once Count == MaxSize),
        /// attempting to enqueue another item will cause undefined behavior.
        /// </summary>
        int MaxSize { get; }
    }
}
