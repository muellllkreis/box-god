using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyancy : MonoBehaviour {

	public float waterLevel = 3;
	public float floatHeight = 1.5f;
	public float bounceDamp = 3f;
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