using System.Collections.Generic;
using System.Linq;
using Godot;

public class Navigator: AutoloadSingletonNode<Navigator>
{
    private Node _currentScene;

    private List<string> _sceneQueue = new List<string>();
    public override void _Ready()
    {
        base._Ready();
        Viewport root = GetTree().Root;
            
        _currentScene = root.GetChild(root.GetChildCount() - 1);
        if (_currentScene.Filename != string.Empty)
        {
            _sceneQueue.Add(_currentScene.Filename);
        }
    }

    public void PopScene()
    {
        if (_sceneQueue.Count < 1)
        {
            
            return;
        }
        _sceneQueue.RemoveAt(_sceneQueue.Count - 1);
        GotoScene(_sceneQueue.Last());
    }
        
        

    public void GotoScene(string path)
    {
        
        GD.Print($"going {path} , {_sceneQueue.Count}");
        CallDeferred(nameof(_DeferredGotoScene), path);
    }

    private void _DeferredGotoScene(string path)
    {
        // It is now safe to remove the current scene
        _currentScene.Free();

        // Load a new scene.
        PackedScene nextScene = (PackedScene)GD.Load(path);

        
        // Instance the new scene.
        _currentScene = nextScene.Instance();
        _sceneQueue.Add(_currentScene.Filename);
        // Add it to the active scene, as child of root.
        GetTree().Root.AddChild(_currentScene);

        // Optionally, to make it compatible with the SceneTree.change_scene() API.
        GetTree().CurrentScene = _currentScene;
    }
}