public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        //initialize a new array to contain the multiples
        double[] multiplesArray = new double[length];
        //create a for loop so that we run the multiples for the exact length specified
        for (int i = 0; i <= length; i++)
        {
            //insert the first multiple value to the first index of our collecting array
            multiplesArray[i] = number * (i + 1);
        }

        return multiplesArray; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        int numberOfData = data.Count;
        if (amount == 0) return;

        List<int> rotatedArray = new List<int>();

        //take the last amount of items specified and populate it to the rotated array
        for (int i = numberOfData - amount; i < numberOfData; i++)
        {
            rotatedArray.Add(data[i]);
        }

        //insert remaining part of data into the rotated array
        for (int i = 0; i < numberOfData - amount; i++)
        {
            rotatedArray.Add(data[i]);
        }
        data.Clear();
        data.AddRange(rotatedArray);
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

    }
}
