using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehavior : MonoBehaviour
{
    private Transform playerPosition;
    Vector3 currentVelocity;

    [SerializeField] private float smooth;
    [SerializeField] private float maxSpeed;

    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(playerPosition.position.x, playerPosition.position.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smooth, maxSpeed);
    }
}
