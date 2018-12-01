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
                residents[i].pregnant = true;
            }
        }
    }

    public void Upgrade() {
        maxPeople += personCapIncreasePerRank;
        rank++;
    }

    public bool AddToHouse(Person p) {
        bool success = false;
        if (residents.Count < maxPeople) {
            residents.Add(p);
            success = true;
        }
        return success;
    }

    public void RemovePerson(Person p) {
        p.homeless = true;
        residents.Remove(p);
    }

    private void OnMouseDown() {
        transform.localScale = new Vector2(1.1f, 1.1f);
    }

    private void OnMouseEnter() {
        transform.localScale = new Vector2(1.05f, 1.05f);
    }

    private void OnMouseExit() {
        transform.localScale = new Vector2(1f, 1f);
    }

    private void OnMouseUpAsButton() {
        transform.localScale = new Vector2(1.05f, 1.05f);
    }
}
