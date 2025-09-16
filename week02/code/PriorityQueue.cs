public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Add a new value to the queue with an associated priority.  The
    /// node is always added to the back of the queue regardless of 
    /// the priority.
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="priority">The priority</param>
    public void Enqueue(string value, int priority)
    {
        var newNode = new PriorityItem(value, priority);
        _queue.Add(newNode);
    }

    public string Dequeue()
    {
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        // Find index of the item with the **highest** priority (i.e., largest number)
        int highPriorityIndex = 0;
        int maxPriority = _queue[0].Priority;

        for (int i = 1; i < _queue.Count; i++)
        {
            if (_queue[i].Priority > maxPriority)
            {
                maxPriority = _queue[i].Priority;
                highPriorityIndex = i;
            }
            // Don't replace index if equal — keeps FIFO for duplicates
        }

        var value = _queue[highPriorityIndex].Value;
        _queue.RemoveAt(highPriorityIndex);
        return value;
    }



    // This method is used in the test code to inspect the queue.
    public List<(string, int)> ToList()
    {
        return _queue.Select(item => (item.Value, item.Priority)).ToList();
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    // The graders rely on this method to check if you fixed all the bugs, so changes to it will cause you to lose points.
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}



internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    // The graders rely on this method to check if you fixed all the bugs, so changes to it will cause you to lose points.
    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}