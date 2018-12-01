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

    public Sprite[] armSprites;

    private void Start() {
        maxWorkers = startWorkerCap;
    }
    
    // returns food yield based on number of workers
    public float Work() {
        float yield = 0;

        yield = Random.Range(1, workers.Count);

        return yield;
    }

    // upgrades farm
    public void Upgrade() {
        rank++;
        maxWorkers += workerCapIncreasePerRank;
    }
}
