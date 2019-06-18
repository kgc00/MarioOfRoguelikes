using UnityEngine;
using System.Collections.Generic;

public class CameraZoom : MonoBehaviour
{
    public void CenterAndZoom()
    {
        LevelData levelData = FindObjectOfType<BoardBehaviour>().LevelData;
        Camera cam = GetComponent<Camera>();
        Transform transform = GetComponent<Transform>();

        Vector2Int min = SetMinV2Int(levelData);

        Vector2Int max = SetMaxV2Int(levelData);
        // Debug.Log("min: " + min.ToString());
        // Debug.Log("max: " + max.ToString());

        // min:
        // (-2, 0)
        // max:
        // (2, 7)
        // difference:
        // (4, 7)

        // each tile is 1 unit

        // Debug.Log("dif: " + (max - min));

        // set cam position
        cam.transform.position = new Vector3((float)max.x / 2,
        (float)max.y / 2,
        cam.transform.position.z
        );
        // cam.Size = max.x / 2;
    }

    private Vector2Int SetMinV2Int(LevelData ld)
    {
        int minX = 99;
        int minY = 99;

        for (int i = 0; i < ld.tiles.Count; i++)
        {
            if (ld.tiles[i].location.x < minX)
            {
                minX = ld.tiles[i].location.x;
            }
            if (ld.tiles[i].location.y < minY)
            {
                minY = ld.tiles[i].location.y;
            }
        }
        return new Vector2Int(minX, minY);
    }

    private Vector2Int SetMaxV2Int(LevelData ld)
    {
        int maxX = -99;
        int maxY = -99;

        for (int i = 0; i < ld.tiles.Count; i++)
        {
            if (ld.tiles[i].location.x > maxX)
            {
                maxX = ld.tiles[i].location.x;
            }
            if (ld.tiles[i].location.y > maxY)
            {
                maxY = ld.tiles[i].location.y;
            }
        }
        return new Vector2Int(maxX, maxY);
    }
}