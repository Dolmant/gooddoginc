using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Vector3 target;
    public float speed;
    protected Animator ani;

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
        ani = GetComponent<Animator>();
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
        directionVec = target - transform.position;
	    
        if (Mathf.Abs(directionVec.x) > Mathf.Abs(directionVec.y)) {
            if (directionVec.x > 0)
            {
                direction = Direction.right;
            } else
            {
                direction = Direction.left;
            }
        } else
        {
            if (directionVec.y > 0)
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
        transform.position = directionVec.normalized * speed + transform.position;
        
        if (Vector3.Distance(transform.position, target) > speed)
        {
            transform.position = (target - transform.position).normalized * speed + transform.position;
        }
        else
        {
            transform.position = target;
        }
    }
    void HandleAnimation()
    {
        if (directionPrev != direction) {
            ani.SetInteger("state", (int)direction);
        }
    }
}
