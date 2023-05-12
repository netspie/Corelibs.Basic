namespace Corelibs.Basic.Collections;

public static class TreeExtensions
{
    public static TNode FindOrDefault<TNode, TProperty>(
        this TNode node,
        TProperty property,
        Func<TNode, IEnumerable<TNode>> getChildren,
        Func<TNode, TProperty> getProperty)
        where TNode : class
        where TProperty : class
    {
        if (node == default || property == default)
            return default;

        if (getProperty(node) == property)
            return node;

        foreach (var child in getChildren(node))
            if (getProperty(child) == property) 
                return child;

        return default;
    }
}
