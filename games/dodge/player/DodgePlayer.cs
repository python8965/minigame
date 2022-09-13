using Godot;
using System;

public class DodgePlayer : Area2D
{
    [Export]
    public int Speed = 500; 
    
    [Signal]
    public delegate void Hit();
    // Called when the node enters the scene tree for the first time.
    private Vector2 _velocity = Vector2.Zero; // The player's movement vector.
    private CollisionShape2D _collision;
    public override void _Ready()
    {
        _collision = GetNode<CollisionShape2D>("Collision");
        
        Position = Global.Instance.ScreenSize / 2;
    }

    public override void _Draw()
    {
        base._Draw();
        RectangleShape2D rectangleShape2D =(RectangleShape2D)_collision.Shape;

        Rect2 rect = new Rect2{ Position = -rectangleShape2D.Extents,End = rectangleShape2D.Extents};
        
        DrawRect(rect,Colors.Aqua);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        
        
        Position += _velocity * delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0, Global.Instance.ScreenSize.x),
            y: Mathf.Clamp(Position.y, 0, Global.Instance.ScreenSize.y)
        );
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        
        _velocity = Vector2.Zero;

        if (Input.IsActionPressed("move_right"))
        {
            _velocity.x += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            _velocity.x -= 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            _velocity.y += 1;
        }

        if (Input.IsActionPressed("move_up"))
        {
            _velocity.y -= 1;
        }
        
        if (_velocity.Length() > 0)
        {
            _velocity = _velocity.Normalized() * Speed;
        }
    }

    public void OnPlayerBodyEntered(PhysicsBody2D body)
    {
        Hide(); // Player disappears after being hit.
        EmitSignal(nameof(Hit));
        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("Collision").SetDeferred("disabled", true);
    }
}
