using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTerrain : MonoBehaviour {
    public Transform BrokenTerrain;
    public Transform effect;
    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "ExplosiveActive")
        {
            rb.isKinematic = false;
            BreakDeath();
        }
    }

    private void BreakDeath()
    {
        Instantiate(BrokenTerrain, transform.position, BrokenTerrain.transform.rotation);
        Destroy(gameObject);
    }
}
