using Microsoft.Xna.Framework.Graphics;

namespace GuineasEngine;

public enum TransitionState
{
    Playing,
    Stopped
}

public abstract class Transition : IUpdateable, IDrawable
{
    private float _transitionCounter = 0f;

    public event Action<Transition> OnStart;
    public event Action<Transition> OnProgress;
    public event Action<Transition> OnComplete;

    public TransitionState State { get; private set; } = TransitionState.Stopped;

    public float Duration = 0f;
    public float Progress { get; private set; } = 0f;

    public bool Reverse = false;

    public virtual void Load() {}
    public virtual void Unload() {}

    public virtual void Start()
    {
        if (State != TransitionState.Stopped) return;
        State = TransitionState.Playing;
        OnStart?.Invoke(this);
    }

    public virtual void Stop()
    {
        if (State != TransitionState.Playing) return;
        State = TransitionState.Stopped;
        _transitionCounter = 0f;
        Progress = 0f;
    }

    public virtual void Update(float deltaTime)
    {
        if (State != TransitionState.Playing) return;
        _transitionCounter += deltaTime;

        Progress = float.Min(_transitionCounter / Duration, 1f);
        if (Reverse) Progress = 1f - Progress;
        if ((!Reverse && Progress >= 1f) || (Reverse && Progress <= 0f))
        {
            Stop();
            OnComplete?.Invoke(this);
            return;
        }
        OnProgress?.Invoke(this);
    }

    public virtual void Draw(SpriteBatch spriteBatch) {}

    public Transition Clone()
        => MemberwiseClone() as Transition;
}