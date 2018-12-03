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
    public GameObject nightUI;
    public RectTransform allPeopleListUI;
    public GameObject endGameUI;

    public GameObject buildingUI;
    public RectTransform buildingWorkerList;
    public Image buildingWorkerImage;
    public Button buildingHireButton;
    public Button buildingFireButton;
    public TextMeshProUGUI buildingNameText;
    public TextMeshProUGUI buildingCountText;

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

    //Displays list of people into content rect transform sets content to proper size for number of people
    public void DisplayAllPeople(List<Person> people, RectTransform content) {

        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }

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
            obTrans.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ShowAllPeopleUI() {
        DisplayAllPeople(Village.instance.GetPeopleList(), allPeopleListUI);
        allPeopleListUI.transform.parent.gameObject.SetActive(true);
    }

    public void HideAllPeopleUI() {
        allPeopleListUI.transform.parent.gameObject.SetActive(false);
    }

    public void ShowNightUI() {
        ShowAllPeopleUI();
        nightUI.SetActive(true);
    }

    public void HideNightUI() {
        HideAllPeopleUI();
        nightUI.SetActive(false);
    }

    public void ShowGameOverUI() {
        endGameUI.SetActive(true);
    }

    public void ShowTavernUI(Tavern t) {
        ShowBuilidingUI(t);
    }

    public void ShowBuilidingUI(Building b) {
        DisplayAllPeople(b.GetWorkerList(), buildingWorkerList);
        buildingCountText.text = b.GetWorkerList().Count.ToString() + "/" + b.GetMaxWorkers().ToString();
        string bName = b.GetName();
        buildingNameText.text = bName;
        if (bName.Equals("Tavern")) {
            buildingHireButton.onClick.AddListener(() => Village.instance.AssignTreasureJob());
        } else if (bName.Equals("Farm")) {
            buildingHireButton.onClick.AddListener(() => Village.instance.AssignFarmJob());
        } else if (bName.Equals("Construction")) {
            buildingHireButton.onClick.AddListener(() => Village.instance.AssignBuilderJob());
        }
        buildingWorkerImage.sprite = b.GetWorkerSprite();
        buildingUI.SetActive(true);
    }

    public void HideBuildingUI() {
        buildingHireButton.onClick.RemoveAllListeners();
        buildingFireButton.onClick.RemoveAllListeners();
        buildingUI.SetActive(false);
    }

}
