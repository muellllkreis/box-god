using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    RaycastHit hit;
    Vector3 size;
    public Vector3 jump;
    public float jumpForce = 0.1f;
    public float springJumpForce;
    private float distToGround;
    public Vector3 movement;
    public LayerMask ignoreLayer;

    private Vector3 velocity;
    private float minVelocity = 0.1f;
    private bool goLeft;
    private Vector3 previous;
    private float timeStamp = 0.0f;
    private List<Vector3> blocked = new List<Vector3>();
    private float tempspeed;
    private float bounceForce = 2f;
    bool bounce;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        size = GetComponent<Collider>().bounds.size;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        movement = new Vector3(speed, 0.0f, 0.0f);
        distToGround = GetComponent<Collider>().bounds.extents.y;
        bounce = false;
    }
	
	// Update is called once per frame
	void Update () {

        // compute velocity
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


        //Debug.Log("Velocity: " + velocity + " " + isBlocked() + " " + isGrounded());
        transform.position += new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
        ComputeVelocity();
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
    }

    protected void ComputeVelocity()
    {
        if((velocity.x < minVelocity) && isGrounded() && isBlocked())
        {
            if(!bounce)
            {
                bounce = true;
                tempspeed = speed;
                speed = 0;
                GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0.0f, 0.0f) * bounceForce, ForceMode.Impulse);
                speed = tempspeed;
            }
            bounce = false;
        }
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
