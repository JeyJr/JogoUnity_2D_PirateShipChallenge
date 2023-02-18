using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerBulletsPooling : MonoBehaviour
{
    [Header("PREFAB")]
    public GameObject bulletPrefab;

    [Space(5)]
    [Header("FRONT")]
    public int maxFrontalPoolSize;
    public Queue<GameObject> pooledFrontalAmmo = new();
    public Transform frontalPosition;
    bool isShootingF;

    [Space(5)]
    [Header("SIDE")]
    public int maxSidePoolSize;
    public Queue<GameObject> pooledSideAmmo = new();
    public Transform sidePosition;
    bool isShootingS;
    [SerializeField] private float rotationValueToIncrement;
    [SerializeField] private float initialRotationValue;

    [Space(10)]
    public GameObject explosion;

    private void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < maxFrontalPoolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, frontalPosition.position, Quaternion.identity);
            obj.GetComponent<BulletBehavior>().explosion = Instantiate(explosion, obj.transform.position, Quaternion.identity);
            EnqueueFrontalAmmoObj(obj);
        }

        for (int i = 0; i < maxSidePoolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, sidePosition.position, Quaternion.identity);
            obj.GetComponent<BulletBehavior>().explosion = Instantiate(explosion, obj.transform.position, Quaternion.identity);
            EnqueueSideAmmoObj(obj);
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isShootingF)
        {
            isShootingF = true;
            FrontalSpawnBullet();
        }

        if (Input.GetKey(KeyCode.Space) && !isShootingS)
        {
            isShootingS = true;
            SideSpawnBullet();
        }
    }

    #region Frontal
    private async void FrontalSpawnBullet()
    {
        await Task.Delay(500);

        if(pooledFrontalAmmo.Count > 0)
        {
            DequeueFrontalAmmoObj();
        }
        
        isShootingF = false;
    }

    public void EnqueueFrontalAmmoObj(GameObject obj)
    {
        obj.SetActive(false);
        obj.GetComponent<BulletBehavior>().Frontal = true;
        pooledFrontalAmmo.Enqueue(obj);
    }

    public GameObject DequeueFrontalAmmoObj()
    {
        GameObject obj = pooledFrontalAmmo.Dequeue();
        obj.SetActive(true);
        obj.GetComponent<BulletBehavior>().MoveDirection = transform.rotation * Vector3.up;
        obj.GetComponent<BulletBehavior>().SetDisableBullet();
        obj.transform.position = frontalPosition.position;
        return obj;
    }
    #endregion

    #region Side
    private async void SideSpawnBullet()
    {
        await Task.Delay(500);

        if (pooledSideAmmo.Count > 0)
        {
            DequeueSideAmmoObj();
        }

        isShootingS = false;
    }

    public void EnqueueSideAmmoObj(GameObject obj)
    {
        obj.SetActive(false);
        pooledSideAmmo.Enqueue(obj);
    }

    public void DequeueSideAmmoObj()
    {
        sidePosition.localEulerAngles = new Vector3(0, 0, initialRotationValue);

        for (int i = 0; i < maxSidePoolSize; i++)
        {
            GameObject obj = pooledSideAmmo.Dequeue();
            sidePosition.Rotate(0, 0, rotationValueToIncrement);

            obj.SetActive(true);
            obj.GetComponent<BulletBehavior>().MoveDirection = sidePosition.transform.right;
            obj.GetComponent<BulletBehavior>().SetDisableBullet();

            obj.transform.position = sidePosition.position;
        }
    }
    #endregion

}
