using Godot;
using System;

public class Inventory : GridContainer
{
    [Export]
    public int itemSize = 120;

    [Export]
    public int displayItems = 133;

    public override void _Ready()
    {
        updateSlots();
    }

    bool swiping = false;
    Vector2 swipeStart;

    public void updateSlots(){
        // item gen.
        for (int i = 0; i < displayItems; i++)
        {
            TextureRect itm = new TextureRect();
            itm.Expand = true;
            itm.RectMinSize = new Vector2(itemSize, itemSize);
            itm.Texture = (Texture) ResourceLoader.Load("res://icon.png");
            AddChild(itm);
        }
    }

    public override void _Input(InputEvent @event)
    {
        ScrollContainer invContainer = (ScrollContainer) GetNode("/root/InventoryMain/InventoryContainer");
        Control over = (Control) GetNode("/root/InventoryMain/Overlay");
        Label lbl = (Label) over.GetNode("BackGround/Label");

        if (over.Visible)
            return;
        
        if (@event is InputEventMouseButton eventMouseButton){
            if (@event.IsPressed()){
                swiping = true;
                swipeStart = eventMouseButton.Position;
            } 
            else
            {
                if((eventMouseButton.Position.y >= invContainer.RectGlobalPosition.y &&
                    eventMouseButton.Position.y <= invContainer.RectGlobalPosition.y + invContainer.RectSize.y) &&
                    eventMouseButton.ButtonIndex == (int) ButtonList.Left){
                        float delta = Math.Abs(eventMouseButton.Position.y - swipeStart.y);
                        if (delta <= 10)
                        {
                            Vector2 mp = (MakeInputLocal(@event) as InputEventMouseButton).Position;
                            int id = getIDbyPT(mp);
                            if (id >= 0 && id <= displayItems)
                            {
                                over.Visible = true;
                                lbl.Text = "Item " + id.ToString() + " was pressed!";
                            }
                        }
                    }
            }
        }
        

    }

    private int getIDbyPT(Vector2 mp)
    {
        int iCol = (int) Math.Floor(mp.x / itemSize);
        int iRow = (int) Math.Floor(mp.y / itemSize);
        return iRow * Columns + iCol;
    }
}
