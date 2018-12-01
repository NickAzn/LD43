using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    private float rndNumber;
    
    public string gender;
    public int age;
    public bool homeless;
    public int job;
    public bool pregnant;
    
	// Use this for initialization
	void Start () {
        rndNumber = Random.Range(1f, 2f);

        if(rndNumber == 1){
            gender = "male";
        }
        else {
            gender = "female";
        }

        age = 0;
        homeless = true;
        job = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
