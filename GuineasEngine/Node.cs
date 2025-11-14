using GuineasEngine.Components;

namespace GuineasEngine;

public class Node : IUpdateable, IDrawable
{
    public string Name = "Node";

    internal Node Parent { get; set; }

    protected List<Node> Children { get; set; }

    public Node()
    {
        Children = [];
    }

    public Node GetParent()
    {
        return Parent;
    }

    public void AddChild(Node child)
    {
        child.Parent = this;
        Children.Add(child);
    }

    public void RemoveChild(Node child)
    {
        child.Parent = null;
        Children.Remove(child);
    }

    public virtual void Update(float deltaTime)
    {
        for (int i = 0; i < Children.Count; i++) Children[i].Update(deltaTime);
    }

    public virtual void Draw()
    {
        for (int i = 0; i < Children.Count; i++) Children[i].Draw();
    }
    
    public override string ToString()
    {
        return Name;
    }
}