using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCamera : MonoBehaviour
{
	public Transform targetMain;
	public Transform targetSecondary;
	
	public Vector3 offset = new Vector3(0, 0, -10);
	
	// Update is called once per frame
	void Update ()
	{
//		float d = Vector3.Distance(targetMain.position, targetSecondary.position);
		transform.position = Vector3.Lerp(targetMain.position, targetSecondary.position, 0.5f) +offset;
	}
}
