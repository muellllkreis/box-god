using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private static MusicManager instance = null;
    public static MusicManager Instance
    {
        get { return instance; }
    }
    public AudioClip backgroundMusic;
    AudioSource source;


    void Awake()
    {
        this.source = this.GetComponent<AudioSource>();
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            source.clip = backgroundMusic;
            source.volume = 0.5f;
            source.Play();
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Update () {
		
	}
}
