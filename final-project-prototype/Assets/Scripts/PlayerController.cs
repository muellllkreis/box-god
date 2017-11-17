using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //General Variables
    public float speed;
	public float boatSpeed;
    public float jumpForce = 0.1f;
    public float springJumpForce;
    Vector3 size;
    private Rigidbody rb;
    RaycastHit hit;
	private bool moveBoat = false;

    private Vector3 jump;
    private float distToGround;
    Vector3 movement;
    public LayerMask ignoreLayer;
    PlayerHealth playerHealth;

    //Variables for Bouncing
    bool bounce;
    private Vector3 velocity;
    private float minVelocity = 0.1f;
    private Vector3 previous;
    private float timeStamp = 0.0f;
    private List<Vector3> blocked = new List<Vector3>();
    private float tempspeed;
    private float bounceForce = 2f;
    public int collisionDamage;
	private GameObject boat;
	private Rigidbody boatRigidBody;

    //for switching back and forth on different parts of the level
    private bool goLeft = false;

    //Variables for Fall Damage
    private float airTime;
    public float maxFallVelocity;

    // Use this for initialization
    void Start () {
        playerHealth = this.GetComponent<PlayerHealth>(); 
        rb = GetComponent<Rigidbody>();
        size = GetComponent<Collider>().bounds.size;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        movement = new Vector3(speed, 0.0f, 0.0f);
        distToGround = GetComponent<Collider>().bounds.extents.y;
        bounce = false;
        previous = transform.position;
		if (GameObject.Find("Boat") != null) {
			boat = GameObject.Find ("Boat");
			boatRigidBody = boat.GetComponent<Rigidbody>();
		}

    }
	
	// Update is called once per frame
	void Update () {

        // compute velocity for bouncing back purposes
        Vector3 currFrameVelocity = (transform.position - previous) / Time.deltaTime;
        velocity = Vector3.Lerp(velocity, currFrameVelocity, 0.1f);
        if(previous == transform.position)
        {
            blocked.Add(transform.position);
        }
        else
        {
            blocked.Clear();
        }
        previous = transform.position;
        //BounceBack if blocked
        if (goLeft)
        {
            if ((-1 * velocity.x < minVelocity) && isGrounded() && isBlocked())
            {
                BounceBack();
            }
        }
        else if ((velocity.x < minVelocity) && isGrounded() && isBlocked())
        {
            BounceBack();
        }
        if((velocity.y < maxFallVelocity) && isGrounded()) 
        {
            Debug.Log(playerHealth.currentHealth);
            playerHealth.TakeDamage((int) velocity.y * (-5));
            Debug.Log(playerHealth.currentHealth);
        }

        //Debug.Log("Velocity: " + velocity + " " + isBlocked() + " " + isGrounded());
        transform.position += new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;

		if (moveBoat == true) 
		{
			boatRigidBody.velocity = boat.transform.forward + new Vector3(boatSpeed, 0.0f, 0.0f);
			//boat.transform.position += new Vector3(1.5f, 0.0f, 0.0f) * Time.deltaTime;
		}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Plank")
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            collision.gameObject.tag = "used";
        }
        if (collision.gameObject.tag == "Spring")
        {
            rb.AddForce(jump * springJumpForce, ForceMode.Impulse);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 currFrameVelocity = (transform.position - previous) / Time.deltaTime;
            velocity = Vector3.Lerp(velocity, currFrameVelocity, 0.1f);
            if (!((velocity.x < minVelocity) && isGrounded() && isBlocked()))
            {
                BounceBack();
            }
        }
		if (collision.gameObject.tag == "Boat") 
		{
			speed = 0;
			moveBoat = true;
		}

		if (collision.gameObject.tag == "endBlock") 
		{
			speed = 1.5f;
			moveBoat = false;
		}

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerSwitchBox")
        {
            goLeft = !goLeft;
            speed = -1 * speed;
        }
    }

    protected void BounceBack()
    {
            if(!bounce)
            {
                bounce = true;
                tempspeed = speed;
                speed = 0;
                GetComponent<Rigidbody>().AddForce(new Vector3(-1 * tempspeed, 0.0f, 0.0f) * bounceForce, ForceMode.Impulse);
                speed = tempspeed;
                if(playerHealth.currentHealth > 0)
                {
                    playerHealth.TakeDamage(collisionDamage);
                }
            }
            bounce = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Plank")
        {
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    bool isBlocked()
    {
        return Physics.Raycast(transform.position, new Vector3(speed, 0.0f, 0.0f), distToGround + 0.1f);
    }
}
