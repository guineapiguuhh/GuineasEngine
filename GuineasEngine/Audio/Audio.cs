using Microsoft.Xna.Framework.Audio;

namespace GuineasEngine.Audio;

public class Audio : Node
{
    public static float MasterVolume { get; set; } = 1f;

    public SoundEffectInstance Instance { get; private set; }
    public SoundState State => Instance.State;

    public float Volume
    {
        get => Instance.Volume;
        set => Instance.Volume = value;
    }

    public float Pitch
    {
        get => Instance.Pitch;
        set => Instance.Pitch = value;
    }

    public float Pan
    {
        get => Instance.Pan;
        set => Instance.Pan = value;
    }

    public bool IsLooped 
    {
        get => Instance.IsLooped;
        set => Instance.IsLooped = value;
    }

    public Audio(SoundEffect sound) : this("Audio", sound) {}
    public Audio(string name, SoundEffect sound) : base(name)
    {
        Instance = sound.CreateInstance();
    }

    public void Play()
    {
        Play(1f, 0f, 0f, false);
    }
    public void Play(float volume, float pitch, float pan, bool isLooped)
    {
        Instance.Play();
        Instance.Volume = MasterVolume * volume;
        Instance.Pitch = pitch;
        Instance.Pan = pan;
        Instance.IsLooped = isLooped;
    }
    
    public void Stop()
    {
        Stop(true);
    }
    public void Stop(bool immediate)
    {
        Instance.Stop(immediate);
    }

    public void Resume()
    {
        Instance.Resume();
    }

    public void Pause()
    {
        Instance.Pause();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Instance.Dispose();
        }
    }

    public static Audio FromFile(string path)
    {
        return new Audio(SoundEffect.FromFile(path));
    }
}