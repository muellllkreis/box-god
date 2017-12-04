using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPortal : MonoBehaviour {

    private GameObject gm;
    public AudioClip wellDone;
    AudioSource source;

    void Start()
    {
        this.gm = GameObject.FindGameObjectWithTag("GameManager");
        this.source = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player") {
            this.source.clip = wellDone;
            this.source.Play();
            gm.GetComponent<GameManager>().ShowStats();
		}
	}

}
