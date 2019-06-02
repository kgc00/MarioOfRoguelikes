using System.Collections.Generic;

class SwitchTrigger : BaseTrigger
{
    TileType closed;
    TileType open;

    public SwitchTrigger(SwitchTriggerData data)
    {
        this.closed = data.Closed;
        this.open = data.Open;
    }
    public void OnEnter(Unit unit, Tile tile)
    {
        // find doors FindTileByType()
        List<Tile> doors = tile.Board.FindTileByType(closed);
        // switch all doors
        foreach (Tile door in doors)
        {
            door.Board.PlaceTile(door.Position, open);
        }
    }
    public void OnLeave(Unit unit, Tile tile) { }
    public void StartTimer(Board board, Tile tile) { }
}