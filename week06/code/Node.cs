public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Insert a new node while maintaining BST properties
        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else if (value > Data) // Prevent duplicates
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
        // If value == Data, do nothing (avoid duplicates)
    }

    public bool Contains(int value)
    {
        // Recursively search the BST
        if (value == Data)
            return true;
        else if (value < Data)
            return Left != null && Left.Contains(value);
        else
            return Right != null && Right.Contains(value);
    }

    public int GetHeight()
    {
        // Recursively calculate the height of left and right subtrees
        int leftHeight = Left?.GetHeight() ?? 0;
        int rightHeight = Right?.GetHeight() ?? 0;
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}
