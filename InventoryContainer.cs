using Godot;
using System;

public class InventoryContainer : ScrollContainer
{
    public override void _Ready()
    {
        Theme invis = new Theme();
        StyleBoxEmpty empty = new StyleBoxEmpty();
        invis.SetStylebox("scroll", "VScrollBar", empty);
        GetVScrollbar().Theme = invis;
        
    }
}
