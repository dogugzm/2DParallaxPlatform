using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        
        anim = GetComponent<Animator>();
    }

    public void PlayIdleAnim()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isJumping", false);
    }
    public void PlayRunningAnim()
    {
        anim.SetBool("isRunning", true);
        anim.SetBool("isJumping", false);
    }
    public void StopRunningAnim()
    {
        anim.SetBool("isRunning", false);
    }
    public void StopJumppingAnim()
    {
        anim.SetBool("isJumping", false);
    }

    public void PlayJumpingAnim()
    {
        anim.SetBool("isJumping", true);
    }

    public void PlayAttackAnim()
    {
        anim.SetTrigger("Attacking");
    }
}
