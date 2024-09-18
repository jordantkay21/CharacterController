using System;
using UnityEngine;

public class AnimationSystem
{
    private Animator animator;

    // Constructor to initialize the animator
    public AnimationSystem(Animator animator)
    {
        this.animator = animator;
    }

    // Method to update animation based on movement state
    /// <summary>
    /// 
    /// </summary>
    /// <param name="moveInput"></param>
    /// <param name="isSprinting">0 = false | 1 = true</param>
    /// <param name="isJumping"></param>
    /// <param name="isGrounded"></param>
    public void UpdateAnimation(bool isIdle, Vector2 moveInput, float speed, bool isJumping, bool isGrounded)
    {
        // Update movement animation
        animator.SetBool("IsIdle", isIdle);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isGrounded", isGrounded);

        animator.SetFloat("MoveSpeed", speed);
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputZ", moveInput.y);


        // Handle jumping and falling
        if (isJumping)
        {
            animator.SetTrigger("Jump");
        }
        else if (isGrounded)
        {
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            animator.SetBool("IsGrounded", false);
        }
    }

    // Additional methods for handling root motion if necessary
    public void OnAnimatorMove()
    {
        // Handle root motion-based movement here if needed
    }
}
