using Godot;
using System;

public class First3DPlayer : KinematicBody
{
    [Export]
    public int Speed = 500; 
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Vector3 _velocity = Vector3.Zero; // The player's movement vector.

    private float _rotateVelocity = 0;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      
  }

  public override void _PhysicsProcess(float delta)
  {
      base._PhysicsProcess(delta);
      MoveAndSlide(_velocity.Rotated(new Vector3(0,1,0),Rotation.y)*delta);
      RotateY(_rotateVelocity * delta);
  }
  
  public override void _UnhandledInput(InputEvent @event)
  {
      base._UnhandledInput(@event);
        
      _velocity = Vector3.Zero;
      _rotateVelocity = 0;

      if (Input.IsActionPressed("move_right"))
      {
          _velocity.z += 1;
      }

      if (Input.IsActionPressed("move_left"))
      {
          _velocity.z -= 1;
      }

      if (Input.IsActionPressed("move_down"))
      {
          _velocity.x -= 1;
      }

      if (Input.IsActionPressed("move_up"))
      {
          _velocity.x += 1;
      }

      if (_velocity.Length() > 0)
      {
          _velocity = _velocity.Normalized() * Speed;
      }

      if (Input.IsKeyPressed((int)KeyList.Q))
      {
          _rotateVelocity = 1;
      }
      
      if (Input.IsKeyPressed((int)KeyList.E))
      {
          _rotateVelocity = -1;
      }
  }
}
