using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    public LayerMask layerMask;
    public float distance;

    private void Start()
    {
        moveSpeed = moveSpeed <= 0 ? 1 : moveSpeed;
        rotationSpeed = rotationSpeed <= 0 ? 100 : rotationSpeed;
    }

    private void Update()
    {
        Movement();
        LimitMovementArea();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }
    }
    public void LimitMovementArea()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, layerMask);

        if(hit.collider != null)
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = 1;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.up * distance, Color.red);
    }
}
