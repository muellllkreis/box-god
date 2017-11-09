using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Transform canvas;
	public float menuDelay = 1.0f;
	public static GameManager instance = null;

	// Use this for initialization
	void Start () {
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1.0f;

	}
	
	// Update is called once per frame
	void Update () {

		//code for pausing the game when esc or space are pressed
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) {
			if (canvas.gameObject.activeInHierarchy == false) {
				canvas.gameObject.SetActive (true);
				Time.timeScale = 0.0f;
			} else {
				canvas.gameObject.SetActive (false);
				Time.timeScale = 1.0f;
			}
		}

		if (playerAI.isDead(true)) {
			canvas.gameObject.SetActive (true);
		}


	}

	public void ContinueLevel() {
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1.0f;
	}

	public void RestartLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void QuitGame() {
		Application.Quit ();
	}

}
