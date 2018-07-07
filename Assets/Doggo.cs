using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : Movement {
    // Use this for initialization
    override protected void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	override protected void Update ()
	{
        base.Update();
	    HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButton(0)) {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0f;
        }
    }

    public void Fight(Enemy enemy) {
        Destroy(enemy);
        ani.SetBool("fight", true);
    }
}
