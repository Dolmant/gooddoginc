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
        if ((doggo.transform.position - transform.position).magnitude < 10) {
            target = doggo.transform.position;
        }
        if ((doggo.transform.position - transform.position).magnitude < 0.5) {
            FightDoggo();
        }
        base.FixedUpdate();
    }

    void FightDoggo() {
        doggo.Fight(this);
    }
}
