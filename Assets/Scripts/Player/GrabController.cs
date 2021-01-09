using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float rayDistance = 2f;
    public LayerMask boxLayer;

    CharacterMovementController characterMovementController;

    private void Awake()
    {
        characterMovementController = GetComponent<CharacterMovementController>();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * GetFlipvalue(), rayDistance,boxLayer);
        GameObject box = hit.collider.gameObject;
        
        if (hit.collider!=null && Input.GetKey(KeyCode.F) )
        {
            
            //karakter zıplamasını kapat
            characterMovementController.canJump = false;


            box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            characterMovementController.canJump = true;

            box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            

            //y yi de kapat
            box.GetComponent<FixedJoint2D>().enabled = false;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (transform.right * GetFlipvalue() * rayDistance));

    }

    private int GetFlipvalue()
    {
        if (spriteRenderer.flipX==true)
        {
            return -1;
        }
        return 1;
    }
}
