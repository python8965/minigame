using Godot;
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
// ReSharper disable once InconsistentNaming
// ReSharper disable once UnusedType.Global
public class Dodge : Node2D
{
    
    [Export] 
    public PackedScene DotScene;

    private Timer _dotSpawnTimer;
    private Player _player;
    private Label _timeLabel;
    private bool _gameEnd = false;
    private readonly ulong _startTime = Time.GetTicksMsec();

    public override void _Ready()
    {
        base._Ready();
        GD.Randomize();

        _player = GetNode<Player>("Player");
        _timeLabel = GetNode<Label>("TimeLabel");

        _dotSpawnTimer = GetNode<Timer>("Timer");
        
        _dotSpawnTimer.Connect("timeout", this, "_on_Timer_timeout");
        _dotSpawnTimer.Start();
        
        
        _player.Connect("Hit", this, "OnPlayerHit");
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (!_gameEnd) _timeLabel.Text = $"time elapsed : {Time.GetTicksMsec() - _startTime}";
    }
    public void spawnDots()
    {
        Dot dot = (Dot)DotScene.Instance();
        _player.Connect("Hit", dot, "OnPlayerHit");
        
        PathFollow2D dotSpawnLocation = GetNode<PathFollow2D>("SpawnPath/SpawnLocation");
        dotSpawnLocation.Offset = GD.Randi();

        // Set the mob's direction perpendicular to the path direction.
        float direction = dotSpawnLocation.Rotation + Mathf.Pi / 2;
        // Set the mob's position to a random location.
        dot.Position = dotSpawnLocation.Position;

        // Add some randomness to the direction.
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        dot.Rotation = direction;

        // Choose the velocity.
        Vector2 velocity = new Vector2((float)GD.RandRange(250.0, 500.0), 0);
        dot.LinearVelocity = velocity.Rotated(direction);
        // Spawn the mob by adding it to the Main scene.
        AddChild(dot);
    }

    public void OnPlayerHit()
    {
        _gameEnd = true;
        _dotSpawnTimer.Stop();

        GetNode<Label>("Popup/Panel/HBoxContainer/Label").Text =
            $"game ends in {Time.GetTicksMsec() - _startTime}";
        GetNode<Popup>("Popup").Popup_();
        
        
    }

    public void OnGameExit()
    {
        Navigator.Instance.PopScene();
    }

    public void _on_Timer_timeout()
    {
        _dotSpawnTimer.WaitTime -= (_dotSpawnTimer.WaitTime / 50);
        spawnDots();
        _dotSpawnTimer.Start();
    }
}
