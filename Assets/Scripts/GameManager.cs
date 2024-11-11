using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerHappiness = 50;
    public int playerWealth = 50;
    public int playerPopulation = 50;
    public int playerEnvironment = 50;

    public ResourceBar happinessBar;
    public ResourceBar wealthBar;
    public ResourceBar populationBar;
    public ResourceBar environmentBar;

    public UIManager uiManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        happinessBar.SetValue(playerHappiness);
        wealthBar.SetValue(playerWealth);
        populationBar.SetValue(playerPopulation);
        environmentBar.SetValue(playerEnvironment);
    }

    public void ApplyResourceChanges(int happinessChange, int wealthChange, int populationChange, int environmentChange)
    {
        playerHappiness += happinessChange;
        playerWealth += wealthChange;
        playerPopulation += populationChange;
        playerEnvironment += environmentChange;

        playerHappiness = Mathf.Clamp(playerHappiness, 0, 100);
        playerWealth = Mathf.Clamp(playerWealth, 0, 100);
        playerPopulation = Mathf.Clamp(playerPopulation, 0, 100);
        playerEnvironment = Mathf.Clamp(playerEnvironment, 0, 100);

        happinessBar.SetValue(playerHappiness);
        wealthBar.SetValue(playerWealth);
        populationBar.SetValue(playerPopulation);
        environmentBar.SetValue(playerEnvironment);

        if (playerHappiness <= 0 || playerWealth <= 0 || playerPopulation <= 0 || playerEnvironment <= 0)
        {
            if (uiManager != null)
            {
                Debug.Log("Resource reached zero. Ending game...");
                uiManager.DisplayEndCard();
            }
        }
    }
}


