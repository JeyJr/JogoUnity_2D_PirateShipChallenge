using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehavior : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    Vector3 currentVelocity;

    [SerializeField] private float smooth;
    [SerializeField] private float maxSpeed;


    void Update()
    {
        Vector3 target = new Vector3(playerPosition.position.x, playerPosition.position.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smooth, maxSpeed);
    }
}
