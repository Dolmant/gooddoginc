﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOfInterest : MonoBehaviour {
    public Doggo doggo;
    public State progress;

    public enum State
    {
        zero,
        half,
        full
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// void FixedUpdate ()
	// {
    //     if (progress != State.full && !doggo.INTERACT && (doggo.transform.position - transform.position).magnitude < 0.5) {
    //         doggo.InteractWithDoggo(this);
    //     }
    // }

    public void Progress() {
        if (progress == State.zero) {
            progress = State.half;
            doggo.loadable.Score += 100;
        } else if (progress == State.half) {
            progress = State.full;
            doggo.loadable.Score += 250;
        }
    }
}
