using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static FadeController instance;
    private Animator animator;

    private void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        animator.SetBool("Out", false);
    }

    public void FadeOut()
    {
        animator.SetBool("Out", true);
    }
}
