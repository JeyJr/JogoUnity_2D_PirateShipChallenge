using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPosition;

    private bool isShooting = false;
    public BoatsSFX boatSFX;

    private void OnEnable()
    {
        boatSFX = GetComponent<BoatsSFX>();
    }

    //UnityEvent 
    public void BoatEventStartShoot()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        float waitTime = Random.Range(2, 5);
        yield return new WaitForSeconds(waitTime);
        
        var obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<BulletBehavior>().MoveDirection = transform.rotation * Vector3.up;
        obj.GetComponent<BulletBehavior>().SetDisableBullet();
        obj.GetComponent<BulletBehavior>().SetTargetEnemys(Target.Player);
        obj.transform.position = spawnPosition.position;

        boatSFX.PlayClip(BoatSFXClip.shoot);

        isShooting = false;
    }
}
