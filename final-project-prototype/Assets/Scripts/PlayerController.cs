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
    public float springJumpForce = 10000000.3f;
    private float distToGround;
    public Vector3 movement;
    public LayerMask ignoreLayer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        size = GetComponent<Collider>().bounds.size;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        movement = new Vector3(speed, 0.0f, 0.0f);
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
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
            collision.gameObject.tag = "used";
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
}
