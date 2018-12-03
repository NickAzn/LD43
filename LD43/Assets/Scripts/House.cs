using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IBuilding {

    public int startPersonCap;
    public int personCapIncreasePerRank;

    List<Person> residents = new List<Person>();
    int maxPeople;
    int rank;

    public Sprite workerSprite;
    public Sprite[] houseSprites;

    public AudioClip hoverSound;
    public AudioClip clickDownSound;
    public AudioClip clickUpSound;
    public ParticleSystem upgradeParticle;

    // Use this for initialization
    void Awake () {
        maxPeople = startPersonCap;
        rank = 1;
	}

    public void Sex() {
        int totalFertil = 0;
        foreach (Person p in residents) {
            totalFertil += p.GetFertilSkill();
        }

        int maxNew = residents.Count / 2;
        int chance = 5 - rank;
        if (chance < 2)
            chance = 2;
        for (int i = 0; i < totalFertil; i++) {
            if (Random.Range(0,5) == 0 && maxNew > 0) {
                Village.instance.NewVillager();
                maxNew--;
            }
        }
    }

    public void Upgrade() {
        GetComponent<Animator>().Play("Upgrade");
        upgradeParticle.Emit(30);
        maxPeople += personCapIncreasePerRank;
        rank++;
        if (rank <= 3)
            GetComponent<SpriteRenderer>().sprite = houseSprites[rank - 1];
    }

    public bool HireWorker(Person p) {
        bool success = false;
        if (residents.Count < maxPeople) {
            residents.Add(p);
            success = true;
        }
        return success;
    }

    public void RemoveWorker(Person p) {
        p.homeless = true;
        residents.Remove(p);
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
        return residents;
    }

    public int GetMaxWorkers() {
        return maxPeople;
    }

    public string GetName() {
        return "Housing";
    }

    public int GetBuildingPower() {
        int power = 0;
        foreach (Person p in residents)
            power += p.GetFertilSkill();
        return power;
    }
}
