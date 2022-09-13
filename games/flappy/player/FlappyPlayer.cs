using Godot;
using System;

public class FlappyPlayer : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal]
    private delegate void Hit();
    
    private Vector2 _velocity = Vector2.Zero;

    private float factor = Global.Instance.ScreenSize.y / 10;
    private Timer _timer;

    private bool _canJump = true;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _timer = GetNode<Timer>("JumpTimer");
        _timer.Connect("timeout", this, "JumpTimerEnd");
        _timer.OneShot = true;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        
        _velocity += new Vector2(0, delta * 9.8f * factor);
        
        Position += _velocity * delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0, Global.Instance.ScreenSize.x),
            y: Mathf.Clamp(Position.y, 0, Global.Instance.ScreenSize.y)
        );
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);

        if (Input.IsActionPressed("jump") && _canJump)
        {
            _canJump = false;
            _velocity.y = -7 * factor;
            _timer.Start(0.8f);
        }
    }

    public void JumpTimerEnd()
    {
        _canJump = true;
    }
    
    public void OnPlayerBodyEntered(PhysicsBody2D body)
    {
        Hide(); // Player disappears after being hit.
        EmitSignal(nameof(Hit));
        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("Collision").SetDeferred("disabled", true);
    }
}
