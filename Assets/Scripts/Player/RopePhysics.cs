using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePhysics : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    HingeJoint2D hingeJoint2D;

    public float pushForce = 10f;
    public bool attached = false;
    public Transform attachedTo;
    private GameObject disregard;
    public float speedOnRope = 4;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        hingeJoint2D = GetComponent<HingeJoint2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeybordInput();
        

    }

    public void Attach(Rigidbody2D rigidbodyRope) 
    {
        hingeJoint2D.connectedBody = rigidbodyRope;
        hingeJoint2D.enabled = true;
        attached = true;
        attachedTo = rigidbodyRope.gameObject.transform.parent;
    }
    public void Detach()
    {
        attached = false;
        hingeJoint2D.enabled = false;
        attachedTo = null;
    }
    public void Slide() 
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attached)
        {
            if (collision.CompareTag("Rope"))
            {
                if (attachedTo != collision.gameObject.transform.parent)  //aynı ipte değilsen
                {
                    if (disregard == null || collision.gameObject.transform.parent != disregard)
                    {
                        Attach(collision.gameObject.GetComponent<Rigidbody2D>());
                    }
                    
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attached)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //slide
            }
            if (Input.GetKey(KeyCode.S))
            {
                //slide
            }
        }
    }

    private void CheckKeybordInput()
    {
        if (attached)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody2D.AddRelativeForce(new Vector3(-1, 0, 0) * pushForce);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody2D.AddRelativeForce(new Vector3(+1, 0, 0) * pushForce);
            }
            
            if (Input.GetKey(KeyCode.Space))
            {
                Detach();
            }
        }
    }
}
