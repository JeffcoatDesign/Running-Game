using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float liftForce;
    public float maxLift;
    public float liftDecay;
    public float liftAdd;
    private Vector3 startPos;
    private float curLiftAmnt;
    private Rigidbody2D rig;
    
    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        if (!GameManager.instance.gameRunning)
            return;

        if (Input.GetKey(KeyCode.Space) && curLiftAmnt != 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, liftForce);
            curLiftAmnt = Mathf.Clamp(curLiftAmnt - liftDecay * Time.deltaTime, 0, maxLift);
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            curLiftAmnt = Mathf.Clamp(curLiftAmnt + liftAdd * Time.deltaTime, 0, maxLift);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
            GameManager.instance.Lose();
    }
    public void StartPlayer () 
    {
        transform.SetPositionAndRotation(startPos, transform.rotation);
        curLiftAmnt = maxLift;
    }
}
