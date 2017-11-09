﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void PlayGame() {
		SceneManager.LoadScene ("level 1");
	}

	public void QuitGame() {
		Application.Quit ();
	}

}
