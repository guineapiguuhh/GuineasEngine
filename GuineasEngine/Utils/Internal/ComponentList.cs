using GuineasEngine.Utils.Collections;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Utils.Internal;

internal class ComponentList : IUpdateable, IDrawable
{
    Entity Entity;

    readonly FastList<Component> Members;

    public int Count => Members.Count;
    public Component this[int index] => Members[index];

    readonly List<Component> MembersToAdd;
    readonly List<Component> MembersToRemove;

    public ComponentList(Entity entity)
    {
        Entity = entity;
        Members = new FastList<Component>();
        MembersToAdd = [];
        MembersToRemove = [];
    }
    
    public void Clear()
    {
        Entity = null;
        for (int i = 0; i < Count; i++)
            Remove(Members[i]);
        QueueLists();
        Members.Clear();
    }

    public void Add(Component component)
    {
        MembersToAdd.Add(component);
    }

    public void Remove(Component component) 
    {
        MembersToAdd.Remove(component);
        MembersToRemove.Add(component);
    }

    public T Get<T>()
        where T : Component
    {
        for (int i = 0; i < Count; i++)
            if (Members[i] is T member) return member;
        return null;
    }

    public bool Has<T>() 
        where T : Component
    {
        for (int i = 0; i < Count; i++)
            if (Members[i] is T) return true;
        return false;
    }

    public void QueueLists()
    {
        if (MembersToRemove.Count > 0)
        {
            for (int i = 0; i < MembersToRemove.Count; i++)
            {
                var requested = MembersToRemove[i];
                requested.Entity = null;
                requested.Removed();
                Members.Remove(requested);
            }
            MembersToRemove.Clear();
        }
        
        if (MembersToAdd.Count > 0)
        {
            for (int i = 0; i < MembersToAdd.Count; i++)
            {
                var requested = MembersToAdd[i];
                requested.Entity = Entity;
                requested.Ready();
                Members.Add(requested);
            }
            MembersToAdd.Clear();
        }
    }

    public void Update(float deltaTime)
    {
        QueueLists();
        for (int i = 0; i < Count; i++)
            if (Members[i].IsActive) Members[i].Update(deltaTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Count; i++) 
            if (Members[i].IsVisible && Members[i].IsActive) Members[i].Draw(spriteBatch);
    }
}