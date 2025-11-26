using System.Runtime.CompilerServices;
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
        for (int i = 0; i < Members.Count; i++)
        {
            Members[i].Entity = null;
        }
        Members.Clear();
        MembersToAdd.Clear();
        MembersToRemove.Clear();
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get<T>()
        where T : Component
    {
        for (int i = 0; i < Count; i++)
        {
            if (Members[i] is T member) return member;
        }
        return null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>() 
        where T : Component
    {
        for (int i = 0; i < Count; i++)
        {
            if (Members[i] is T) return true;
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void QueueMembers()
    {
        QueueMembersToRemove();
        QueueMembersToAdd();
    }

    void QueueMembersToRemove()
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
    }

    void QueueMembersToAdd()
    {
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update(float deltaTime)
    {
        QueueMembers();
        for (int i = 0; i < Count; i++)
        {
            Members[i].Update(deltaTime);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Count; i++)
        {
            Members[i].Draw(spriteBatch);
        }
    }
}