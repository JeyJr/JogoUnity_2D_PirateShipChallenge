using UnityEngine;
using UnityEngine.Events;

public class EnemyChaserCollider : MonoBehaviour
{
    private BoatStats playerStats;
    [SerializeField] private float damage;

    public UnityEvent hitPlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Target.Player.ToString()))
        {
            playerStats = collision.gameObject.GetComponent<BoatStats>();
            hitPlayer.Invoke();
        }
    }

    public void HitTheTarget()
    {
        playerStats.TakeDamage(damage);
    }

}
