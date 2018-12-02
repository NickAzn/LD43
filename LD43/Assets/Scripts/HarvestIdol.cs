using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestIdol : MonoBehaviour {

    int boost;

	// Use this for initialization
	void Start () {
        boost = Random.Range(0, 3);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int Activate () {
        return boost;
    }

    private void OnMouseUpAsButton()
    {
        Activate();
    }
}
