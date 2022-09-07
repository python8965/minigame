using Godot;
using Godot.Collections;
using System;

public class TestMainSceneScript : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    
    ItemList _gameList;
    
    #pragma warning disable 0649
    [Export]
    private Array<PackedScene> games;
    #pragma warning restore 649
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _gameList = GetNode<ItemList>("ItemList");
        
        
        foreach (PackedScene game in games)
        {

            if (!(game is null))
            {
                _gameList.AddItem(game.ResourcePath);
            }
        }
    }

    public void OnStartButtonPressed()
    {
        if (!_gameList.IsAnythingSelected()) return;
        
        var path = games[_gameList.GetSelectedItems()[0]].ResourcePath;
        
        Navigator.Instance.GotoScene(path);
    }
}
