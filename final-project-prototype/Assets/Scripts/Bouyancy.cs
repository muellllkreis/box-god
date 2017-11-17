using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyancy : MonoBehaviour {

	public float waterLevel = 2.3f;
	public float floatHeight = 0.1f;
	public float bounceDamp = 0.5f;
	public Vector3 buoyancyCenterOffset;

	float forceFactor;
	Vector3 actionPoint;
	Vector3 upLift;

	private void Update()
	{
		actionPoint = transform.position + transform.TransformDirection(buoyancyCenterOffset);
		forceFactor = 1 - ((actionPoint.y - waterLevel) / floatHeight);

		if(forceFactor > 0)
		{
			upLift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
			GetComponent<Rigidbody>().AddForceAtPosition(upLift, actionPoint);
		}
	}
}﻿