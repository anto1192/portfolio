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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBall();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Goal")
        {
            onGoalShooted.Invoke(null, null);
        }
        if (firstCollision && (collision.collider.gameObject.tag == "Floor" || collision.collider.gameObject.tag == "Wall"))
        {
            firstCollision = false;
            onBallShooted.Invoke(this.gameObject, null);
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
