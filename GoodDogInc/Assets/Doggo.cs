using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour {
    public Vector2 target;
    public float speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (target - (Vector2)transform.position).normalized * speed + (Vector2)transform.position;
    }
}
