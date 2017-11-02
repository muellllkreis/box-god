using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatCollider : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("AI");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "endBlock") 
		{
			player.GetComponent<PlayerController>().speed = 1.5f;
		}

	}
}
