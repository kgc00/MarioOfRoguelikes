using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
public class TileBehaviour : MonoBehaviour {
    private Tile tile;
    private SpriteRenderer spriteRenderer;

    private void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    private void Start () {
        RefreshSprite ();
    }

    public void SetTile (Tile t) {
        tile = t;
        transform.position = new Vector3 (tile.Position.x, tile.Position.y, 0);
    }

    public void RefreshSprite () {
        spriteRenderer.sprite = tile.Type.Image;
    }

}