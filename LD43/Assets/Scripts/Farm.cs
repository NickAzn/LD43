using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour, Building {

    // Game stats given in editor
    public int startWorkerCap;
    public int workerCapIncreasePerRank;

    List<Person> workers = new List<Person>();
    int maxWorkers;
    int rank;

    public Sprite workerSprite;
    public Sprite[] farmSprites;

    public AudioClip hoverSound;
    public AudioClip clickDownSound;
    public AudioClip clickUpSound;

    private void Start() {
        maxWorkers = startWorkerCap;
        rank = 1;
    }
    
    // returns food yield based on number of workers
    public float Work() {
        float yield = 0;

        for (int i = 0; i < workers.Count; i++) {
            yield += workers[i].GetFarmSkill();
        }

        return yield;
    }

    // upgrades farm
    public void Upgrade() {
        rank++;
        maxWorkers += workerCapIncreasePerRank;
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
        UIManager.instance.ShowBuilidingUI(this);
    }

    public Sprite GetWorkerSprite() {
        return workerSprite;
    }

    public List<Person> GetWorkerList() {
        return workers;
    }

    public int GetMaxWorkers() {
        return maxWorkers;
    }

    public string GetName() {
        return "Farm";
    }
}
