using GuineasEngine.Components;
using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Systems;

public class SceneManager : IUpdateable, IDrawable
{
    public Scene Current { get; private set; }
    public Scene Next { get; private set; }

    public bool HasTransition => InTransition is not null || OutTransition is not null;

    public Transition InTransition;
    public Transition OutTransition;

    public void Reload()
    {
        Switch(Current);
    }

    public void Switch(Scene nextScene)
    {
        Next = nextScene;
        Unload();
        DoTransition();
    }

    protected void DoTransition()
    {
        if (!HasTransition)
        {
            Current = Next;
            Current?.Load();
            Next = null;
            return;
        }
    }

    public void Unload()
    {
        Current?.Dispose();
        GC.Collect();
    }

    public virtual void Update(float deltaTime)
    {
        Current?.Update(deltaTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Current?.Draw(spriteBatch);
    }
}