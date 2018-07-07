﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Vector3 target;
    public float speed;
    public Animator ani;
    public Rigidbody2D rb;

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
    virtual protected void Start () {
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

    virtual protected void Update()
    {
        HandleAnimation();
    }
	
	// Update is called once per frame
	virtual protected void FixedUpdate ()
	{
	    CheckDirection();
        Move();
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

    void Move()
    {
        rb.velocity = (target - transform.position).normalized * speed;
        
        if (Vector3.Distance(transform.position, target) < speed * Time.deltaTime)
        {
            rb.velocity = Vector3.zero;
            rb.MovePosition(target);
        }
    }
    void HandleAnimation()
    {
        if (directionPrev != direction) {
            ani.SetInteger("state", (int)direction);
        }
    }
}
