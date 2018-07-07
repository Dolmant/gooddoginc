using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Movement
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, -1f);
    }
    
    protected override void FixedUpdate()
    {
        
    }
}