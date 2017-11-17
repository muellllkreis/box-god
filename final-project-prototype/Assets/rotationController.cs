using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationController : MonoBehaviour {
	private float lastZRotation;
	private float currentZRotation; 
	public float dampRotation = 0.5f;
	private float rotationDifference;
	public float speed = 0.1F;

	public float torque;
	public Rigidbody rb;
	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		currentZRotation = transform.rotation.z;
		print ("current rotation 1: " + currentZRotation);
		print ("last rotation 2: " + lastZRotation);

		lastZRotation = transform.rotation.z;

		rotationDifference = lastZRotation - currentZRotation;

		if ((rotationDifference) < 2 && rotationDifference > 0) {
			if (currentZRotation >= 90 && currentZRotation <= 180) {
				rb.AddTorque (0,0,-3);
			}
			if (currentZRotation >= -180 && currentZRotation <= -90) {
				rb.AddTorque (0,0,3);
			}


		} 
	}
}
