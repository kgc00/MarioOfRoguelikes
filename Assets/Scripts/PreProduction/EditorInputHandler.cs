using UnityEngine;
[System.Serializable]
public class EditorInputHandler : MonoBehaviour
{
    BoardCreator boardCreator;
    public void Initialize(BoardCreator _boardCreator)
    {
        this.boardCreator = _boardCreator;
    }

    private void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            boardCreator.MoveAndUpdateMarker(new Vector2Int(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            boardCreator.MoveAndUpdateMarker(new Vector2Int(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            boardCreator.MoveAndUpdateMarker(new Vector2Int(0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            boardCreator.MoveAndUpdateMarker(new Vector2Int(1, 0));
        }

    }

}