namespace GuineasEngine.Components;

// TODO: implementar o sistema de transição

public enum TransitionState
{
    Playing,
    Ended
}

public abstract class Transition : Node
{
    public abstract float Duration { get; set; }

    public float Progress { get; private set; } = 0f;

    public TransitionState State { get; private set; } = TransitionState.Ended;

    public virtual void Start()
    {
        if (State == TransitionState.Playing) return;

        Progress = 0f;
        State = TransitionState.Playing;
    }

    public virtual void End()
    {
        if (State == TransitionState.Ended) return;

        Progress = 0f;
        State = TransitionState.Ended;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (State != TransitionState.Playing) return;
        Progress += deltaTime;
        if (Progress > Duration) End();
    }
}