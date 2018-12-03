using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Class to display UI for a person
public class UIPerson : MonoBehaviour {

    public Person person;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI skillsText;

	public void UpdateDisplay() {
        if (person != null) {
            nameText.text = person.personName;
            string jobName = "N/A";
            if (person.job == 1) {
                jobName = "Farmer";
            } else if (person.job == 2) {
                jobName = "Builder";
            } else if (person.job == 3) {
                jobName = "Explorer";
            }
            skillsText.text = "Job: " + jobName
                               + "\nFertility: " + person.GetFertilSkill()
                               + "\nFarming: " + person.GetFarmSkill()
                               + "\nBuilding: " + person.GetBuildSkill()
                               + "\nExploring: " + person.GetHuntSkill();
        }
    }

    public void SelectPerson() {
        Village v = Village.instance;
        v.SelectPerson(person);
        v.GetPersonHighlighter().transform.SetParent(transform);
        v.GetPersonHighlighter().transform.localPosition = new Vector3(0, 0, 0);
    }
}
