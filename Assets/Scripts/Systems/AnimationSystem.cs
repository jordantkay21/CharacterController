using System;
using UnityEngine;

public class AnimationSystem
{
    private Animator animator;
    private AnimationData animData;

    // Constructor to initialize the animator
    public AnimationSystem(Animator animator, AnimationData animData)
    {
        this.animator = animator;
        this.animData = animData;
    }


    // Method to update animation based on movement state
    public void UpdateAnimation()
    {

    }



    public void OnJumpAnimationEnd()
    {
        //Reset isJumping to false
        animator.SetBool("IsJumping", false);
    }
}
