using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person {

    public string personName;
    public int age;
    public bool homeless;
    public int job;
    public bool pregnant;

    int farmSkill;
    int fertilSkill;
    int buildSkill;
    int huntSkill;
    
    // Use this for initialization
    public Person () {

        personName = Village.instance.GetRandomName();
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
}
