using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leash : MonoBehaviour
{
	private LineRenderer line;
	public GameObject master;
	public GameObject slave;

	public float leashPullRange;
	public float leashBreakRange;

	private Rigidbody2D masterRb;
	private Rigidbody2D slaveRb;
	private Transform masterLeashPoint;
	private Transform slaveLeashPoint;
	
	void Start ()
	{
		line = GetComponent<LineRenderer>();

		masterLeashPoint = master.transform.Find("Leash point");
		slaveLeashPoint = slave.transform.Find("Leash point");

		masterRb = master.GetComponent<Rigidbody2D>();
		slaveRb = slave.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (master && slave)
		{
			line.SetPosition(0, masterLeashPoint.position);
			line.SetPosition(1, slaveLeashPoint.position);

			float d = Vector3.Distance(masterLeashPoint.position, slaveLeashPoint.position);

			if (d > leashBreakRange)
			{
				// Break leash
				// slaveRb.AddForce();
			}
			
			else if (d > leashPullRange)
			{
				// Pull slave towards master
				
			}
			
			
		}
	}
}
