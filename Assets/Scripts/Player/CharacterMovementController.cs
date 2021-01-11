using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public enum MovementState
    {
        IDLE,
        RUNNING,
        JUMPING,
        ATTACKING
    }

    public enum FacingDirection
    {
        RIGHT,LEFT
    }

    [Header("Values")]    
    public float movementSpeed;
    public float jumpForce;

    [Header("RayCast Values")]
    public LayerMask platformLayerMask;
    public float isGroundedRayLength = 0.5f;

    [Header("States")]
    public MovementState movementState;
    public FacingDirection facingDirection;


    CharacterAnimationController animationController;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody2D;
    CapsuleCollider2D capsuleCollider;
    HingeJoint2D hingeJoint;
    
    public bool canJump;

    // Start is called before the first frame update
    void Awake()
    {
        canJump = true;
        animationController = GetComponent<CharacterAnimationController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        hingeJoint = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space)&& IsGrounded()&&canJump ==true)
        {
            rigidBody2D.velocity = Vector2.up*jumpForce;
        }
    }
    private void FixedUpdate()
    {
        //SetCharacterState();
        HandleMovement();
        PlayAnimationsBasedStates();
        SetCharacterFacial();
    }

    public void SetCharacterState(MovementState movementStates) {
        movementState = movementStates;
    }
    private void HandleMovement()
    {
        rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody2D.velocity = new Vector2(-movementSpeed, rigidBody2D.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody2D.velocity = new Vector2(+movementSpeed, rigidBody2D.velocity.y);
        }
        else
        {
            if (IsGrounded())
            {
                rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            //rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, rigidBody2D.velocity.y);

            //rigidBody2D.constraints = /*RigidbodyConstraints2D.FreezePositionX |*/ RigidbodyConstraints2D.FreezeRotation;
        }
    }
    public bool IsGrounded()
    {
        //new ray
        RaycastHit2D raycastHit2D = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size,CapsuleDirection2D.Vertical,0,Vector2.down,isGroundedRayLength,platformLayerMask);
        
        //RaycastHit2D raycastHit2D = Physics2D.BoxCast(spriteRenderer.bounds.center,spriteRenderer.bounds.size,0f,Vector2.down,isGroundedRayLength,platformLayerMask);        
        return raycastHit2D.collider != null;
    }

   

    //private void SetCharacterState()
    //{
    //    if (IsGrounded())
    //    {
    //        if (rigidBody2D.velocity.x==0)
    //        {
    //            movementState = MovementState.IDLE;         
    //        }
    //        else if (rigidBody2D.velocity.x < 0)
    //        {
    //            facingDirection = FacingDirection.LEFT;
    //            movementState = MovementState.RUNNING;
    //        }
    //        else if(rigidBody2D.velocity.x > 0)
    //        {
    //            facingDirection = FacingDirection.RIGHT;
    //            movementState = MovementState.RUNNING;
    //        }
    //    }
    //    else
    //    {
    //        movementState = MovementState.JUMPING;     
    //    }
    //}
    private void SetCharacterFacial()
    {
        switch (facingDirection)
        {
            case FacingDirection.RIGHT:
                spriteRenderer.flipX = false;
                break;
            case FacingDirection.LEFT:
                spriteRenderer.flipX = true;
                break;
            
        }
    }
    private void PlayAnimationsBasedStates()
    {
        switch (movementState)
        {
            case MovementState.IDLE:
                animationController.PlayIdleAnim();
                break;
            case MovementState.RUNNING:
                animationController.PlayRunningAnim();
                break;
            case MovementState.JUMPING:
                animationController.PlayJumpingAnim();
                break;
            case MovementState.ATTACKING:
                break;
            default:
                break;
        }
    }
}
