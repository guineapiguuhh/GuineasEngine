using GuineasEngine.Utils.Collections;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Utils.Internal;

internal class EntityList : IUpdateable, IDrawable
{
    protected FastList<Entity> Members;
    public int Count => Members.Count;

    protected List<KeyValuePair<int, Entity>> RequestedToInsert;
    protected List<Entity> RequestedToRemove;

    public Entity this[int index] => Members[index];

    public EntityList()
    {
        Members = new FastList<Entity>();
        RequestedToInsert = [];
        RequestedToRemove = [];
    }

    public virtual void Clear()
    {
        Members.Clear();
        RequestedToInsert.Clear();
        RequestedToRemove.Clear();
    }

    public int IndexOf(Entity item)
    {
        for (int i = 0; i < Members.Count; i++)
        {
            if (Members[i] == item) return i;
        }
        return -1;
    }

    public void Contains(Entity item) => Members.Contains(item);

    public void Add(Entity item) => Insert(Count, item);

    public void Insert(int index, Entity item) => RequestedToInsert.Add(new KeyValuePair<int, Entity>(index, item));

    public void Remove(Entity item) => RequestedToRemove.Add(item);

    public void ResolveRequests()
    {
        if (RequestedToInsert.Count > 0)
        {
            for (int i = 0; i < RequestedToInsert.Count; i++)
            {
                var requested = RequestedToInsert[i];
                Members.Insert(requested.Key, requested.Value);
            }
            RequestedToInsert.Clear();
        }

        if (RequestedToRemove.Count > 0)
        {
            for (int i = 0; i < RequestedToRemove.Count; i++)
            {
                var requested = RequestedToRemove[i];
                Members.Remove(requested);
            }
            RequestedToRemove.Clear();
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

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Members.Count; i++)
        {
            Members[i].Draw(spriteBatch);
        }
    }
}