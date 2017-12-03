using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //General Variables
    public float moveSpeed = 1.5f;
    public static float collisionSpeed;
    public float jumpForce = 0.1f;
    public float springJumpForce;
    Vector3 size;
    private Rigidbody rb;
    RaycastHit hit;

    private Vector3 jump;
    private float distToGround;
    Vector3 movement;
    public LayerMask ignoreLayer;
    PlayerHealth playerHealth;

    //Variables for Bouncing
    public float bounceTime = 2f;
    private bool bouncing = false;
    private Vector3 velocity;
    public Vector3 previous;
    private float timeStamp = 0.0f;
    private List<Vector3> blocked = new List<Vector3>();
    public int collisionDamage;
    public LayerMask bounceMask;

    //for switching back and forth on different parts of the level
    private bool goLeft = false;

    //Variables for Fall Damage
    private float airTime;
    public float maxFallVelocity;

    Coroutine groundDamage = null;

    //Variables for Stats
    public int objectsUsed = 0;
    int timePassed = 0;

    // Use this for initialization
    void Start () {
        playerHealth = this.GetComponent<PlayerHealth>(); 
        rb = GetComponent<Rigidbody>();
        size = GetComponent<Collider>().bounds.size;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        movement = new Vector3(moveSpeed, 0.0f, 0.0f);
        distToGround = GetComponent<Collider>().bounds.extents.y;
        previous = transform.position;
        PlayerController.collisionSpeed = moveSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 currFrameVelocity;
        // compute velocity for bouncing back purposes
        if (Time.deltaTime == 0.0f)
        {
            currFrameVelocity = (transform.position - previous);
        }
        else
        {
            currFrameVelocity = (transform.position - previous) / Time.deltaTime;
        }

        velocity = Vector3.Lerp(velocity, currFrameVelocity, 0.1f);

        Debug.Log(velocity.y);
        if(previous == transform.position)
        {
            blocked.Add(transform.position);
        }
        else
        {
            blocked.Clear();
        }
        previous = transform.position;
        if (bouncing && Time.time - timeStamp > bounceTime)
        {
            bouncing = false;
            goLeft = !goLeft;
            moveSpeed = -1 * moveSpeed;
        }

        if (isGrounded() && isBlocked() && !bouncing)
        {
            bouncing = true;
            timeStamp = Time.time;
            goLeft = !goLeft;
            moveSpeed = -1 * moveSpeed;
            playerHealth.TakeDamage(collisionDamage);
        }
        //Debug.Log("Velocity: " + velocity + " " + isBlocked() + " " + isGrounded());
        transform.position += new Vector3(moveSpeed, 0.0f, 0.0f) * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((velocity.y < maxFallVelocity) && isGrounded())
        {
            if(!(collision.gameObject.tag == "Pad") && !(collision.gameObject.tag == "Finish"))
            {
                Debug.Log(playerHealth.currentHealth);
                playerHealth.TakeDamage((int)velocity.y * (-8));
                Debug.Log(playerHealth.currentHealth);
            }
        }
        if (collision.gameObject.tag == "Plank")
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            collision.gameObject.tag = "used";
        }
        if (collision.gameObject.tag == "Spring")
        {
            rb.AddForce(jump * springJumpForce, ForceMode.Impulse);
        }
        if (collision.gameObject.tag == "Spikes")
        {
            playerHealth.TakeDamage(playerHealth.currentHealth);
        }
        if (collision.gameObject.tag == "DamageGround")
        {
            groundDamage = StartCoroutine(TakeDamageOnGround(5));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "DamageGround")
        {
            Debug.Log("Exit Collision");
            StopCoroutine(groundDamage);
        }
    }

    public IEnumerator TakeDamageOnGround(float damage)
    {
        while(true)
        {
            playerHealth.TakeDamage(5);
            yield return new WaitForSeconds(0.7f);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerSwitchBox")
        {
            goLeft = !goLeft;
            moveSpeed = -1 * moveSpeed;
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    bool isBlocked()
    {
        return Physics.Raycast(transform.position, new Vector3(moveSpeed * .5f, 0.0f, 0.0f), distToGround + 0.1f, bounceMask);
    }
}
