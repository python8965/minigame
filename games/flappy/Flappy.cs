using Godot;
using static Godot.Mathf;
using Godot.Collections;
using System;
using Array = Godot.Collections.Array;

public class Flappy : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export] 
    public PackedScene PillarScene;
    
    [Signal]
    public delegate void PlayerXChanged();

    private float _playerX;

    private FlappyPlayer _player;
    private ParallaxBackground _background;
    private Timer _spawnTimer;
    
    private bool _gameEnd = false;
    
    private ulong _startTime = Time.GetTicksMsec();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _player = GetNode<FlappyPlayer>("FlappyPlayer");
        _player.Connect("Hit", this, "OnPlayerHit");
        
        _spawnTimer = GetNode<Timer>("SpawnTimer");
        _spawnTimer.Connect("timeout", this, "SpawnTimerEnd");
        _spawnTimer.OneShot = true;
        
        _spawnTimer.Start(0.0f);
        
        _background = GetNode<ParallaxBackground>("ParallaxBackground");
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
    
        if (!_gameEnd)
        {
            _playerX -= delta * 1000;
            _background.ScrollOffset = new Vector2(_playerX, 0);
        }
    }

    

    public void SpawnPillar(bool isTop, float height)
    {
        float spawnY = 0;
        if (!isTop) spawnY = Global.Instance.ScreenSize.y - (height/2) + 200;
        
        Pillar pillar = (Pillar)PillarScene.Instance();

        pillar.PillarOffsetX = _playerX;

        pillar.Position = new Vector2(Global.Instance.ScreenSize.x,spawnY);
        pillar.GetPlayerX = () => _playerX;
        pillar.Height = height;
        
        AddChild(pillar);
        
    }

    public void SpawnTimerEnd()
    {
        Random random = new Random();
        SpawnPillar(random.Next(0,2) == 0, random.Next(500,800));
        
        _spawnTimer.Start((float)random.NextDouble()+0.5f);
    }

    public void OnPlayerHit()
    {
        _gameEnd = true;
        _spawnTimer.Stop();
        
        GetNode<Label>("Popup/Panel/HBoxContainer/Label").Text =
            $"game ends in {Time.GetTicksMsec() - _startTime}";
        GetNode<Popup>("Popup").Popup_();
    }
    
    public void OnGameExit()
    {
        Navigator.Instance.PopScene();
    }
}
