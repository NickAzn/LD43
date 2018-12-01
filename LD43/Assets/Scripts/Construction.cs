using System.Collections;
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
}
