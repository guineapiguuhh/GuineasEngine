using System.Text.Json;

namespace GuineasEngine.Utils;

public class Save<T> : ICloneable
    where T : struct
{
    public readonly string FileName;
    public readonly string SavePath;

    public readonly string FullPath;

    public T Data;

    public Save(string fileName, string package)
        : this(fileName, package, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) { }
        
    public Save(string fileName, string package, string savePath)
    {
        FileName = fileName;
        SavePath = Path.Join(savePath, package);
        if (!Directory.Exists(SavePath)) Directory.CreateDirectory(SavePath);

        FullPath = Path.Join(SavePath, FileName);
        if (!File.Exists(FullPath)) Clear();
        else Read();
    }

    public void Clear()
    {
        Data = new T();
        Flush();
    }

    public void Flush()
    {
        string content = JsonSerializer.Serialize(Data);
        File.WriteAllText(FullPath, content);
    }
    
    public T Read()
    {
        var content = File.ReadAllText(FullPath);
        Data = JsonSerializer.Deserialize<T>(content);
        return Data;
    }

    public object Clone()
    {
        return new Save<T>(FileName, null, SavePath)
        {
            Data = Data
        };
    }
}