using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public enum Target { Enemy, Player}
public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private readonly float damage = 1;
    public UnityEvent onHit;
    public Vector3 MoveDirection { get; set; }
    public GameObject explosion;
    private string target;

    public void SetTargetEnemys(Target target) {
        this.target = target.ToString();
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(MoveDirection * Time.deltaTime * moveSpeed, Space.World);
        }
    }

    public void SetDisableBullet()
    {
        StartCoroutine(DisableBullet());
    }
    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(target.ToString()))
        {
            collision.GetComponent<BoatStats>().TakeDamage(damage);
            onHit.Invoke();
        }
    }

    public void OnHitSFX()
    {
        //Play SoundEffect
    }
    public void OnHitExplosion()
    {
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.GetComponent<VFXBehavior>().SetEnabledAndPlayAnimation(AnimName.explosion);
        }
    }

    public void OnHitInstantiateExplosion()
    {
        if (explosion != null)
        {
            var obj = Instantiate(explosion, transform.position, Quaternion.identity);
            obj.GetComponent<VFXBehavior>().SetDestroyObj(.3f);
        }
    }

    public void OnHitDestroy()
    {
        StopCoroutine(DisableBullet());
        Destroy(gameObject);
    }
}
