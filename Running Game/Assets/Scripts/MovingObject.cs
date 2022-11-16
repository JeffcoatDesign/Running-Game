using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public bool respawns;
    public Rigidbody2D rig;
    void Update()
    {
        if (!GameManager.instance.gameRunning)
        {
            if (rig.velocity == Vector2.zero)
                return;
            else
                rig.velocity = Vector2.zero;
        }

        rig.velocity = new Vector2(-GameManager.instance.currentSpeed, rig.velocity.y);

        if (rig.position.x < -30)
            MoveOrDie();
    }

    void MoveOrDie()
    {
        if (respawns)
            rig.position = new Vector2(30, rig.position.y);
        else
            Destroy(gameObject);
    }
}
