using UnityEngine;

public class EnemyChaserCollider : MonoBehaviour
{
    private BoatStats playerStats;
    [SerializeField] private float damage;

    private void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<BoatStats>();
    }

    public void WhenCollidingDamageTheTarget()
    {
        playerStats.TakeDamage(damage);
    }
}
