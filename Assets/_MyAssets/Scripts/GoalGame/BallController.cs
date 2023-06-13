using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private bool firstCollision = true;

    [SerializeField]
    private float force;

    public event EventHandler onBallShooted;
    public event EventHandler onGoalShooted;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBall();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (firstCollision && (collision.collider.gameObject.tag == Constant.FLOOR_TAG || collision.collider.gameObject.tag == Constant.WALL_TAG))
        {
            firstCollision = false;
            onBallShooted.Invoke(this.gameObject, null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Constant.GOAL_TAG)
        {
            onGoalShooted.Invoke(null, null);
        }
    }

    private void ShootBall()
    {
        if (transform.parent == null)
        {
            //ball already shooted
            return;
        }
        rb.AddForce(transform.forward * force);
        transform.parent = null;
        rb.useGravity = true;
    }
}
