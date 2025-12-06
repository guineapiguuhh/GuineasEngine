namespace GuineasEngine.Utils;

public enum TimerState
{
    Running,
    Stopped,
    Paused
}

public class Timer : IUpdateable
{
    private float _timeCounter = 0f;

    public event Action<Timer> OnStart;
    public event Action<Timer> OnProgress;
    public event Action<Timer> OnComplete;

    public event Action<Timer> OnCompleteLoop;

    public TimerState State { get; private set; } = TimerState.Stopped;

    public float Duration = 0f;
    public float Progress { get; private set; } = 0f;

    public int Loops = 0;
    public uint CurrentLoop { get; private set; } = 0;
    public uint LoopsLeft { get; private set; } = 0;

    public float Percent => Progress * 100f;

    public float CurrentTime => Duration * Progress;
    public float TimeLeft => Duration - CurrentTime;


    public Timer(float duration) : this(duration, 0) {}

    public Timer(float duration, int loops)
    {
        Duration = duration;
        Loops = loops;
    }

    public virtual void Start()
    {
        if (State != TimerState.Stopped) return;
        State = TimerState.Running;
        Progress = 0f;
        _timeCounter = 0f;
        LoopsLeft = (uint)Loops;
        CurrentLoop = 0;
        OnStart?.Invoke(this);
    }

    public virtual void Stop()
    {
        if (State != TimerState.Running) return;
        State = TimerState.Stopped;
        LoopsLeft = 0;
    }

    public virtual void Pause()
    {
        if (State != TimerState.Running) return;
        State = TimerState.Paused;
    }

    public virtual void Resume() 
    {
        if (State != TimerState.Paused) return;
        State = TimerState.Running;
    }

    public virtual void Update(float deltaTime)
    {
        if (State != TimerState.Running) return;
        _timeCounter += deltaTime;
        Progress = float.Min(_timeCounter / Duration, 1f);
        OnProgress?.Invoke(this);
        if (Progress >= 1f)
        {
            if (Loops < 0) 
            {
                Progress = 0f;
                _timeCounter = 0f;
                OnCompleteLoop?.Invoke(this);
                return;
            }
            else if (LoopsLeft > 0)
            {
                LoopsLeft--;
                CurrentLoop++;
                Progress = 0f;
                _timeCounter = 0f;
                OnCompleteLoop?.Invoke(this);
                return;
            }
            Stop();
            OnCompleteLoop?.Invoke(this);
            OnComplete?.Invoke(this);
            return;
        }
    }

    #region Static Methods
    public static Timer Start(float duration, int loops, Action<Timer> onStart, Action<Timer> onProgress, Action<Timer> onComplete)
    {
        var timer = new Timer(duration, loops);
        timer.OnStart += onStart;
        timer.OnProgress += onProgress;
        timer.OnComplete += onComplete;
        return timer;
    }

    public static Timer Wait(float duration, Action<Timer> onComplete)
    {
        var timer = new Timer(duration);
        timer.OnComplete = onComplete;
        return timer;
    }

    public static Timer Loop(float interval, Action<Timer> onComplete, Action<Timer> onCompleteLoop)
    {
        var timer = new Timer(interval);
        timer.OnComplete += onComplete; 
        timer.OnCompleteLoop += onCompleteLoop; 
        return timer;
    }
    #endregion
}