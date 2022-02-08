using Godot;
using System;

public class Overlay : Control
{
    public override void _Ready()
    {
        
    }

    public void onButtonClosePressed()
    {
        this.Visible = false;
    }

}
