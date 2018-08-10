using System;
using System.Collections.Generic;
using System.Text;

namespace NetPriorityQueue
{
    /// <summary>
    /// Represents contents of a StablePriorityQueueNode
    /// </summary>
    public class StablePriorityQueueNode : FastPriorityQueueNode
    {
        /// <summary>
        /// Represents the order the node was inserted in
        /// </summary>
        public long InsertionIndex { get; internal set; }
    }
}
