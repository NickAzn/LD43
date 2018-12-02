using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

    public GameObject personUIPrefab;

    public TextMeshProUGUI foodText;
    public TextMeshProUGUI dayTimeText;
    public TextMeshProUGUI personCountText;
    public TextMeshProUGUI sacBalanceText;
    public ScrollRect buildingWorkerList;
    public GameObject nightUI;
    public RectTransform allPeopleListUI;
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

    public void DisplayAllPeople(List<Person> people, RectTransform content) {

        content.sizeDelta = new Vector2(people.Count * 160f, 0f);
        content.localPosition = new Vector2(people.Count * 80f, 0f);

        for (int i = 0; i < people.Count; i++) {
            GameObject ob = Instantiate(personUIPrefab);
            UIPerson personUI = ob.GetComponent<UIPerson>();
            personUI.person = people[i];
            personUI.UpdateDisplay();

            ob.transform.SetParent(content);
            RectTransform obTrans = ob.GetComponent<RectTransform>();
            obTrans.anchorMax = new Vector2(0f, .5f);
            obTrans.anchorMin = new Vector2(0f, .5f);
            obTrans.anchoredPosition = new Vector2(80 + i * 160, 0);
        }
    }

    public void ShowNightUI() {
        DisplayAllPeople(Village.instance.GetPeopleList(), allPeopleListUI);
        nightUI.SetActive(true);
    }

    public void HideNightUI() {
        nightUI.SetActive(false);
    }

    public void ShowGameOverUI() {
        endGameUI.SetActive(true);
    }
}
