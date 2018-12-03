using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour, IBuilding {

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
    public ParticleSystem upgradeParticle;

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
        GetComponent<Animator>().Play("Upgrade");
        upgradeParticle.Emit(30);
        rank++;
        maxWorkers += workerCapIncreasePerRank;
        if (rank <= 3)
            GetComponent<SpriteRenderer>().sprite = farmSprites[rank - 1];
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
        if (UIManager.instance.blockingUI)
            return;

        GetComponent<Animator>().Play("Click");
        SoundManager.instance.PlaySfx(clickDownSound);
    }
    private void OnMouseEnter() {
        if (UIManager.instance.blockingUI)
            return;

        GetComponent<Animator>().Play("Hover");
        SoundManager.instance.PlaySfx(hoverSound);
    }
    private void OnMouseExit() {
        if (UIManager.instance.blockingUI)
            return;
        GetComponent<Animator>().Play("Idle");
    }

    //Give visual feedback and perform an action when clicked
    private void OnMouseUpAsButton() {
        if (UIManager.instance.blockingUI)
            return;

        GetComponent<Animator>().Play("Hover");
        SoundManager.instance.PlaySfx(clickUpSound);
        UIManager.instance.ToggleBuildingUI(this);
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

    public int GetBuildingPower() {
        int power = 0;
        foreach (Person p in workers)
            power += p.GetFarmSkill();
        return power;
    }
}
