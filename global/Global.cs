using Godot;
using Godot.Collections;
using System;
using Array = Godot.Collections.Array;
using Time = Godot.Time;

public class Global : AutoloadSingletonNode<Global>
{
    public Vector2 ScreenSize => GetViewport().Size;

    public override void _EnterTree()
    {
        base._EnterTree();
    }
}

class Logger
{
    internal enum LogLevel
    {
        Verbose,Info,Debug,Error
    }
    private const string escape = "0x1b";
    private const string AnsiColorRed = "\\e[31m";
    private const string AnisColorReset = "\\e[0m";

    public static void DebugPrint(LogLevel logLevel,params object[] what)
    {
        
        
        var time = Time.GetTimeStringFromSystem(false);

        var logLevelText = "";

        switch (logLevel)
        {
            case LogLevel.Verbose:
                logLevelText = "VERBOSE";
                break;
            case LogLevel.Info:
                logLevelText = "INFO";
                break;
            case LogLevel.Debug:
                logLevelText = "DEBUG";
                break;
            case LogLevel.Error:
                logLevelText = $"{AnsiColorRed}ERROR{AnisColorReset}";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
        }
        

        GD.Print($"[{logLevelText}] {time} ",what);
    }
    
    public static void DebugPrint(params object[] what)
    {
        var time = Time.GetTimeStringFromSystem(false);
        GD.Print($"{time} ",what);
    }
}
