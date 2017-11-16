using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml;
using System;



public class GameManager : MonoBehaviour {

	public Transform canvas;
    public Transform stats;
    public TextAsset xmlRawFile;

	GameObject player;
	PlayerHealth playerHealth;
	public float menuDelay = 1.0f;
	public static GameManager instance = null;

    private bool isPaused;
    private int timePlayed = 0;
    private int seconds = 0;
    private int minutes = 0;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		canvas.gameObject.SetActive (false);
        stats.gameObject.SetActive(false);
		Time.timeScale = 1.0f;
        StartCoroutine("PlayTime");
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

		if (playerHealth.isDead) {
			canvas.gameObject.SetActive (true);
			GameObject continueButton = GameObject.Find ("ContinueButton");
			continueButton.GetComponent<Button> ().interactable = false;
			GameObject menuText = GameObject.Find ("PausedText");
			menuText.GetComponent<Text> ().text = "Game Over";
		}

	}

    public void ShowStats()
    {
        stats.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        GameObject healthval = GameObject.Find("HealthValue");
        GameObject timeval = GameObject.Find("TimeValue");
        GameObject objval = GameObject.Find("ObjectValue");

        string data = xmlRawFile.text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(data));

        string xmlPathPattern = "//levels/level";
        XmlNodeList levels = xmlDoc.SelectNodes(xmlPathPattern);
        foreach(XmlNode level in levels)
        {
            if(level.Attributes["name"].Value == SceneManager.GetActiveScene().name)
            {
                XmlNode time = level.FirstChild;
                XmlNode objects = time.NextSibling;
                bool perfectTime = PerfectTime(Int32.Parse(time.InnerXml));
                bool perfectObject = PerfectObjectCount(Int32.Parse(objects.InnerXml));
                bool perfectHealth = (playerHealth.currentHealth == 100);
                int score = (perfectTime ? 1 : 0) + (perfectObject ? 1 : 0) + (perfectHealth ? 1 : 0);
                if(score == 0)
                {
                    score = 1;
                }
                Image stars = GameObject.Find(score.ToString() + "StarRating").GetComponent<Image>();
                GameObject comment = GameObject.Find("CommentText");
                stars.enabled = true;
                GenerateHeader(score);
                if (perfectTime && perfectObject && perfectHealth)
                {
                    comment.GetComponent<Text>().text = "Perfect, it does not get much better than this! Bring on the next level!";
                }
                else
                {
                    if(!perfectTime)
                    {
                        comment.GetComponent<Text>().text = "Try to finish the level in less time. Avoid walls and build slopes! " + "We think it should only take " + Int32.Parse(time.InnerXml) + " seconds!";
                    }
                    else if(!perfectObject)
                    {
                        comment.GetComponent<Text>().text = "Try beating the level with less object(s). You can do it with " + Int32.Parse(objects.InnerXml) + " objects! Be creative!";
                    }
                    else
                    {
                        comment.GetComponent<Text>().text = "Try finishing the level without losing health! You can do this!";
                    }
                }
            }
        }

        healthval.GetComponent<Text>().text = playerHealth.currentHealth + "%";
        objval.GetComponent<Text>().text = player.GetComponent<PlayerController>().objectsUsed.ToString();
        timeval.GetComponent<Text>().text = minutes.ToString() + ":" + seconds.ToString();
        
    }

    public void NextLevel()
    {
        int levelNum = SceneManager.GetActiveScene().buildIndex;
        levelNum++;
        SceneManager.LoadScene(levelNum);
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

    private void GenerateHeader(int score)
    {
        GameObject title = GameObject.Find("StatsHeader");
        if (score == 1)
        {
            title.GetComponent<Text>().text = "At least you made it to the end...";
        }
        else if(score == 2)
        {
            title.GetComponent<Text>().text = "Not bad!";
        }
        else
        {
            title.GetComponent<Text>().text = "Perfect score!";
        }
    }

    private bool PerfectTime(int reftime)
    {
        int curtime = timePlayed;
        if (reftime - curtime >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool PerfectObjectCount(int refobjs)
    {
        int curobjs = player.GetComponent<PlayerController>().objectsUsed;
        if (refobjs >= curobjs)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator PlayTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timePlayed += 1;
            seconds = (timePlayed % 60);
            minutes = (timePlayed / 60) % 60;
        }
    }

}