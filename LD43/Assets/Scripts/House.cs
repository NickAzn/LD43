using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

    public int startPersonCap;
    public int personCapIncreasePerRank;

    List<Person> residents = new List<Person>();
    int maxPeople;
    int rank;

    public Sprite[] houseSprites;

	// Use this for initialization
	void Start () {
        maxPeople = startPersonCap;
        rank = 1;
	}

    public void Sex() {
        int pairs = residents.Count / 2;

        for (int i = 0; i < pairs; i++) {
            if (Random.Range(0,5) == 0) {
                //make person pregnant
            }
        }
    }

    public void Upgrade() {
        maxPeople += personCapIncreasePerRank;
        rank++;
    }
}
