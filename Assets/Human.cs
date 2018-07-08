﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Movement
{
    public PathNode currentPathTarget;
    public PathNode previousPathTarget;
    public Vector3 pathVector;
    public float pathForceDivider;

    protected override void Start()
    {
        base.Start();
        GoToNode(currentPathTarget);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Path") && other.GetComponent<PathNode>() == currentPathTarget)
        {
            GoToNode(currentPathTarget.GetNextNode());
        }
    }

    void GoToNode(PathNode node)
    {
        previousPathTarget = currentPathTarget;
        
        currentPathTarget = node;
        target = currentPathTarget.transform.position;
        goingForTarget = true;
        
        pathVector =  currentPathTarget.transform.position - previousPathTarget.transform.position;
    }

    private Vector2 perp;
    private Vector3 cross;
    protected override void MoveGravity()
    {
        base.MoveGravity();
        
        // Move back towards the path
        
        // Move perpendicular to the pathvector
        perp = new Vector2(pathVector.y, -pathVector.x);
        // Determine direction based on which side of path vector we're on
        cross = Vector3.Cross(transform.position - previousPathTarget.transform.position, pathVector);
        rb.velocity += perp * -cross.z / pathForceDivider;
    }
}