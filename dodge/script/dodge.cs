using Godot;
using System;

// ReSharper disable once CheckNamespace
// ReSharper disable once InconsistentNaming
// ReSharper disable once UnusedType.Global
public class dodge : Node2D
{
    private Player _player = new Player();
    private var interval = () => {
    };

    public class Player : CollisionShape2D
    {
        private double X { get; set; } 
        private double Y { get; set; }
    }

    public class Dot : CollisionShape2D
    {
        private double X { get; set; } 
        private double Y { get; set; }
        
    }
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddChild(_player);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
    }
}
