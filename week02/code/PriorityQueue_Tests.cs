using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue multiple elements with varying priorities (including duplicates and ties).
    // Expected Result: Items should be stored in the order they were enqueued.
    // Defect(s) Found: None - this test checks if the internal order matches the insertion order.
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
    // Scenario: Items should be dequeued in order of highest priority first (larger number = higher priority).
    // Expected Result: Dequeue order should be B (50), A (20), C (15), C (14), E (11), A (5), D (5).
    // Defect(s) Found: None expected, verifies proper priority-based dequeue.
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
    // Scenario: All items have the same priority; they should be dequeued in the order they were enqueued (FIFO).
    // Expected Result: Dequeue order should be P, Q, R.
    // Defect(s) Found: This tests correct tie-breaking using FIFO.
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
    // Scenario: Some items share the highest priority; they should be dequeued in FIFO order among same-priority items.
    // Expected Result: Dequeue order should be B (20), C (20), A (10), D (5).
    // Defect(s) Found: Validates FIFO within same-priority and overall priority ordering.
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

    [TestMethod]
    // Scenario: Attempting to dequeue from an empty queue.
    // Expected Result: Should throw InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: Ensures robust error handling for edge cases.
    public void TestPriorityQueue_EmptyDequeue()
    {
        var priorityQueue = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }
}
