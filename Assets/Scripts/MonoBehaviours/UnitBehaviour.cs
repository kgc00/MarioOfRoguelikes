using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
[System.Serializable]
public class UnitBehaviour : MonoBehaviour {
    private SpriteRenderer spriteRenderer;

    private void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    public void UpdatePosition (Vector2Int position) {
        transform.position = new Vector3 (position.x, position.y, -1);
    }

    public void UpdateSprite (Sprite sprite) {
        spriteRenderer.sprite = sprite;
    }

    public void MatchColorToEnergy (float normalizedSize) {
        // spriteRenderer.GetComponentInParent<Transform>()
        // .localScale = new Vector3(normalizedSize, normalizedSize, normalizedSize);
    }

    public void MatchSizeToEnergy (float normalizedSize) {
        spriteRenderer.GetComponentInParent<Transform> ()
            .localScale = new Vector3 (normalizedSize, normalizedSize, normalizedSize);
    }
}