using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Movement
{

    override protected void Start()
    {
        base.Start();
        target = new Vector3(0f, -9999999f, 0f);
        goingForTarget = true;
    }
}