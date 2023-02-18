using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour
{
    private float maxLife = 10f;
    private float currentLife = 10;

    public UnityEvent onDeath, onHit;

    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        onHit.Invoke();

        if(currentLife <= 0)
        {
            onDeath.Invoke();
        }
    }

    public void LifeBarUpdate()
    {
        //UpdateLifeBar
    }

    public void Death()
    {
        //Anim?
        //Destroy?
        //SFX?
    }
}
