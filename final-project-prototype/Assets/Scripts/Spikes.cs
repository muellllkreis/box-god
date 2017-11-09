using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    public Transform SpikeParent;

    // Use this for initialization
    void Start () {
        foreach (Transform child in SpikeParent)
        {
            child.gameObject.tag = "Spikes";
            Debug.Log(child.gameObject.tag);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
