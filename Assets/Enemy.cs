using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement {
    public Doggo doggo;
    private State confidence = State.confident;

    private bool confidenceShaken;
    public enum State
    {
        confident,
        nervous,
        scared
    }
    
	// Update is called once per frame
	override protected void FixedUpdate ()
	{
        var doggoPos = (doggo.transform.position - transform.position).magnitude;
        if (doggoPos < 10 && !confidenceShaken) {
            goingForTarget = true;
            target = doggo.transform.position;
        }
        if (!confidenceShaken && doggoPos < 3 && doggo.BARK) {
            if (confidence == State.confident) {
                confidenceShaken = true;
                confidence = State.nervous;
                goingForTarget = false;
                StartCoroutine(RestoreConfidence());
            } else if (confidence == State.nervous) {
                confidenceShaken = true;
                confidence = State.scared;
                goingForTarget = false;
                StartCoroutine(RestoreConfidence());
            } else {
                Debug.Log("MEOW *dies*");
                Destroy(gameObject);
                return;
            }
        }
        if (doggoPos < 0.5) {
            FightDoggo();
        }
        base.FixedUpdate();
    }

    public IEnumerator RestoreConfidence() {
        yield return new WaitForSeconds(2);
        confidenceShaken = false;
        goingForTarget = true;
    }

    void FightDoggo() {
        doggo.Fight(this);
    }
}
