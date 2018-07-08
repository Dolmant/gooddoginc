using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Human : Movement
{
    public Animator grumpyAni;
    public PathNode currentPathTarget;
    public PathNode previousPathTarget;
    public Vector3 pathVector;
    public float pathForceDivider;

    public Doggo myDog;

    protected override void Start()
    {
        base.Start();
        GoToNode(currentPathTarget);
        myDog.Walkies = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Path") && other.GetComponent<PathNode>() == currentPathTarget)
        {
            GoToNode(currentPathTarget.GetNextNode());
        }
    }

    public void Grumpy() {
        Debug.Log("I'm grumpy!");
        grumpyAni.SetBool("grumpy", true);
        StartCoroutine(GrumpyAni());
    }

    public IEnumerator GrumpyAni() {
        yield return new WaitForSeconds(2);
        grumpyAni.SetBool("grumpy", false);
    }

    void GoToNode(PathNode node)
    {
        previousPathTarget = currentPathTarget;

        if (node)
        {
            currentPathTarget = node;
            target = currentPathTarget.transform.position;
        }
        else
        {
            target = transform.position;
        }
        
        goingForTarget = true;
        
        pathVector =  currentPathTarget.transform.position - previousPathTarget.transform.position;

        speed = previousPathTarget.segmentSpeed;
        if (previousPathTarget.pauseTime > 0f)
        {
            StartCoroutine(PauseAtNode(previousPathTarget.pauseTime));
        }
        
        // Special nodes
        switch (previousPathTarget.nodeType)
        {
            case PathNode.NodeType.Normal:
                break;
            case PathNode.NodeType.Start:
                myDog.Walkies = true;
                break;
            case PathNode.NodeType.End:
                myDog.EndWalk();
                break;
            
        }
    }

    IEnumerator PauseAtNode(float time)
    {
        goingForTarget = false;
        yield return new WaitForSeconds(time);
        goingForTarget = true;
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