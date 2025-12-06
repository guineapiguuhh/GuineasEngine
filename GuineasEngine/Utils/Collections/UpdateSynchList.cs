namespace GuineasEngine.Utils.Collections;

public class UpdateSynchList<T> : IUpdateable
{
    protected FastList<T> Members;
    public int Count => Members.Count;

    protected List<KeyValuePair<int, T>> RequestedToInsert;
    protected List<T> RequestedToRemove;

    public T this[int index] => Members[index];

    public UpdateSynchList()
    {
        Members = new FastList<T>();
        RequestedToInsert = [];
        RequestedToRemove = [];
    }

    public virtual void Clear()
    {
        Members.Clear();
        RequestedToInsert.Clear();
        RequestedToRemove.Clear();
    }

    public void Contains(T item) => Members.Contains(item);

    public void Add(T item) => Insert(Count, item);

    public void Insert(int index, T item) => RequestedToInsert.Add(new KeyValuePair<int, T>(index, item));

    public void Remove(T item) => RequestedToRemove.Add(item);

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

    public virtual void Update(float deltaTime) => ResolveRequests();
}