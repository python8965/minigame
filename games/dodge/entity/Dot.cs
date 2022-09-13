using Godot;
using System;

public class Dot : RigidBody2D
{

    private VisibilityNotifier2D _visibility;

    public override void _Ready()
    {
        _visibility = GetNode<VisibilityNotifier2D>("VisibilityNotifier2D");
        
        
    }

    public override void _PhysicsProcess(float delta)
    {
        // GD.Print(_visibility.IsOnScreen());
        // GD.Print(this.Position);
    }

    public override void _Draw()
    {
        base._Draw();
        
        DrawCircle(Vector2.Zero, 5.0f, Colors.Red);
    }

    public void OnPlayerHit()
    {
        Visible = false;
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
}
