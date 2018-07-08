using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Movement
{

    override protected void Start()
    {
        base.Start();
        rb.velocity = new Vector2(0f, -speed);
    }
    
    protected override void FixedUpdate()
    {
        // Stopping super class's fixed update
    }
}