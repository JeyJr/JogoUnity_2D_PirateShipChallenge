using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletPlayerPool : MonoBehaviour
{
    private PlayerBulletsPooling playerBulletsPooling;
    public bool Frontal { get; set; }

    private void Start()
    {
        playerBulletsPooling = GameObject.FindWithTag("Player").GetComponent<PlayerBulletsPooling>();
    }

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
            playerBulletsPooling.EnqueueFrontalAmmoObj(gameObject); //Frontal
        }
        else
        {
            playerBulletsPooling.EnqueueSideAmmoObj(gameObject); //Side
        }
    }
}
