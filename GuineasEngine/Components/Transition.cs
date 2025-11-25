using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine.Components;

public enum TransitionState
{
    Playing,
    Paused,
    Stopped
}

public abstract class Transition : IUpdateable, IDrawable
{
    public event EventHandler OnPlay;
    public event EventHandler OnStop;

    public float Duration { get; set; } = 0f;
    public float Progress { get; private set; } = 0f;

    public bool Reverse { get; set; } = false;

    public TransitionState State { get; private set; } = TransitionState.Stopped;

    public virtual void Load() {}
    public virtual void Unload() {}

    public virtual void Play()
    {
        if (State == TransitionState.Playing) return;

        State = TransitionState.Playing;
        Progress = Reverse ? Duration : 0f;

        OnPlay?.Invoke(this, EventArgs.Empty);
    }

    public virtual void Stop()
    {
        if (State == TransitionState.Stopped) return;

        State = TransitionState.Stopped;
        Progress = 0f; // Best QOL ever

        OnStop?.Invoke(this, EventArgs.Empty);
    }

    public virtual void Resume()
    {
        if (State == TransitionState.Stopped) return;
        State = TransitionState.Playing;
    }

    public virtual void Pause()
    {
        if (State != TransitionState.Playing) return;
        State = TransitionState.Paused;
    }

    public virtual void Update(float deltaTime)
    {
        if (State != TransitionState.Playing) return;
        if (Reverse)
        {
            Progress -= deltaTime;
            if (Progress <= 0f) Stop();
            return;
        }
        Progress += deltaTime;
        if (Progress >= Duration) Stop();
    }

    public virtual void Draw(SpriteBatch spriteBatch) {}
}