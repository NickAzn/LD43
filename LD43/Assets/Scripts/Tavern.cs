using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern : MonoBehaviour,  Building {

    // Game stats given in editor
    public int startWorkerCap;
    public int workerCapIncreasePerRank;

    List<Person> workers = new List<Person>();
    int maxWorkers;
    int rank;

    int harvestIdols = 0;
    int fertilityIdols = 0;
    int bloodIdols = 0;
    int constructionIdols = 0;

    public Sprite workerSprite;
    public Sprite[] tavernSprites;
    public AudioClip hoverSound;
    public AudioClip clickDownSound;
    public AudioClip clickUpSound;

    // Use this for initialization
    void Start () {
        maxWorkers = startWorkerCap;
        rank = 1;
	}
	
    public void Work() {
        int idolNumber;

        for (int i = 0; i < workers.Count; i++) {
            if (Random.Range(0,10) == 0) {
                idolNumber = (Random.Range(1,4));
                if(idolNumber == 1) {
                    harvestIdols++;
                }else if(idolNumber == 2) {
                    fertilityIdols++;
                }else if(idolNumber == 3) {
                    bloodIdols++;
                }else if(idolNumber == 4) {
                    constructionIdols++;
                }
            }
        }
    }

	public void Upgrade() {
        maxWorkers += workerCapIncreasePerRank;
        rank++;
        GetComponent<SpriteRenderer>().sprite = tavernSprites[rank - 1];
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

        SoundManager.instance.PlaySfx(clickDownSound);
        transform.localScale = new Vector2(1.1f, 1.1f);
    }
    private void OnMouseEnter() {
        if (UIManager.instance.blockingUI)
            return;

        SoundManager.instance.PlaySfx(hoverSound);
        transform.localScale = new Vector2(1.05f, 1.05f);
    }
    private void OnMouseExit() {
        if (UIManager.instance.blockingUI)
            return;

        transform.localScale = new Vector2(1f, 1f);
    }

    //Give visual feedback and perform an action when clicked
    private void OnMouseUpAsButton() {
        if (UIManager.instance.blockingUI)
            return;

        SoundManager.instance.PlaySfx(clickUpSound);
        transform.localScale = new Vector2(1.05f, 1.05f);
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
        return "Tavern";
    }
}
