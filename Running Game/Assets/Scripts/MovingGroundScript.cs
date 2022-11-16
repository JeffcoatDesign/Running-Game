using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGroundScript : MonoBehaviour
{
    public Rigidbody2D rig;
    public Rigidbody2D otherGround;
    public Collider2D spawnBounds;
    public GameObject[] obstacles;
    private List<GameObject> spawnedObstacles = new List<GameObject>();
    private float width;
    private float boundsWidth;
    private float boundsHeight;

    private void Start()
    {
        width = transform.localScale.x;
        boundsWidth = spawnBounds.bounds.size.x;
        boundsHeight = spawnBounds.bounds.size.y;
    }
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

        if (rig.position.x < -width)
            Move();
    }

    void Move()
    {
        rig.position = new Vector2(otherGround.position.x + width, rig.position.y);
        ClearObstacles();
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-0.5f * boundsWidth, 0.5f * boundsWidth), Random.Range(-0.5f * boundsHeight, 0.5f * boundsHeight), 0);
        spawnPos += spawnBounds.transform.position;
        int randInd = Random.Range(0, obstacles.Length);
        GameObject obj = Instantiate(obstacles[randInd], spawnBounds.transform, true);
        obj.transform.SetPositionAndRotation(spawnPos, obj.transform.rotation);
        spawnedObstacles.Add(obj);
    }

    public void ClearObstacles()
    {
        foreach (GameObject obj in spawnedObstacles)
        {
            Destroy(obj);
        }
        spawnedObstacles = new List<GameObject>();
    }
}
