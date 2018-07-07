using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour {
    public Vector3 target;
    public float speed;
    public Animator ani;

    enum Direction
    {
        forward,
        back,
        left,
        right
    }

    private Direction direction;
    private Vector3 directionVec;

    // Use this for initialization
    void Start () {
        ani = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    CheckDirection();
	    HandleAnimation();
	    HandleInput();
	    MoveDoggo();
    }

    void CheckDirection()
    {
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

    void HandleInput()
    {
        if (Input.GetMouseButton(0)) {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0f;
        }
    }

    void MoveDoggo()
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
        ani.SetInteger("state", (int)direction);
    }
}
