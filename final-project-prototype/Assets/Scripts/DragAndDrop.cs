using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    Vector3 dist;
    float posX;
    float posY;
    Rigidbody rb;
    Camera main;
    bool active;
    Ray ray;
    RaycastHit hit;
    Vector3 originalPos;

    GameObject player;
    PlayerHealth playerHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        originalPos = transform.position;
        this.active = true;
        rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
        main = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    void OnMouseDown()
    {
        if(active)
        {
            dist = main.WorldToScreenPoint(transform.position);
            posX = Input.mousePosition.x - dist.x;
            posY = Input.mousePosition.y + dist.y;
        }
    }

    private void OnMouseDrag()
    {
        if (active)
        {
            //Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
            Vector3 curPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist.z);
            Vector3 worldPos = main.ScreenToWorldPoint(curPos);
            transform.position = worldPos;
        }
    }

    private void OnMouseUp()
    {
        //Check if object has been placed back into the GUI
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "GUIChecker")
            {
                transform.position = originalPos;
                return;
            }
        }

        if(transform.tag == "Explosive")
        {
            StartCoroutine(ExplosiveCountdown());
        }

		if (transform.tag == "Boat") {
			GameObject varGameObject = GameObject.Find ("Boat");
			varGameObject.GetComponent<Bouyancy> ().enabled = true;
			varGameObject.GetComponent<DragAndDrop> ().enabled = false;
			this.active = true;
			rb.useGravity = true;
		} else {
			//Make Object immovable and a collider for the level
			rb.useGravity = true;
			//rb.mass = 1000;
			this.active = false;
			BoxCollider boxcollider = GetComponent <BoxCollider> ();
			boxcollider.size = new Vector3(1, 1, 1);
			gameObject.layer = LayerMask.NameToLayer("3D GUI");
		}
        


    }

    public IEnumerator ExplosiveCountdown(float countdownValue = 3)
    {
        float currCountdownValue = countdownValue;
        while(currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        transform.tag = "ExplosiveActive";
        rb.AddForce(new Vector3(0.0f, 0.01f, 0.0f) * 0.1f, ForceMode.Impulse);
        if(Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            playerHealth.TakeDamage(80);
        }
        Destroy(gameObject, 0.1f);
    } 
}
