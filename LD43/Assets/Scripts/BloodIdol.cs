using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodIdol : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Activate()
    {
        //reduce # of sacrifices needed for that night
    }

    private void OnMouseUpAsButton()
    {
        Activate();
    }
}
