using GuineasEngine.Utils.Collections;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Utils.Internal;

internal class ComponentList : IUpdateable, IDrawable
{
    protected Entity Entity;

    protected FastList<Component> Members;
    public int Count => Members.Count;

    protected List<Component> RequestedToAdd;
    protected List<Component> RequestedToRemove;

    public ComponentList(Entity entity)
    {
        Entity = entity;
        Members = new FastList<Component>();
        RequestedToAdd = [];
        RequestedToRemove = [];
    }

    public virtual void Clear()
    {
        Entity = null;
        Members.Clear();
        RequestedToAdd.Clear();
        RequestedToRemove.Clear();
    }

    public void Add(Component item) 
    {
        item.Entity = Entity;
        RequestedToAdd.Add(item);
    }

    public void Remove(Component item) 
    {
        if (item.Entity != Entity) return;
        item.Entity = null;
        RequestedToAdd.Remove(item);
        RequestedToRemove.Add(item);
    }

    public T Get<T>()
        where T : Component
    {
        for (int i = 0; i < Members.Count; i++)
        {
            if (Members[i] is T member) return member;
        }
        return null;
    }

    public bool Has<T>()
        where T : Component
    {
        for (int i = 0; i < Members.Count; i++)
        {
            if (Members[i] is T) return true;
        }
        return false;
    }

    public void ResolveRequests()
    {
        if (RequestedToRemove.Count > 0)
        {
            for (int i = 0; i < RequestedToRemove.Count; i++)
            {
                var requested = RequestedToRemove[i];
                requested.Removed();
                Members.Remove(requested);
            }
            RequestedToRemove.Clear();
        }

        if (RequestedToAdd.Count > 0)
        {
            for (int i = 0; i < RequestedToAdd.Count; i++)
            {
                var requested = RequestedToAdd[i];
                requested.Ready();
                Members.Add(requested);
            }
            RequestedToAdd.Clear();
        }
    }

    public virtual void Update(float deltaTime) 
    {
        ResolveRequests();
        for (int i = 0; i < Members.Count; i++)
        {
            Members[i].Update(deltaTime);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Members.Count; i++)
        {
            Members[i].Draw(spriteBatch);
        }
    }
}