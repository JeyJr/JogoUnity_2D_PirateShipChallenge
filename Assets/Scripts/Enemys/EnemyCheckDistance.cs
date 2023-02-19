using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class EnemyCheckDistance : MonoBehaviour
{

    [SerializeField] private float distance = 0;
    [SerializeField] private float minDistance = 0; //chaser: 0 - shooter: 1
    [SerializeField] private float maxDistance = 3;
    private EnemyMovement enemyMovement;
    private Transform target;

    [Space(10)]
    public UnityEvent boatEvents;

    public bool IsDead { get; set; }

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        enemyMovement.Target = target;
    }

    private void Update()
    {
        if (!IsDead)
        {
            CheckDistanceToChaseTarget();
        }
    }
    private void CheckDistanceToChaseTarget()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance < maxDistance && distance > minDistance)
        {
            SetInterrupedMovement(false);
        }
        else if(distance > maxDistance)
        {
            SetInterrupedMovement(true);
        }
        else
        {
            SetInterrupedMovement(true);
            boatEvents.Invoke(); //EnemyShooter or EnemyExplode
        }
    }

    void SetInterrupedMovement(bool value) => enemyMovement.InterruptedMovement = value;
}
