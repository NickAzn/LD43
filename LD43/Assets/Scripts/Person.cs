using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    public string name;
    public int age;
    public bool homeless;
    public int job;
    public bool pregnant;

    int farmSkill;
    int fertilSkill;
    int buildSkill;
    int huntSkill;
    
    // Use this for initialization
    void Start () {

        string[] arrOfNames = new string[] { "Sean", "Nick", "Nich", "Tyler"};


        age = 0;
        homeless = true;
        job = 0;

        int statPoints = Random.Range(2, 7);
        for (int i = 0; i < statPoints; i++) {
            int rndStat = Random.Range(0, 4);
            if (rndStat == 0)
                farmSkill++;
            else if (rndStat == 1)
                fertilSkill++;
            else if (rndStat == 2)
                buildSkill++;
            else
                huntSkill++;
        }
	}
	
    public int GetFarmSkill() {
        return farmSkill;
    }

    public int GetFertilSkill() {
        return fertilSkill;
    }

    public int GetBuildSkill() {
        return buildSkill;
    }

    public int GetHuntSkill() {
        return huntSkill;
    }

    // Visual feedback to show this object is clickable
    private void OnMouseDown() {
        transform.localScale = new Vector2(1.1f, 1.1f);
    }
    private void OnMouseEnter() {
        transform.localScale = new Vector2(1.05f, 1.05f);
    }
    private void OnMouseExit() {
        transform.localScale = new Vector2(1f, 1f);
    }

    //Give visual feedback and perform an action when clicked
    private void OnMouseUpAsButton() {
        transform.localScale = new Vector2(1.05f, 1.05f);
        Village v = Village.instance;
        if (v.GetSelectedPerson() == this)
            v.DeselectPerson();
        else
            v.SelectPerson(this);
    }
}
