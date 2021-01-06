using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterAnimationController characterAnimationController;
    CharacterMovementController characterMovementController;
    Health health;
    CharacterCombat characterCombat;
    Rigidbody2D rigidBody2D;
    private void Awake()
    {
        characterAnimationController = GetComponent<CharacterAnimationController>();
        characterMovementController = GetComponent<CharacterMovementController>();
        characterCombat = GetComponent<CharacterCombat>();
        health = GetComponent<Health>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        SetCharacterState();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            characterMovementController.movementState != CharacterMovementController.MovementState.JUMPING)      //NORMALDE FİXEDUPDATE İÇİNDEYDİ
        {
            StartCoroutine(AttackOrder());
        }
    }
    private void SetCharacterState()
    {
        if (characterCombat.isAttacking==true)
        {
            return;
        }
        if (characterMovementController.IsGrounded())
        {
            if (rigidBody2D.velocity.x == 0)
            {
                characterMovementController.SetCharacterState(CharacterMovementController.MovementState.IDLE);
            }
            else if (rigidBody2D.velocity.x < 0)
            {
                characterMovementController.facingDirection =CharacterMovementController.FacingDirection.LEFT;
                characterMovementController.SetCharacterState(CharacterMovementController.MovementState.RUNNING);
            }
            else if (rigidBody2D.velocity.x > 0)
            {
                characterMovementController.facingDirection = CharacterMovementController.FacingDirection.RIGHT;
                characterMovementController.SetCharacterState(CharacterMovementController.MovementState.RUNNING);
            }
        }
        else
        {
            characterMovementController.SetCharacterState(CharacterMovementController.MovementState.JUMPING);
        }
    }

    private IEnumerator AttackOrder()
    {
        if (characterCombat.isAttacking)
        {
            yield break;
        }
        characterCombat.isAttacking = true;

        characterMovementController.movementState = CharacterMovementController.MovementState.ATTACKING;

        characterAnimationController.PlayAttackAnim();

        yield return new WaitForSeconds(0.8f);

        characterCombat.Attack();

        characterCombat.isAttacking = false;

        yield break;
    } 
}
