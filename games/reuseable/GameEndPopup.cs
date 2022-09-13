using Godot;
using System;

public class GameEndPopup : Popup
{
    [Signal]
    public delegate void OnExit();

    public void OnButtonPressed()
    {
        EmitSignal(nameof(OnExit));
    }
}
