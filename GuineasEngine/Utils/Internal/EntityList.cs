using GuineasEngine.Utils.Collections;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Utils.Internal;

internal class EntityList : IUpdateable, IDrawable
{
    Scene Scene;

    readonly FastList<Entity> Members;

    public int Count => Members.Count;
    public Entity this[int index] => Members[index];

    public EntityList(Scene scene)
    {
        Scene = scene;
        Members = new FastList<Entity>();
    }

    public void Clear()
    {
        Scene = null;
        Members.Clear();
    }

    public int? IndexOf(Entity entity)
    {
        for (int i = 0; i < Count; i++)
        {
            if (Members[i] == entity) 
                return i;
        }
        return null;
    }

    public void Add(Entity entity) => Insert(Count, entity);

    public void Insert(int index, Entity entity)
    {
        entity.QueueComponents();
        entity.Scene = Scene;
        Members.Insert(index, entity);
    }

    public void Remove(Entity entity) 
    {
        entity.ClearComponents();
        entity.Scene = null;
        Members.Remove(entity);
    }

    public void Update(float deltaTime)
    {
        for (int i = 0; i < Count; i++)
            if (Members[i].IsActive) Members[i].Update(deltaTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Count; i++)
            if (Members[i].IsVisible && Members[i].IsActive) Members[i].Draw(spriteBatch);
    }
}