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
    private float distToGround;
    private float collisionSpeed;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
        size = GetComponent<Collider>().bounds.size;
        distToGround = GetComponent<Collider>().bounds.extents.y;
        movement = new Vector3(speed, 0.0f, 0.0f);
        collisionSpeed = speed;
        if (PlayerController.collisionSpeed > speed)
        {
            collisionSpeed = PlayerController.collisionSpeed;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(isBlocked())
        {
            if (goLeft)
            {
                speed = speed * -1;
                goLeft = false;
                movement = new Vector3(speed, 0.0f, 0.0f);
            }
            else
            {
                speed = speed * -1;
                movement = new Vector3(speed, 0.0f, 0.0f);
                goLeft = true;
            }
        }
        transform.position += movement * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (goLeft)
        {
            speed = speed * -1;
            goLeft = false;
            movement = new Vector3(speed, 0.0f, 0.0f);
        }
        else
        {
            speed = speed * -1;
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
                speed = speed * - 1;
                movement = new Vector3(speed, 0.0f, 0.0f);
            }
            else
            {
                speed = speed * - 1;
                movement = new Vector3(speed, 0.0f, 0.0f);
                goLeft = true;
            }
        }
    }

    bool isBlocked()
    {
        if (!goLeft)
        {
            return Physics.Raycast(transform.position, new Vector3(-.5f*collisionSpeed, 0.0f, 0.0f), distToGround + 0.1f);
        }
        return Physics.Raycast(transform.position, new Vector3(.5f*collisionSpeed, 0.0f, 0.0f), distToGround + 0.1f);
    }
}
