using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leash : MonoBehaviour
{
	private LineRenderer line;
	public GameObject master;
	public GameObject slave;

	public float pullRange;
	public float breakRange;
	public float pullForce;
	public float pullForceIncrease;
	private float pullingTime = 0;

	private Movement masterMovement;
	private Movement slaveMovement;
	private Transform masterLeashPoint;
	private Transform slaveLeashPoint;
	
	void Start ()
	{
		line = GetComponent<LineRenderer>();

		masterLeashPoint = master.transform.Find("Leash point");
		slaveLeashPoint = slave.transform.Find("Leash point");

		masterMovement = master.GetComponent<Movement>();
		slaveMovement = slave.GetComponent<Movement>();
	}
	
	void FixedUpdate () {
		if (master && slave)
		{
			line.SetPosition(0, masterLeashPoint.position);
			line.SetPosition(1, slaveLeashPoint.position);

			float d = Vector3.Distance(masterLeashPoint.position, slaveLeashPoint.position);

			if (d > breakRange)
			{
				// Break leash
				// slaveRb.AddForce();
			}
			
			else if (d > pullRange)
			{
				pullingTime += Time.deltaTime * pullForceIncrease;
				
				// Pull slave towards master
				slaveMovement.gravity = (pullRange - d) * pullForce * pullingTime *
				                        (slaveLeashPoint.position - masterLeashPoint.position).normalized;
				
				Debug.Log(pullingTime);
			}
			else
			{
				pullingTime = 0;
			}
			
			
		}
	}
}
