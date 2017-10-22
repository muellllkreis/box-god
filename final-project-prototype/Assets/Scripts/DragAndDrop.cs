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

    private void Start()
    {
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
            Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
            Vector3 worldPos = main.ScreenToWorldPoint(curPos);
            transform.position = worldPos;
        }
    }

    private void OnMouseUp()
    {
        rb.useGravity = true;
        rb.mass = 1000;
        this.active = false;
        gameObject.layer = LayerMask.NameToLayer("3D GUI");
    }
}
