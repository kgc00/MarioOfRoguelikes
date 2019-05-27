using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class UnitBehaviour : MonoBehaviour
{
    private Unit unit;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = unit.Type.Image;
    }

    void Update()
    {
        unit.Tick();
        transform.position = new Vector3(unit.Position.x, unit.Position.y, -1);
    }

    public void SetUnit(Unit u)
    {
        unit = u;
    }
}
