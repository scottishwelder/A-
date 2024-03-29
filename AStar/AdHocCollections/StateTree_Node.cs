using System.Text;

namespace AStar.AdHocCollections;

/// Partial definition 3 of 5
public partial class StateTree<T>
{
    private class Node
    {
        public readonly T Data;
        public int Height = 1;
        public Node? Left;
        public Node? Right;

        public Node(T data, Node? left, Node? right)
        {
            Data = data;
            Left = left;
            Right = right;
        }

        public void UpdateHeight()
        {
            Height = Math.Max(GetHeight(Left), GetHeight(Right)) + 1;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            var left = Left?.ToString();
            var right = Right?.ToString();
            if (left is not null)
                builder.Append(left + ", \n");
            builder.Append(Data);
            if (right is not null)
                builder.Append(", \n" + right);
            return builder.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Left is not null)
                foreach (var element in Left)
                    yield return element;

            yield return Data;

            if (Right is not null)
                foreach (var element in Right)
                    yield return element;
        }
    }
}