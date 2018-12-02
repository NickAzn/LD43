﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

    public int startPersonCap;
    public int personCapIncreasePerRank;

    List<Person> residents = new List<Person>();
    int maxPeople;
    int rank;

    public Sprite[] houseSprites;

    public AudioClip hoverSound;
    public AudioClip clickDownSound;
    public AudioClip clickUpSound;

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

    // Visual feedback to show this object is clickable
    private void OnMouseDown() {
        SoundManager.instance.PlaySfx(clickDownSound);
        transform.localScale = new Vector2(1.1f, 1.1f);
    }
    private void OnMouseEnter() {
        SoundManager.instance.PlaySfx(hoverSound);
        transform.localScale = new Vector2(1.05f, 1.05f);
    }
    private void OnMouseExit() {
        transform.localScale = new Vector2(1f, 1f);
    }

    //Give visual feedback and perform an action when clicked
    private void OnMouseUpAsButton() {
        SoundManager.instance.PlaySfx(clickUpSound);
        transform.localScale = new Vector2(1.05f, 1.05f);
        //TODO: action on click
    }
}
