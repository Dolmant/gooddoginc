using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement {
    public Doggo doggo;
    private State confidence = State.confident;
    public GameObject tombStone;

    private bool confidenceShaken;
    public enum State
    {
        confident,
        nervous,
        scared
    }

    public void Die() {
        Debug.Log("MEOW *dies*");
        TombStone();
        Destroy(gameObject);
    }

    public void TombStone() {
        Instantiate(tombStone, transform.position, transform.rotation);
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
                rb.velocity = -rb.velocity / 2;
                StartCoroutine(RestoreConfidence());
            } else if (confidence == State.nervous) {
                confidenceShaken = true;
                confidence = State.scared;
                rb.velocity = -rb.velocity / 2;
                StartCoroutine(RestoreConfidence());
            } else {
                Die();
                return;
            }
        }
        if (doggoPos < 0.5) {
            FightDoggo();
        }
        if (!confidenceShaken) {
            base.FixedUpdate();
        }
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
