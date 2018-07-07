using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leash : MonoBehaviour
{
	private LineRenderer line;
	public Transform master;
	public Transform slave;
	
	// Use this for initialization
	void Start ()
	{
		line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (master && slave)
		{
			line.SetPosition(0, master.position);
			line.SetPosition(1, slave.position);
		}
	}
}
