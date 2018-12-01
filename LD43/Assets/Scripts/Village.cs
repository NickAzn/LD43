using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour {

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
    public Construction constuction;
    public Tavern tavern;

    List<Item> itemList = new List<Item>();

    int sacBalance;
    int curDay = 1;
    float curDayTime = 0f;


	// Use this for initialization
	void Awake () {
        food = startFood;
        sacBalance = 1;

        for (int i = 0; i < startPersonCount; i++) {
            GameObject ob = Instantiate(personPrefab);
            Person p = ob.GetComponent<Person>();
            peopleList.Add(p);
        }
	}
	
	// Update is called once per frame
	void Update () {
        curDayTime += Time.deltaTime;
        if (curDayTime >= dayLength)
            EndDay();
	}

    //Ends the day and starts new day
    void EndDay() {
        if (sacBalance <= 0) {
            curDayTime = 0;
            curDay++;
            food -= peopleList.Count * peopleFoodCost;
            food += farm.Work();
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
        Destroy(p.gameObject);
    }
}
