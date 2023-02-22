using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    public bool IsDead { get; set; }

    private void Start()
    {
        moveSpeed = moveSpeed <= 0 ? 1 : moveSpeed;
        rotationSpeed = rotationSpeed <= 0 ? 100 : rotationSpeed;
    }

    private void Update()
    {
        if (!IsDead)
        {
            Movement();
        }
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
}
