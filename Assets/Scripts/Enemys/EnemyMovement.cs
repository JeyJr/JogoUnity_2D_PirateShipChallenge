using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float moveSpeed = .3f;
    [SerializeField] private float rotationSpeed = 80;
    public Transform Target { get; set; }


    public bool InterruptedMovement { get; set; }
    public bool IsDead { get; set; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Update()
    {
        if (!IsDead)
        {
            if (!InterruptedMovement)
            {
                Movement();
            }
            LookingAtTheTarget();
        }
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, MoveSpeed * Time.deltaTime);
    }

    void LookingAtTheTarget()
    {
        Vector3 dir = Target.position - transform.position;
        dir.z = 0f;

        if(dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
