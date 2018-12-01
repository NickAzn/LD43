﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour {

    // Game stats given in editor
    public int startWorkerCap;
    public int workerCapIncreasePerRank;

    List<Person> workers = new List<Person>();
    int maxWorkers;
    int rank;

    public Sprite[] tarvernSprites;

    // Use this for initialization
    void Start() {
        maxWorkers = startWorkerCap;
        rank = 1;
    }

    public void Build() {
        //rank up building
    }

    public void Upgrade() {
        maxWorkers += workerCapIncreasePerRank;
        rank++;
    }

    public bool HireWorker(Person p) {
        bool success = false;
        if (workers.Count < maxWorkers) {
            workers.Add(p);
            success = true;
        }
        return success;
    }

    public void RemoveWorker(Person p) {
        p.job = 0;
        workers.Remove(p);
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
        Village.instance.AssignBuilderJob();
    }
}
