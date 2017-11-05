using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPortal : MonoBehaviour {

	public void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player") {
			int levelNum = SceneManager.GetActiveScene ().buildIndex;
			levelNum++;
			SceneManager.LoadScene (levelNum);

		}
	}

}
