using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public Vector3 MoveDirection { get; set; }
    private PlayerBulletsPooling playerBulletsPooling;
    public bool Frontal { get; set; }

    [Space(10)]
    private readonly float damage = 1;
    public UnityEvent onHit;

    [Space(10)]
    public GameObject explosion;

    private void Start()
    {
        playerBulletsPooling =GameObject.FindWithTag("Player").GetComponent<PlayerBulletsPooling>();
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(MoveDirection * Time.deltaTime * moveSpeed, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyStats>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            onHit.Invoke();
        }
    }

    #region Bullet in Pool
    public void SetDisableBullet()
    {
        StartCoroutine(DisableBullet());
    }
    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(.3f);
        Enqueue();
    }
    public void Enqueue()
    {
        if (Frontal)
        {
            playerBulletsPooling.EnqueueFrontalAmmoObj(gameObject);
        }
        else
        {
            playerBulletsPooling.EnqueueSideAmmoObj(gameObject);
        }
    }
    #endregion

    public void BulletHitSFX()
    {
        //Play SoundEffect
    }
    public void BulletExplosion()
    {
        explosion.transform.position = transform.position;
        explosion.GetComponent<VFXBehavior>().SetEnabledAndPlayAnimation(AnimName.explosion);
    }
}
