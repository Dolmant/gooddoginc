using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement {
    public Doggo doggo;
    override protected void Start () {
        base.Start();
    }

	// Update is called once per frame
	override protected void FixedUpdate ()
	{
        var doggoPos = (doggo.transform.position - transform.position).magnitude;
        if (doggoPos < 10) {
            target = doggo.transform.position;
        }
        if (doggoPos < 3 && doggo.BARK) {
            Debug.Log("MEOW *dies*");
            Destroy(gameObject);
            return;
        }
        if (doggoPos < 0.5) {
            FightDoggo();
        }
        base.FixedUpdate();
    }

    void FightDoggo() {
        doggo.Fight(this);
    }
}
