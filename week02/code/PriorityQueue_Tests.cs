using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: 
    // Expected Result: 
    // Defect(s) Found: 
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 50);
        priorityQueue.Enqueue("C", 15);
        priorityQueue.Enqueue("A", 20);
        priorityQueue.Enqueue("C", 14);
        priorityQueue.Enqueue("D", 5);
        priorityQueue.Enqueue("E", 11);

        List<(string, int)> expectedResults = new()
        {
            ("A", 5),
            ("B", 50),
            ("C", 15),
            ("A", 20),
            ("C", 14),
            ("D", 5),
            ("E", 11)
        };

        CollectionAssert.AreEqual(expectedResults, priorityQueue.ToList());
    }


    [TestMethod]
    // Scenario: 
    // Expected Result: 
    // Defect(s) Found: 
    // Scenario: Items should be dequeued by highest priority (larger number).
    // Expected Result: B (50), A (20), C (15), C (14), E (11), A (5), D (5)
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 50);
        priorityQueue.Enqueue("C", 15);
        priorityQueue.Enqueue("A", 20);
        priorityQueue.Enqueue("C", 14);
        priorityQueue.Enqueue("D", 5);
        priorityQueue.Enqueue("E", 11);

        List<string> expectedOrder = new() { "B", "A", "C", "C", "E", "A", "D" };

        foreach (var expected in expectedOrder)
        {
            var actual = priorityQueue.Dequeue();
            Assert.AreEqual(expected, actual);
        }
    }

    [TestMethod]
    // Scenario: Same priority → FIFO dequeue order
    // Expected Result: P, Q, R (since all same priority)
    public void TestPriorityQueue_SamePriorityOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("P", 10);
        priorityQueue.Enqueue("Q", 10);
        priorityQueue.Enqueue("R", 10);

        Assert.AreEqual("P", priorityQueue.Dequeue());
        Assert.AreEqual("Q", priorityQueue.Dequeue());
        Assert.AreEqual("R", priorityQueue.Dequeue());
    }


    [TestMethod]
    public void TestPriorityQueue_TieBreakerFIFO()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 10);
        pq.Enqueue("B", 20);
        pq.Enqueue("C", 20);
        pq.Enqueue("D", 5);

        Assert.AreEqual("B", pq.Dequeue()); // First of highest priority
        Assert.AreEqual("C", pq.Dequeue()); // Second of highest priority
        Assert.AreEqual("A", pq.Dequeue()); // Next highest priority
        Assert.AreEqual("D", pq.Dequeue()); // Lowest priority
    }
    // Add more test cases as needed below.

    [TestMethod]
    // Scenario: Dequeue from empty queue should throw
    // Expected Result: InvalidOperationException with correct message
    public void TestPriorityQueue_EmptyDequeue()
    {
        var priorityQueue = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }
}