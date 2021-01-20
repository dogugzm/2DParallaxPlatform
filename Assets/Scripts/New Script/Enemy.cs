using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    
    [SerializeField] private float speed = 5;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private BoxCollider2D bodyCollider;


    public LayerMask groundLayer;



    private bool mustTurn;
    private bool doPatrol;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        doPatrol = true;
    }


    private void FixedUpdate()
    {
        if (doPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (doPatrol)
        {
            Patrol();
        }
    }



    private void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    private void Flip()
    {
        doPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        doPatrol = true;
    }
}
