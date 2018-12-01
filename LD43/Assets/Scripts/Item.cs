using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public float farming;
    public float sex;
    public float building;
    public float hunting;

	// Use this for initialization
	void Start () {
        farming = Random.Range(0f, 3f);
        sex = Random.Range(0f, 3f);
        building = Random.Range(0f, 3f);
        hunting = Random.Range(0f, 3f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Increase() {

    }
}
