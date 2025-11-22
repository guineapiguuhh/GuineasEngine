using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public class Node : IUpdateable, IDrawable, IDisposable
{
    public string Name = string.Empty;

    public bool IsVisible { get; set; } = true;

    public Node() : this("Node") {}
    public Node(string name)
    {
        Name = name;
        Children = [];
    }

    #region Virtual methods
    public virtual void Update(float deltaTime)
    {
        ForEach((child) => child.Update(deltaTime));
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        ForEach((child) =>
        {
            if (child.IsVisible) child.Draw(spriteBatch);
        });
    }

    protected virtual void Dispose(bool disposing) {}
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public override string ToString()
    {
        return GetType().Name + $"({Name})";
    }
    #endregion

    #region Composition system
    public Node Parent { get; internal set; }
    private List<Node> Children { get; set; }
    public int ChildrenCount => Children.Count;

    public Node GetChild(int index) => Children[index];

    public Node GetChildByName(string name)
    {
        for (int i = 0; i < ChildrenCount; i++)
        {
            var child = GetChild(i);
            if (child.Name == name) return child;
        }
        return null;
    }

    public bool ContainsChild(Node child) => Children.Contains(child);

    public bool ContainsChildByName(string name) => Children.Contains(GetChildByName(name));

    public List<Node> GetChildren() => GetChildren(false);

    public List<Node> GetChildren(bool includeInternal)
    {
        if (!includeInternal) return Children;

        var internalChildren = new List<Node>();
        for (int i = 0; i < ChildrenCount; i++)
        {
            var child = GetChild(i);
            internalChildren.Add(child);

            var children = child.GetChildren(false);
            for (int ii = 0; ii < child.ChildrenCount; ii++)
                internalChildren.Add(children[ii]);
        }
        return internalChildren;
    }

    public void ForEach(Action<Node> action)
    {
        for (int i = 0; i < ChildrenCount; i++)
            action(GetChild(i));
    }

    public void AddChild(Node child) => InsertChild(ChildrenCount, child);

    public void InsertChild(int index, Node child)
    {
        if (child == this)
            throw new Exception("You can't add him as your own child.");
        
        child.Parent?.RemoveChild(child);
        child.Parent = this;
        Children.Insert(index, child);
    }

    public void RemoveChild(Node child)
    {
        if (!ContainsChild(child))
            throw new Exception("You can't remove something that doesn't contain it.");

        child.Parent = null;
        Children.Remove(child);   
    }
    #endregion
}