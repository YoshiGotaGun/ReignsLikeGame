using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Button acceptButton;
    public Button declineButton;

    // Reference to the CardValue component on the current card
    public CardValue currentCard;

    // Array of prefabs (e.g., different cards or UI elements)
    public GameObject[] cardPrefabs;

    // List to track available prefabs that haven't been used yet
    private List<GameObject> availablePrefabs = new List<GameObject>();

    // Special end card to display
    public GameObject endCardPrefab;

    // Flag to check if the game is over
    private bool isGameOver = false;

    void Start()
    {
        availablePrefabs.AddRange(cardPrefabs);

        acceptButton.onClick.AddListener(OnAccept);
        declineButton.onClick.AddListener(OnDecline);

        if (currentCard != null)
        {
            currentCard.SetupCard();
        }
    }

    void OnAccept()
    {
        if (isGameOver) return;

        Debug.Log("Accepted Choice");
        if (currentCard != null)
        {
            GameManager.instance.ApplyResourceChanges(
                currentCard.acceptHappiness,
                currentCard.acceptWealth,
                currentCard.acceptPopulation,
                currentCard.acceptEnvironment
            );
        }

        UpdateUI();
    }

    void OnDecline()
    {
        if (isGameOver) return;

        Debug.Log("Declined Choice");

        if (currentCard != null && currentCard.gameObject.name == "MayorStart")
        {
            DisplayEndCard();
        }
        else
        {
            if (currentCard != null)
            {
                GameManager.instance.ApplyResourceChanges(
                    currentCard.declineHappiness,
                    currentCard.declineWealth,
                    currentCard.declinePopulation,
                    currentCard.declineEnvironment
                );
            }

            UpdateUI();
        }
    }

    public void DisplayEndCard()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("Game Over! Displaying End Card.");

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject endCard = Instantiate(endCardPrefab, transform.position, Quaternion.identity);
        endCard.transform.SetParent(transform, false);
        currentCard = endCard.GetComponent<CardValue>();

        if (currentCard != null)
        {
            currentCard.SetupCard();
        }
    }

    private void UpdateUI()
    {
        if (isGameOver) return;

        if (availablePrefabs.Count == 0)
        {
            availablePrefabs.AddRange(cardPrefabs);
        }

        int randomIndex = Random.Range(0, availablePrefabs.Count);
        GameObject selectedPrefab = availablePrefabs[randomIndex];

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject selectedCard = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
        selectedCard.transform.SetParent(transform, false);

        availablePrefabs.RemoveAt(randomIndex);
        currentCard = selectedCard.GetComponent<CardValue>();

        if (currentCard != null)
        {
            currentCard.SetupCard();
        }
    }
}





