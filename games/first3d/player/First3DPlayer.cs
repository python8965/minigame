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
    private CollisionShape _body;


    private Vector2 _mouseInput = Vector2.Zero;
    private Vector2 _mouseInputDelayed = Vector2.Zero;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _springArm = GetNode<SpringArm>("SpringArm");
        _body = GetNode<CollisionShape>("CollisionShape");

        Camera cam = GetNode<Camera>("SpringArm/Camera");
    }
    
  public override void _Process(float delta)
  {
      GD.Print(_velocity);
      Vector2 rotation = _mouseInput / 20;
      Vector2 bodyRotation = _mouseInputDelayed / 10;
      
      _mouseInput -= rotation;
      _mouseInputDelayed -= bodyRotation;
      
      _springArm.GlobalRotate(Vector3.Down,Mathf.Deg2Rad(rotation.x/3) );
      _body.GlobalRotate(Vector3.Down,Mathf.Deg2Rad(bodyRotation.x/3) );
      
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
      GD.Print(_velocity);
      MoveAndSlide(_velocity.Rotated(new Vector3(0,1,0),_body.Rotation.y)*delta);
  }
  
  
  
  public override void _UnhandledInput(InputEvent @event)
  {
      base._UnhandledInput(@event);
        
      _velocity = Vector3.Zero;

      if (Input.IsActionPressed("move_right"))
      {
          _velocity.x -= 1;
      }

      if (Input.IsActionPressed("move_left"))
      {
          _velocity.x += 1;
      }

      if (Input.IsActionPressed("move_down"))
      {
          _velocity.z -= 1;
      }

      if (Input.IsActionPressed("move_up"))
      {
          _velocity.z += 1;
      }

      if (_velocity.Length() > 0)
      {
          _velocity = _velocity.Normalized() * Speed;
      }

      _velocity.y = -Gravity;

      if (@event is InputEventMouseMotion mouseMotion)
      {
          _mouseInput += mouseMotion.Relative;
          _mouseInputDelayed += mouseMotion.Relative;
      }
  }
}
