using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement {
    public Doggo doggo;
    private State confidence;

    private Boolean confidenceShaken;
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
            target = doggo.transform.position;
        }
        if (doggoPos < 3 && doggo.BARK) {
            if (confidence == State.confident) {
                confidenceShaken = true;
                confidence = State.nervous;
                target = transform.position;
            } else if (confidence == State.nervous) {
                confidenceShaken = true;
                confidence = State.scared;
                target = transform.position;
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
    }

    void FightDoggo() {
        doggo.Fight(this);
    }
}
