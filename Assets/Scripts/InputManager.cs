using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    public Button acceptButton;
    public Button declineButton;

    // Reference to the CardValue component on the current card
    public CardValue currentCard;

    // Array of prefabs (e.g., different cards or UI elements)
    public GameObject[] cardPrefabs;

    // List to track available prefabs that haven't been used yet
    private List<GameObject> availablePrefabs = new List<GameObject>();

    void Start()
    {
        // Initialize availablePrefabs with all prefabs from cardPrefabs array
        availablePrefabs.AddRange(cardPrefabs);

        // Listen for button clicks
        acceptButton.onClick.AddListener(OnAccept);
        declineButton.onClick.AddListener(OnDecline);

        // Setup the current card's UI
        if (currentCard != null)
        {
            currentCard.SetupCard(); // This updates the text and image of the card
        }
    }

    // Handle Accept button click
    void OnAccept()
    {
        Debug.Log("Accepted Choice");

        // Apply the resource effects for the "Accept" choice via the GameManager
        if (currentCard != null)
        {
            GameManager.instance.ApplyResourceChanges(
                currentCard.acceptHappiness,
                currentCard.acceptWealth,
                currentCard.acceptPopulation,
                currentCard.acceptEnvironment
            );
        }

        // Update the UI after applying the effects
        UpdateUI();
    }

    // Handle Decline button click
    void OnDecline()
    {
        Debug.Log("Declined Choice");

        // Apply the resource effects for the "Decline" choice via the GameManager
        if (currentCard != null)
        {
            GameManager.instance.ApplyResourceChanges(
                currentCard.declineHappiness,
                currentCard.declineWealth,
                currentCard.declinePopulation,
                currentCard.declineEnvironment
            );
        }

        // Update the UI after applying the effects
        UpdateUI();
    }

    // Update any UI elements and randomly switch between different card prefabs
    private void UpdateUI()
    {
        // Refill availablePrefabs if it’s empty
        if (availablePrefabs.Count == 0)
        {
            availablePrefabs.AddRange(cardPrefabs);
        }

        // Select a random prefab from the available prefabs list
        int randomIndex = Random.Range(0, availablePrefabs.Count);
        GameObject selectedPrefab = availablePrefabs[randomIndex];

        // Destroy the current card or UI (if needed)
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject); // This destroys all children (such as the previous card) before instantiating a new one
        }

        // Instantiate the randomly selected prefab
        GameObject selectedCard = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
        selectedCard.transform.SetParent(transform, false);  // Set it as a child of the current object for proper UI management

        // Remove the selected prefab from the availablePrefabs list
        availablePrefabs.RemoveAt(randomIndex);

        // Update the currentCard reference to the new card
        currentCard = selectedCard.GetComponent<CardValue>();

        // Call SetupCard to update the UI from the CardValue component if it exists
        if (currentCard != null)
        {
            currentCard.SetupCard(); // This updates the text and image of the card
        }
    }
}





