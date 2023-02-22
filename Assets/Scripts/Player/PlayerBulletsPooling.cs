using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerBulletsPooling : MonoBehaviour
{
    [SerializeField] private UIController uiController;

    [Header("PREFAB")]
    public GameObject frontalCannonBall;
    public GameObject sideCannonBall;
    

    [SerializeField] private float fShotDelayTime, sShotDelayTime;

    [Space(5)]
    [Header("FRONT")]
    [SerializeField] private int maxFrontalPoolSize;
    [SerializeField] private Queue<GameObject> pooledFrontalAmmo = new();
    [SerializeField] private Transform frontalPosition;
    public bool IsShootingF { get; set; }

    [Space(5)]
    [Header("SIDE")]
    [SerializeField] private int maxSidePoolSize;
    [SerializeField] private Queue<GameObject> pooledSideAmmo = new();
    [SerializeField] private Transform sidePosition;
    public bool IsShootingS { get; set; }

    [SerializeField] private float rotationValueToIncrement;
    [SerializeField] private float initialRotationValue;

    [Space(10)]
    public GameObject explosion;
    public bool IsDead { get; set; }

    private void Start()
    {
        InitializePool();
        uiController.StartCountdownToFrontaleShot(fShotDelayTime);
        uiController.StartCountdownToSideShot(sShotDelayTime);
    }

    void InitializePool()
    {
        
        for (int i = 0; i < maxFrontalPoolSize; i++)
        {
            GameObject obj = Instantiate(frontalCannonBall, frontalPosition.position, Quaternion.identity);
            obj.GetComponent<BulletBehavior>().explosion = Instantiate(explosion, obj.transform.position, Quaternion.identity);
            obj.GetComponent<BulletBehavior>().SetTargetEnemys(Target.Enemy); 
            EnqueueFrontalAmmoObj(obj);
        }

        for (int i = 0; i < maxSidePoolSize; i++)
        {
            GameObject obj = Instantiate(sideCannonBall, sidePosition.position, Quaternion.identity);
            obj.GetComponent<BulletBehavior>().explosion = Instantiate(explosion, obj.transform.position, Quaternion.identity);
            obj.GetComponent<BulletBehavior>().SetTargetEnemys(Target.Enemy);
            EnqueueSideAmmoObj(obj);
        }
    }


    private void Update()
    {
        if (!IsDead)
        {
            if (Input.GetMouseButtonDown(0) && !IsShootingF)
            {
                IsShootingF = true; 
                uiController.StartCountdownToFrontaleShot(fShotDelayTime);
                FrontalSpawnBullet();
            }

            if (Input.GetKey(KeyCode.Space) && !IsShootingS)
            {
                IsShootingS = true; 
                uiController.StartCountdownToSideShot(sShotDelayTime);
                SideSpawnBullet();
            }
        }
    }

    #region Frontal


    private void FrontalSpawnBullet()
    {
        if(pooledFrontalAmmo.Count > 0)
        {
            DequeueFrontalAmmoObj();
        }
    }

    public void EnqueueFrontalAmmoObj(GameObject obj)
    {
        obj.SetActive(false);
        obj.GetComponent<BulletPlayerPool>().Frontal = true;
        pooledFrontalAmmo.Enqueue(obj);
    }

    public GameObject DequeueFrontalAmmoObj()
    {
        GameObject obj = pooledFrontalAmmo.Dequeue();
        obj.SetActive(true);
        obj.GetComponent<BulletBehavior>().MoveDirection = transform.rotation * Vector3.up;
        obj.GetComponent<BulletPlayerPool>().SetDisableBullet();
        obj.transform.position = frontalPosition.position;
        return obj;
    }
    #endregion

    #region Side
    private void SideSpawnBullet()
    {
        if (pooledSideAmmo.Count > 0)
        {
            DequeueSideAmmoObj();
        }
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
            obj.GetComponent<BulletPlayerPool>().SetDisableBullet();
            obj.transform.position = sidePosition.position;
        }
    }
    #endregion

}
