using GuineasEngine;
using GuineasEngine.Components;

namespace Template;

public class FirstScene : Scene
{
    public override void Load()
    {
        System.Console.WriteLine("Hello, World!");
    }
}

public class Game1() : Core("Template", new FirstScene(), 400, 350, false);
