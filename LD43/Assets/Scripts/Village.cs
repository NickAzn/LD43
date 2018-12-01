using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject personPrefab;
    public House house;
    public Farm farm;
    public Construction construction;
    public Tavern tavern;

    List<Item> itemList = new List<Item>();

    int sacBalance;
    int curDay = 1;
    float curDayTime = 0f;

    Person selectedPerson;


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
            GameObject ob = Instantiate(personPrefab);
            Person p = ob.GetComponent<Person>();
            peopleList.Add(p);
            house.AddToHouse(p);
        }
        UIManager.instance.UpdatePersonCountText(peopleList.Count);
	}
	
	// Update is called once per frame
	void Update () {
        curDayTime += Time.deltaTime;
        if (curDayTime >= dayLength)
            EndDay();
        UIManager.instance.UpdateDayTimeText((int)(dayLength - curDayTime));
	}

    //Ends the day and starts new day
    void EndDay() {
        if (sacBalance <= 0) {
            curDayTime = 0;
            curDay++;
            food -= peopleList.Count * peopleFoodCost;
            food += farm.Work();
            UIManager.instance.UpdateFoodText(food);
            sacBalance = sacPerDay * curDay;
        } else {
            EndGame();
        }
    }

    //End game
    void EndGame() {
        Time.timeScale = 0;
    }

    //Sacrifices person
    void Sacrifice(Person p) {
        sacBalance--;
        UIManager.instance.UpdateSacBalanceText(sacBalance);
        Destroy(p.gameObject);
    }

    public void AssignFarmJob() {
        if (selectedPerson != null) {
            if (farm.HireWorker(selectedPerson)) {
                selectedPerson.job = 1;
                DeselectPerson();
            }
        }
    }

    public void AssignBuilderJob() {
        if (selectedPerson != null) {
            if (construction.HireWorker(selectedPerson)) {
                selectedPerson.job = 2;
                DeselectPerson();
            }
        }
    }

    public void AssignTreasureJob() {
        if (selectedPerson != null) {
            if (tavern.HireWorker(selectedPerson)) {
                selectedPerson.job = 3;
                DeselectPerson();
            }
        }
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
        selectedPerson = p;
    }

    public void DeselectPerson() {
        selectedPerson = null;
    }
}
