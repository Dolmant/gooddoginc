using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.Collections;
using UnityEngine;

public class Leash : MonoBehaviour
{
	private LineRenderer line;
	public GameObject master;
	public GameObject slave;

	public float tensionLightD;
	public float tensionLightForce;
	public float tensionHeavyD;
	public float tensionHeavyForce;
	public float tensionLightReverseForce;

	public float minWidth;
	public float maxWidth;
	private float length;

	public float tensionHeavyTime;
	private float tensionHeavyTimeCurrent;
	
	public float breakRange;
	public float pullTensionIncrease;
	public float pullTensionDecrease;
	private float pullingTime;

	private float tension;

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
		if (!master || !slave)
			return;

		length = Vector3.Distance(masterLeashPoint.position, slaveLeashPoint.position);

		if (tensionHeavyTimeCurrent > 0)
		{
			// Pulling towards human
			tensionHeavyTimeCurrent -= Time.deltaTime;
			slaveMovement.gravity = -tensionHeavyForce *
			                        (slaveLeashPoint.position - masterLeashPoint.position).normalized;
			return;
		}
		
		
		if (length > breakRange) {
			// Break leash
		}

		else if (length > tensionHeavyD)
		{
			// Pull hard towards human for a short time
			tensionHeavyTimeCurrent = tensionHeavyTime;
		}
		else if (length > tensionLightD)
		{
			IncreaseTension();
			
			// Pull slave lightly over time towards master
			slaveMovement.gravity = -Mathf.Pow(tensionLightD - length, 2f) * tensionLightForce * pullingTime *
									(slaveLeashPoint.position - masterLeashPoint.position).normalized;
			
			// Also pull master slightly towards slave
			masterMovement.gravity = -Mathf.Pow(tensionLightD - length, 2f) * tensionLightReverseForce * pullingTime *
			                         (masterLeashPoint.position - slaveLeashPoint.position).normalized;
			
			
//			if (pullingTime >= 1f)
//			{
//				// Pull hard towards human for a short time
//				pullingTime = 0f;
//				tensionHeavyTimeCurrent = tensionHeavyTime;
//			}
		}
		else
		{
			DecreaseTension();
			slaveMovement.gravity = Vector3.zero;
		}
		
//		else if (length > pullRange) {
//			IncreaseTension();
//		} else {
//			DecreaseTension();
//		}
//		
//		// Pull slave towards master
//		if (pullingTime > 0f && length > pullRange) {
//			slaveMovement.gravity = -Mathf.Pow(pullRange - length, 2f) * pullForce * pullingTime *
//									(slaveLeashPoint.position - masterLeashPoint.position).normalized;
//		}
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
		float colorFactor = Mathf.Max(0f, 1f - pullingTime);
		line.endColor = new Color(1f, colorFactor, colorFactor);

		
		// Change line width based on current length of leash. 
		// Start thinning at the 'tension light distance', and minimum width is reached at 'tension heavy distance'
		line.startWidth = minWidth +
		                  (maxWidth - minWidth) * 
		                  (1 - Mathf.Max(0f, length - tensionLightD) / (tensionHeavyD - tensionLightD));
		
		line.SetPosition(0, masterLeashPoint.position);
		line.SetPosition(1, slaveLeashPoint.position);
	}
}
