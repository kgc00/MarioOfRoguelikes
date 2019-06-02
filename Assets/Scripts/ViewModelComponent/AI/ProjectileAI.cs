
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ProjectileAI : BaseMonsterAI, BaseAI
{
    Vector2Int direction;

    public ProjectileAI()
    {
        direction = new Vector2Int(1, 0);
        NotificationCenter.AddListener<ActionResultNotification>(OnActionResultNotification);
    }

    public void ChangeDirection(Vector2Int direction)
    {
        this.direction = direction;
    }

    public Action TakeTurn()
    {
        if (MustWait)
        {
            return null;
        }

        return new MoveAction(direction, this, true);
    }

    public void OnActionResultNotification(ActionResultNotification notification)
    {
        if (notification.AI == this)
        {
            BaseOnActionResultNotification(notification);
        }
    }

    ~ProjectileAI()
    {
        NotificationCenter.RemoveListener<ActionResultNotification>(OnActionResultNotification);
    }
}