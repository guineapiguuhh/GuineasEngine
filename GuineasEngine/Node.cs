using GuineasEngine.Components;
using Microsoft.Xna.Framework;

namespace GuineasEngine;

public class Node : Components.IUpdateable, Components.IDrawable, IDisposable
{
    public string Name = string.Empty;

    public Node() : this("Node") {}
    public Node(string name)
    {
        Name = name;
        Children = [];
    }

    #region Transform
    #region Global Transform
    public bool Independent { get; set; } = false;

    public Vector2 GlobalPosition { get; private set; }
    public Vector2 GlobalScale { get; private set; }
    public float GlobalAngle { get;  private set; }

    private void UpdateGlobalTransform()
    {
        if (Parent is null || Independent)
        {
            GlobalAngle = Angle;
            GlobalScale = Scale;
            GlobalPosition = Position;
            return;
        }

        GlobalAngle = Parent.GlobalAngle + Angle;
        GlobalScale = Parent.GlobalScale * Scale;

        var angle = Parent.GlobalAngle;

        var x = Position.X;
        var y = Position.Y;

        var sx = Parent.GlobalScale.X;
        var sy = Parent.GlobalScale.Y;

        var x2 = x * sx * float.Cos(angle) - y * sy * float.Sin(angle);
        var y2 = x * sx * float.Sin(angle) + y * sy * float.Cos(angle);

        GlobalPosition = Parent.GlobalPosition + new Vector2(x2, y2);
    }
    #endregion
    public Vector2 Position = Vector2.Zero;
    public Vector2 Origin = Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public float Angle { get; set; } = 0f;

    public bool IsVisible { get; set; } = true;

    public void LookAt(Vector2 value)
    {
        var dx = Position.X - value.X;
        var dy = Position.Y - value.Y;

        Angle = float.Atan2(dy, dx);
    }
    #endregion

    #region Virtual methods
    public virtual void Update(float deltaTime)
    {
        UpdateGlobalTransform();
        ForEach((child) => child.Update(deltaTime));
    }

    public virtual void Draw()
    {
        ForEach((child) =>
        {
            if (child.IsVisible) child.Draw();
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
    protected List<Node> Children { get; set; }
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