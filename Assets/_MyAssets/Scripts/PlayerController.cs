using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMovingForward;
    private bool isMovingBackward;
    private bool isMovingLeft;
    private bool isMovingRight;
    private bool shouldRotate = false;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;

    public object MathInput { get; private set; }

    // Update is called once per frame
    void FixedUpdate()
    {
        isMovingForward = Input.GetKey(KeyCode.W);
        isMovingBackward = Input.GetKey(KeyCode.S);
        isMovingLeft = Input.GetKey(KeyCode.A);
        isMovingRight = Input.GetKey(KeyCode.D);

        if (isMovingForward || isMovingBackward || isMovingLeft || isMovingRight)
        {
            MovePlayer(isMovingForward, isMovingBackward, isMovingLeft, isMovingRight);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            shouldRotate = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            shouldRotate = true;
        }

        if (shouldRotate)
        {
            if (Input.GetKey(KeyCode.Escape)) {
                shouldRotate = false;
                return;
            }
            float h = Math.Clamp(Input.GetAxis("Mouse X"), -1, 1);
            float v = Math.Clamp(Input.GetAxis("Mouse Y"), -1, 1);

            if (h != 0f || v != 0f)
            {
                //Debug.Log("Horiz: " + h + "; Vert: " + v);
                RotatePlayer(h, v);
            }
        }      
    }

    private void MovePlayer(bool isMovingForward, bool isMovingBackward, bool isMovingLeft, bool isMovingRight)
    {
        Vector3 actualEulerAngles = transform.localEulerAngles;
        
        //set temp localEulerAngels x and z to 0 -> this allows to translate only considering y rotation axis
        transform.localEulerAngles = new Vector3(0, actualEulerAngles.y, 0);
        
        //set direction axis based on user input
        Vector3 direction = Vector3.zero;
        if (isMovingForward)
        {
            direction = new Vector3(direction.x, direction.y, direction.z + 1);
        }
        if (isMovingBackward)
        {
            direction = new Vector3(direction.x, direction.y, direction.z -1);
        }
        if (isMovingLeft)
        {
            direction = new Vector3(direction.x - 1, direction.y, direction.z);
        }
        if (isMovingRight)
        {
            direction = new Vector3(direction.x + 1, direction.y, direction.z);
        }
        
        transform.Translate(direction * Time.deltaTime * movementSpeed, Space.Self);

        //riassign correct localEulerAngels
        transform.localEulerAngles = actualEulerAngles;
    }

    private void RotatePlayer(float h, float v)
    {
        transform.Rotate(Vector3.up, h * Time.deltaTime * rotationSpeed);
        transform.Rotate(Vector3.left, v * Time.deltaTime * rotationSpeed);
        transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
