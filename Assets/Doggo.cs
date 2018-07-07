using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour {
    public Vector2 target;
    public float speed;
    public Animator ani;

    // Use this for initialization
    void Start () {
        ani = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        var left = 2;
        var right = 3;
        var forward = 0;
        var back = 1;
        var direction = (target - (Vector2)transform.position);
        Debug.Log(direction);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            if (direction.x > 0)
            {
                ani.SetInteger("state", right);
            } else
            {
                ani.SetInteger("state", left);
            }
        } else
        {
            if (direction.y > 0)
            {
                ani.SetInteger("state", back);
            }
            else
            {
                ani.SetInteger("state", forward);
            }
        }
        transform.position = direction.normalized * speed + (Vector2)transform.position;
    }
}
