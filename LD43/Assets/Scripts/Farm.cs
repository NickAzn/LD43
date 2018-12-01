using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

    // Game stats given in editor
    public int startWorkerCap;
    public int workerCapIncreasePerRank;

    List<Person> workers = new List<Person>();
    int maxWorkers;
    int rank;

    public Sprite[] farmSprites;

    private void Start() {
        maxWorkers = startWorkerCap;
        rank = 1;
    }
    
    // returns food yield based on number of workers
    public float Work() {
        float yield = 0;

        for (int i = 0; i < workers.Count; i++) {
            yield += workers[i].getFarmSkill();
        }

        return yield;
    }

    // upgrades farm
    public void Upgrade() {
        rank++;
        maxWorkers += workerCapIncreasePerRank;
    }
}
