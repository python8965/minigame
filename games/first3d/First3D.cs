using Godot;
using System;

public class First3D : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void OnGameEndBodyEntered(Node body)
    {
        GD.Print(body);
        Navigator.Instance.PopScene();
    }
}
