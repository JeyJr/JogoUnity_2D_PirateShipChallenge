using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimName { explosion }
public class VFXBehavior : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetEnabledAndPlayAnimation(AnimName animName)
    {
        gameObject.SetActive(true);
        anim.Play(animName.ToString());
    }

    public void SetDisableAtTheEndOfAnimation()
    {
        gameObject.SetActive(false);
    }
}
