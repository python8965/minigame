using Godot;
using System;

public class Pillar : RigidBody2D
{
    private VisibilityNotifier2D _visibility;
    private CollisionShape2D _collision;

    public float Height = 0.0f;
    public float Width = 100.0f;

    public float PillarOffsetX = 0.0f;

    public Func<float> GetPlayerX;
    

    public override void _Ready()
    {
        _visibility = GetNode<VisibilityNotifier2D>("VisibilityNotifier2D");
        _collision = GetNode<CollisionShape2D>("CollisionShape2D");
        _collision.Shape = new RectangleShape2D{Extents = new Vector2(Width/2, Height/2)};
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        
        float PlayerX = GetPlayerX();
        Position = new Vector2(Global.Instance.ScreenSize.x - ( PillarOffsetX-PlayerX),Position.y);
    }

    public override void _Draw()
    {
        base._Draw();
        Vector2 ext = ((RectangleShape2D)_collision.Shape).Extents;
        DrawRect(new Rect2(-ext, Width, Height),  Colors.Black);
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
}
