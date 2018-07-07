using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement {
    private GameObject DoggoRef;
    override protected void Start () {
        base.Start();
        DoggoRef = GameObject.Find("Doggo");
    }

	// Update is called once per frame
	override protected void FixedUpdate ()
	{
        if ((DoggoRef.transform.position - transform.position).magnitude > 10) {
            target = DoggoRef.transform.position;
        }
        if ((DoggoRef.transform.position - transform.position).magnitude < 0.5) {
            FightDoggo();
        }
        base.FixedUpdate();
    }

    void FightDoggo() {
        Doggo doggo = (Doggo)DoggoRef.GetComponent(typeof(Doggo));
        doggo.Fight(this);
    }
}
