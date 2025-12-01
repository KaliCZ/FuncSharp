namespace FuncSharp.Examples;

public class Tree<A> : Coproduct2<Node<A>, Leaf>
{
    public Tree(Node<A> node) : base(node) { }
    public Tree(Leaf leaf) : base(leaf) { }
}

public record Leaf;
public record Node<A>(A Value, Tree<A> Left, Tree<A> Right);

public static class TreeUtilities
{
    public static int LeafCount<A>(Tree<A> tree)
    {
        return tree.Match(
            node => LeafCount(node.Left) + LeafCount(node.Right),
            leaf => 1
        );
    }
}