namespace AStar.AdHocCollections;

public partial class StateTree<T> {
    private static void Rebalance(ref Node node) {
        var balanceFactor = GetHeight(node.Left) - GetHeight(node.Right);
        switch (balanceFactor) {
            case > 1:
                if (GetHeight(node.Left!.Left) > GetHeight(node.Left.Right))
                    RotateRight(ref node);
                else
                    RotateLeftRight(ref node);
                break;
            case < -1:
                if (GetHeight(node.Right!.Right) > GetHeight(node.Right.Left))
                    RotateLeft(ref node);
                else
                    RotateRightLeft(ref node);
                break;
            default:
                node.UpdateHeight();
                break;
        }
    }

    private static void RotateRight(ref Node node) {
        var l = node.Left!;
        var lr = l.Right;
        l.Right = node;
        node.Left = lr;
        node.UpdateHeight();
        l.UpdateHeight();
        node = l;
    }

    private static void RotateLeft(ref Node node) {
        var r = node.Right!;
        var rl = r.Left;
        r.Left = node;
        node.Right = rl;
        node.UpdateHeight();
        r.UpdateHeight();
        node = r;
    }

    private static void RotateLeftRight(ref Node node) {
        RotateLeft(ref node.Left!);
        RotateRight(ref node);
    }

    private static void RotateRightLeft(ref Node node) {
        RotateRight(ref node.Right!);
        RotateLeft(ref node);
    }
}