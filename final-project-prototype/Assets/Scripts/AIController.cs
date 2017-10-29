using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    public float speed = 0;
    Vector3 size;
    private Rigidbody rb;
    RaycastHit hit;
    Vector3 movement;
    public LayerMask ignoreLayer;
    private bool goLeft = true;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
        size = GetComponent<Collider>().bounds.size;
        movement = new Vector3(speed, 0.0f, 0.0f);

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += movement * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (goLeft)
        {
            goLeft = false;
            movement = new Vector3(-1 * speed, 0.0f, 0.0f);
        }
        else
        {
            movement = new Vector3(speed, 0.0f, 0.0f);
            goLeft = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "EnemySwitchBox")
        {
            if (goLeft)
            {
                goLeft = false;
                movement = new Vector3(-1 * speed, 0.0f, 0.0f);
            }
            else
            {
                movement = new Vector3(speed, 0.0f, 0.0f);
                goLeft = true;
            }
        }
        
    }
}
