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
    public void UpdateAnimation(Vector2 moveInput, bool isSprinting, bool isJumping, bool isGrounded)
    {

        // Update movement animation
        float speed = moveInput.magnitude;
        animator.SetFloat("MoveSpeed", speed);

        // Update sprinting state
        animator.SetBool("IsSprinting", isSprinting);

        // Handle jumping and falling
        if (isJumping)
        {
            animator.SetTrigger("Jump");
        }
        else if (isGrounded)
        {
            animator.SetBool("IsGroudned", true);
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
