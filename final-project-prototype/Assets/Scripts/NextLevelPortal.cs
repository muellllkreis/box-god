using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPortal : MonoBehaviour {

    private GameObject gm;

    void Start()
    {
        this.gm = GameObject.FindGameObjectWithTag("GameManager");
    }

    public void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player") {
            gm.GetComponent<GameManager>().ShowStats();
		}
	}

}
