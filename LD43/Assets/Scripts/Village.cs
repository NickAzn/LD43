﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class Village : MonoBehaviour {

    public static Village instance;

    // Game stats given in editor
    public float startFood;
    public float peopleFoodCost;
    public float dayLength;
    public int sacPerDay;
    public int startPersonCount;

    List<Person> peopleList = new List<Person>();
    float food;

    // Declared in editor
    public GameObject personHighlighterPrefab;
    GameObject personHighlighter;
    public House house;
    public Farm farm;
    public Construction construction;
    public Tavern tavern;
    public AudioClip endDaySound;

    List<Item> itemList = new List<Item>();

    int sacBalance;
    int curDay = 1;
    float curDayTime = 0f;
    bool isNight = false;

    Person selectedPerson;

    public List<string> availNames;


	// Use this for initialization
	void Awake () {
        //singleton
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        

        food = startFood;
        UIManager.instance.UpdateFoodText(food);
        sacBalance = 1;
        UIManager.instance.UpdateSacBalanceText(sacBalance);

        for (int i = 0; i < startPersonCount; i++) {
            Person p = new Person();
            peopleList.Add(p);
            house.AddToHouse(p);
        }
        UIManager.instance.UpdatePersonCountText(peopleList.Count);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isNight) {
            curDayTime += Time.deltaTime;
            if (curDayTime >= dayLength)
                EndDay();
            UIManager.instance.UpdateDayTimeText((int)(dayLength - curDayTime));
        }

        if (personHighlighter == null)
            personHighlighter = Instantiate(personHighlighterPrefab);
	}

    //Ends the day and starts new day
    public void EndDay() {
        SoundManager.instance.PlaySfx(endDaySound);
        curDayTime = dayLength - .01f;
        UIManager.instance.UpdateDayTimeText((int)(dayLength - curDayTime));
        food -= peopleList.Count * peopleFoodCost;
        food += farm.Work();
        tavern.Work();
        UIManager.instance.UpdateFoodText(food);
        if (food < 0)
            EndGame();
        else
            StartNight();
    }

    //displays nightUI
    void StartNight() {
        isNight = true;
        DeselectPerson();
        UIManager.instance.ShowNightUI();
    }

    public void EndNight() {
        if (sacBalance > 0) {
            UIManager.instance.HideNightUI();
            EndGame();
        } else {
            isNight = false;
            curDayTime = 0;
            curDay++;
            sacBalance = sacPerDay * curDay;
            UIManager.instance.HideNightUI();
        }
    }

    //End game
    void EndGame() {
        UIManager.instance.ShowGameOverUI();
        Time.timeScale = 0;
    }

    //Sacrifices person
    public void Sacrifice() {
        if (selectedPerson != null) {
            sacBalance--;
            UIManager.instance.UpdateSacBalanceText(sacBalance);
            UIManager.instance.ShowAllPeopleUI();
            Kill(selectedPerson);
        }
    }

    void Kill(Person p) {
        peopleList.Remove(p);
        if (p.job != 0)
            RemovePersonJob(p);
        if (p.homeless == false)
            house.RemovePerson(p);
        availNames.Add(p.personName);
        if (selectedPerson == p)
            DeselectPerson();
        p = null;
    }

    public void AssignFarmJob() {
        if (selectedPerson != null) {
            if (farm.HireWorker(selectedPerson)) {
                RemovePersonJob();
                selectedPerson.job = 1;
                DeselectPerson();
            }
        }
    }

    public void AssignBuilderJob() {
        if (selectedPerson != null) {
            if (construction.HireWorker(selectedPerson)) {
                RemovePersonJob();
                selectedPerson.job = 2;
                DeselectPerson();
            }
        }
    }

    public void AssignTreasureJob() {
        if (selectedPerson != null) {
            if (tavern.HireWorker(selectedPerson)) {
                RemovePersonJob();
                selectedPerson.job = 3;
                DeselectPerson();
            }
        }
    }

    public void RemovePersonJob() {
        if (selectedPerson.job == 1)
            farm.RemoveWorker(selectedPerson);
        else if (selectedPerson.job == 2)
            construction.RemoveWorker(selectedPerson);
        else if (selectedPerson.job == 3)
            tavern.RemoveWorker(selectedPerson);
    }

    void RemovePersonJob(Person p) {
        if (p.job == 1)
            farm.RemoveWorker(p);
        else if (p.job == 2)
            construction.RemoveWorker(p);
        else if (p.job == 3)
            tavern.RemoveWorker(p);
    }

    public void AssignHome() {
        if (selectedPerson != null) {
            if (house.AddToHouse(selectedPerson)) {
                selectedPerson.homeless = false;
                DeselectPerson();
            }
        }
    }

    public void SelectPerson(Person p) {
        if (selectedPerson != null) {
            DeselectPerson();
        }
        personHighlighter.SetActive(true);
        selectedPerson = p;
    }

    public void DeselectPerson() {
        selectedPerson = null;
        personHighlighter.SetActive(false);
    }

    public Person GetSelectedPerson() {
        return selectedPerson;
    }

    public List<Person> GetPeopleList() {
        return peopleList;
    }

    public string GetRandomName() {
        if (availNames.Count == 0) {
            return "No Name";
        } else {
            int rand = Random.Range(0, availNames.Count);
            string name = availNames[rand];
            availNames.RemoveAt(rand);
            return name;
        }
    }

    public GameObject GetPersonHighlighter() {
        return personHighlighter;
    }
}
