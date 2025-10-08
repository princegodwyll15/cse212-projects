using System.Collections;
using System.Collections.Generic; // Added for Dictionary and List in Problems 2, 3, 4, 5
using System; // Added for ValueTuple in Problem 5

public static class Recursion
{
    // Direction vectors for Maze Solver (Up, Down, Right, Left)
    private static readonly int[] DX = { 0, 0, 1, -1 };
    private static readonly int[] DY = { 1, -1, 0, 0 };

    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // TODO Start Problem 1
        // Base Case: If n is 0 or less, the sum is 0.
        if (n <= 0)
        {
            return 0;
        }

        // Recursive Step: n^2 + Sum of squares up to (n-1)
        return n * n + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// 
    /// NOTE: We use the optional parameter 'word' to track the current permutation being built.
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // TODO Start Problem 2
        // Base Case: The current 'word' has reached the desired 'size'
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Recursive Step: Iterate over the available letters
        for (int i = 0; i < letters.Length; i++)
        {
            char chosenLetter = letters[i];

            // 1. Choose: Remove the chosen letter from the available letters for the next recursion level
            // This prevents the letter from being reused in the current path.
            string remainingLetters = letters.Remove(i, 1);

            // 2. Recurse: Build the word with the chosen letter and call the function with the remaining letters
            PermutationsChoose(results, remainingLetters, size, word + chosenLetter);

            // 3. Unchoose (Backtrack): Not explicitly needed here because 'remainingLetters' is passed by value (string),
            // and the loop handles iteration over the original 'letters' string for subsequent branches.
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// 
    /// NOTE: We must adjust the base cases for the recursive formula:
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + CountWaysToClimb(s-2) + CountWaysToClimb(s-3)
    /// The standard base cases are: 0 stairs = 1 way (do nothing), <0 stairs = 0 ways.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Initialize memoization dictionary on first call
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // TODO Start Problem 3

        // Check memoization table
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Base Cases (Adjusted for the recursive relation)
        if (s == 0)
        {
            return 1; // 1 way to climb 0 stairs (do nothing)
        }
        if (s < 0)
        {
            return 0; // Cannot climb a negative number of stairs
        }

        // Solve using recursion
        // Note: The problem provides the relation CountWaysToClimb(s) = CountWaysToClimb(s-1) + ...
        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        // Store the result in the memoization table
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // TODO Start Problem 4
        int wildcardIndex = pattern.IndexOf('*');

        // Base Case: No more wildcards
        if (wildcardIndex == -1)
        {
            results.Add(pattern);
            return;
        }

        // Recursive Step: Split the pattern at the first '*'
        string prefix = pattern[..wildcardIndex];
        string suffix = pattern[(wildcardIndex + 1)..];

        // Branch 1: Replace '*' with '0'
        string patternWithZero = prefix + '0' + suffix;
        WildcardBinary(patternWithZero, results);

        // Branch 2: Replace '*' with '1'
        string patternWithOne = prefix + '1' + suffix;
        WildcardBinary(patternWithOne, results);
    }


    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // 1. Initialization
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // 2. CHOOSE: Add the current position (x, y) to the path.
        // This must be done before the base case, as the current cell is part of the path.
        currPath.Add((x, y));

        // 3. Base Case: End Condition
        if (maze.IsEnd(x, y))
        {
            // Found a complete path. Add its string representation to the results.
            results.Add(currPath.AsString());
        }
        else
        {
            // 4. Recursive Step: Explore all 4 adjacent cells
            for (int i = 0; i < 4; i++)
            {
                int nextX = x + DX[i];
                int nextY = y + DY[i];

                // Check if the move is valid using the helper function
                if (maze.IsValidMove(currPath, nextX, nextY))
                {
                    // Recurse from the new valid position
                    SolveMaze(results, maze, nextX, nextY, currPath);
                }
            }
        }

        // 5. UNCHOOSE (Backtrack): Remove the current position before returning.
        // This resets the state for the next possible path branch.
        currPath.RemoveAt(currPath.Count - 1);
    }
}

/// <summary>
/// Required extension method to format the path as a string.
/// </summary>
public static class ListExtensions
{
    public static string AsString(this List<ValueTuple<int, int>> path)
    {
        // Formats the path as (x1,y1)->(x2,y2)->...
        var pathStrings = new List<string>();
        foreach (var p in path)
        {
            pathStrings.Add($"({p.Item1},{p.Item2})");
        }
        return string.Join("->", pathStrings);
    }
}