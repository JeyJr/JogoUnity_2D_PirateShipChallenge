using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = .3f;
    [SerializeField] private float rotationSpeed = 80;
    public Transform target;
    private float rayDistance = .5f;
    public LayerMask layerMask;

    bool interruptedMovement, isChasing;
    float distance = 0;
    [SerializeField] private float maxDistance;


    private void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (!interruptedMovement)
        {
            Movement();
        }

        LooingAtTheTarget();
        CheckDistanceToTarget();
        LimitMovementArea();
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void LooingAtTheTarget()
    {
        Vector3 dir = target.position - transform.position;
        dir.z = 0f;

        if(dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void LimitMovementArea()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayDistance, layerMask);

        if (hit.collider != null)
        {
            moveSpeed = 0;
            interruptedMovement = true;
        }
        else
        {
            moveSpeed = .2f;
        }
    }


    private void CheckDistanceToTarget()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance < maxDistance)
        {
            interruptedMovement = false;
        }
        else
        {
            interruptedMovement = true;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.up * rayDistance, Color.yellow);
    }
}
