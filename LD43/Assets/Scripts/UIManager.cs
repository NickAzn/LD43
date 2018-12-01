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
}
