using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Vector3 target;
    public bool goingForTarget;
    public float speed;
    public float targetStopRange;
    public Animator ani;
    public Rigidbody2D rb;

    public Vector3 gravity = Vector3.zero;
    
    protected enum Direction
    {
        forward,
        back,
        left,
        right
    }

    protected Direction direction;
    protected Direction directionPrev;
    protected Vector3 directionVec;

    // Use this for initialization
    protected virtual void Start () {
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (!ani)
        {
            ani = GetComponent<Animator>();
        }
        target = transform.position;
    }

    protected virtual void Update()
    {
        HandleAnimation();
    }
	
	// Update is called once per frame
    protected virtual void FixedUpdate ()
	{
	    CheckDirection();
        MoveSelf();
	    MoveGravity();
	}

    void CheckDirection()
    {
        directionPrev = direction;
	    
        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y)) {
            if (rb.velocity.x > 0)
            {
                direction = Direction.right;
            } else
            {
                direction = Direction.left;
            }
        } else
        {
            if (rb.velocity.y > 0)
            {
                direction = Direction.back;
            }
            else
            {
                direction = Direction.forward;
            }
        }
    }

    void MoveSelf()
    {
        rb.velocity = Vector3.zero;
        if (goingForTarget)
        {
            rb.velocity = (target - transform.position).normalized * speed;
            float targetD = Vector3.Distance(transform.position, target);
            if (targetD < targetStopRange || targetD < speed * Time.deltaTime)
            {
                rb.velocity = Vector3.zero;
                goingForTarget = false;
            }
        }
    }

    protected virtual void MoveGravity()
    {
        rb.velocity += (Vector2)gravity;
    }
    
    void HandleAnimation()
    {
        if (directionPrev != direction) {
            ani.SetInteger("state", (int)direction);
        }
    }
    
}
