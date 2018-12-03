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
    public Animator sacBackgroundAnim;
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
    public TextMeshProUGUI workerPowerText;

    public GameObject sacrificeBoiPrefab;

    public bool tavOpen = false;
    public bool houseOpen = false;
    public bool farmOpen = false;
    public bool constOpen = false;

    public bool blockingUI = false;

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
        sacBackgroundAnim.Play("FadeIn");
        SoundManager.instance.PlayNightMusic();
        ShowAllPeopleUI();
        HideBuildingUI();
        blockingUI = true;
        nightUI.SetActive(true);
    }

    public void HideNightUI() {
        sacBackgroundAnim.Play("FadeOut");
        SoundManager.instance.PlayMusic();
        blockingUI = false;
        nightUI.SetActive(false);
    }

    public void ShowSacAnimation(Sprite sacUnit) {
        GameObject sacBoi = Instantiate(sacrificeBoiPrefab);
        sacBoi.GetComponent<SpriteRenderer>().sprite = sacUnit;
    }

    public void ShowGameOverUI() {
        blockingUI = true;
        endGameUI.SetActive(true);
    }

    public void ShowBuilidingUI(Building b) {
        HideBuildingUI();
        DisplayAllPeople(b.GetWorkerList(), buildingWorkerList);
        buildingCountText.text = b.GetWorkerList().Count.ToString() + "/" + b.GetMaxWorkers().ToString();
        string bName = b.GetName();
        buildingNameText.text = bName;
        buildingHireButton.onClick.RemoveAllListeners();
        buildingHireButton.interactable = true;
        buildingFireButton.interactable = true;
        if (b.GetWorkerList().Count >= b.GetMaxWorkers())
            buildingHireButton.interactable = false;
        if (bName.Equals("Tavern")) {
            buildingHireButton.onClick.AddListener(() => Village.instance.AssignTreasureJob());
            tavOpen = true;
        } else if (bName.Equals("Farm")) {
            buildingHireButton.onClick.AddListener(() => Village.instance.AssignFarmJob());
            farmOpen = true;
        } else if (bName.Equals("Construction")) {
            buildingHireButton.onClick.AddListener(() => Village.instance.AssignBuilderJob());
            constOpen = true;
        } else if (bName.Equals("Housing")) {
            buildingHireButton.interactable = false;
            buildingFireButton.interactable = false;
            houseOpen = true;
        }
        workerPowerText.text = b.GetBuildingPower().ToString();
        buildingWorkerImage.sprite = b.GetWorkerSprite();
        buildingUI.SetActive(true);
    }

    public void HideBuildingUI() {
        tavOpen = false;
        farmOpen = false;
        constOpen = false;
        houseOpen = false;
        buildingHireButton.onClick.RemoveAllListeners();
        buildingFireButton.onClick.RemoveAllListeners();
        buildingUI.SetActive(false);
    }

    public void ToggleBuildingUI(Building b) {
        if (b.GetName().Equals("Housing") && houseOpen) {
            HideBuildingUI();
        } else if (b.GetName().Equals("Farm") && farmOpen) {
            HideBuildingUI();
        } else if (b.GetName().Equals("Construction") && constOpen) {
            HideBuildingUI();
        } else if (b.GetName().Equals("Tavern") && tavOpen) {
            HideBuildingUI();
        } else {
            ShowBuilidingUI(b);
        }
    }

}
