using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager instance;

    // Player's resources - initialized to 50 (starting value)
    public int playerHappiness = 50;  // Set initial value to 50
    public int playerWealth = 50;     // Set initial value to 50
    public int playerPopulation = 50; // Set initial value to 50
    public int playerEnvironment = 50; // Set initial value to 50

    // Reference to the ResourceBar components
    public ResourceBar happinessBar;
    public ResourceBar wealthBar;
    public ResourceBar populationBar;
    public ResourceBar environmentBar;

    // Start is called before the first frame update
    void Awake()
    {
        // Singleton pattern (ensure only one instance exists)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy if duplicate GameManager is found
        }

        // Set the initial values for the resource bars
        happinessBar.SetValue(playerHappiness);
        wealthBar.SetValue(playerWealth);
        populationBar.SetValue(playerPopulation);
        environmentBar.SetValue(playerEnvironment);
    }

    // Method to apply resource changes based on choice
    public void ApplyResourceChanges(int happinessChange, int wealthChange, int populationChange, int environmentChange)
    {
        // Adjust player resources based on changes
        playerHappiness += happinessChange;
        playerWealth += wealthChange;
        playerPopulation += populationChange;
        playerEnvironment += environmentChange;

        // Clamp the values to ensure they stay within valid bounds (0 - 100)
        playerHappiness = Mathf.Clamp(playerHappiness, 0, 100);
        playerWealth = Mathf.Clamp(playerWealth, 0, 100);
        playerPopulation = Mathf.Clamp(playerPopulation, 0, 100);
        playerEnvironment = Mathf.Clamp(playerEnvironment, 0, 100);

        // Update the resource bars with the new values
        happinessBar.SetValue(playerHappiness);
        wealthBar.SetValue(playerWealth);
        populationBar.SetValue(playerPopulation);
        environmentBar.SetValue(playerEnvironment);
    }
}
