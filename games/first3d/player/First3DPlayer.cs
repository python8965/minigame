using Godot;
using System;

public class First3DPlayer : KinematicBody
{
    [Export]
    public int Speed = 500;
    public const float MinRotateY = -0.8f;
    public const float MaxRotateY = 0.8f;

    public float Gravity = 490f;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Vector3 _velocity = Vector3.Zero; // The player's movement vector.

    private SpringArm _springArm;

    
    private Vector2 _mouseInputBuffer = Vector2.Zero;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _springArm = GetNode<SpringArm>("SpringArm");
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      GD.Print(_springArm.GlobalRotation);
      Vector2 rotation = _mouseInputBuffer / 10;
      _mouseInputBuffer -= rotation;
      
      Rotate(Vector3.Up,Mathf.Deg2Rad(rotation.x/3) );
      
      if (_springArm.GlobalRotation.x > MaxRotateY && rotation.y < 0
          )
      {
          
      }else if (_springArm.GlobalRotation.x < MinRotateY && rotation.y > 0)
      {
          
      }
      else
      {
          _springArm.RotateObjectLocal(Vector3.Up, Mathf.Deg2Rad(rotation.y / 3));
      }
      
      //Transform = Transform.Orthonormalized();
  }

  public override void _PhysicsProcess(float delta)
  {
      base._PhysicsProcess(delta);
      MoveAndSlide(_velocity.Rotated(new Vector3(0,1,0),Rotation.y)*delta);

      
  }
  
  
  
  public override void _UnhandledInput(InputEvent @event)
  {
      base._UnhandledInput(@event);
        
      _velocity = Vector3.Zero;

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

      _velocity.y = -Gravity;

      if (@event is InputEventMouseMotion mouseMotion)
      {
          GD.Print();
          _mouseInputBuffer += mouseMotion.Relative;
          
          
      }
  }
}
