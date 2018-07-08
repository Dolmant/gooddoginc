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
	public float pullTensionIncrease;
	public float pullTensionDecrease;
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
			float d = Vector3.Distance(masterLeashPoint.position, slaveLeashPoint.position);

			if (d > breakRange) {
				// Break leash
				// slaveRb.AddForce();
			}
			
			else if (d > pullRange) {
				IncreaseTension();
			} else {
				DecreaseTension();
			}
			
			// Pull slave towards master
			if (pullingTime > 0f && d > pullRange) {
				slaveMovement.gravity = (pullRange - d) * pullForce * pullingTime *
										(slaveLeashPoint.position - masterLeashPoint.position).normalized;
			}
			
		}
	}

	void Update()
	{
		UpdateLineGraphic();
	}

	void IncreaseTension()
	{
		pullingTime = Mathf.Min(1f, pullingTime + Time.deltaTime * pullTensionIncrease);
	}

	void DecreaseTension()
	{
		pullingTime = Mathf.Max(0f, pullingTime - Time.deltaTime * pullTensionDecrease);
	}

	void UpdateLineGraphic()
	{
		float colorFactor = Mathf.Max(0f, 1f - pullingTime / 2f);
		line.endColor = new Color(1f, colorFactor, colorFactor);
		
		line.SetPosition(0, masterLeashPoint.position);
		line.SetPosition(1, slaveLeashPoint.position);
	}
}
