using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardValue : MonoBehaviour
{
    // Scenario details
    public string scenarioText;
    public Sprite scenarioImage;

    // Resource effects for "Accept" choice
    public int acceptHappiness;
    public int acceptWealth;
    public int acceptPopulation;
    public int acceptEnvironment;

    // Resource effects for "Decline" choice
    public int declineHappiness;
    public int declineWealth;
    public int declinePopulation;
    public int declineEnvironment;

    // References to UI elements
    public TextMeshProUGUI scenarioTextUI;
    public Image scenarioImageUI;

    // Setup method to apply the data to UI
    public void SetupCard()
    {
        scenarioTextUI.text = scenarioText;
        scenarioImageUI.sprite = scenarioImage;
    }

}
