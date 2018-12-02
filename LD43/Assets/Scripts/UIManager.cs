using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

    public TextMeshProUGUI foodText;
    public TextMeshProUGUI dayTimeText;
    public TextMeshProUGUI personCountText;
    public TextMeshProUGUI sacBalanceText;
    public ScrollRect buildingWorkerList;
    public GameObject nightUI;
    public GameObject endGameUI;

    private void Awake() {
        //Singleton
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void UpdateFoodText(float amt) {
        foodText.text = "Food: " + amt.ToString();
    }

    public void UpdateDayTimeText(int remTime) {
        dayTimeText.text = "Remaining Daytime: " + remTime.ToString();
    }

    public void UpdatePersonCountText(int amt) {
        personCountText.text = "Population: " + amt.ToString();
    }

    public void UpdateSacBalanceText(int amt) {
        sacBalanceText.text = "Required Sacrifices: " + amt.ToString();
    }

    public void DisplayAllWorkers(List<Person> workers)
    {
        for (int i = 0; i < workers.Count; i++)
        {

        }
    }

    public void ShowNightUI() {
        nightUI.SetActive(true);
    }

    public void HideNightUI() {
        nightUI.SetActive(false);
    }

    public void ShowGameOverUI() {
        endGameUI.SetActive(true);
    }
}
