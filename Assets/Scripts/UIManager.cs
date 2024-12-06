using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Button acceptButton;
    public Button declineButton;
    public AudioClip acceptButtonClickSound;
    public AudioClip declineButtonClickSound;
    public AudioSource audioSource;
    public GameObject loadingScreen;
    public float loadingDuration;


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

    // Play the accept button sound
    if (audioSource != null && acceptButtonClickSound != null)
    {
        audioSource.PlayOneShot(acceptButtonClickSound);
    }

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

    StartCoroutine(ShowLoadingAndUpdateUI());
}

void OnDecline()
{
    if (isGameOver) return;

    // Play the decline button sound
    if (audioSource != null && declineButtonClickSound != null)
    {
        audioSource.PlayOneShot(declineButtonClickSound);
    }

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

        StartCoroutine(ShowLoadingAndUpdateUI());
    }
}

    public void DisplayEndCard()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("Game Over! Displaying End Card.");

        // Destroy the current card or UI (if needed)
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the end card prefab
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

        // Check if all cards have been displayed
        if (availablePrefabs.Count == 0)
        {
            // All cards have been displayed, show the end card
            DisplayEndCard();
            return;
        }

        // Select a random prefab from the available prefabs list
        int randomIndex = Random.Range(0, availablePrefabs.Count);
        GameObject selectedPrefab = availablePrefabs[randomIndex];

        // Destroy the current card or UI (if needed)
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the randomly selected prefab
        GameObject selectedCard = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
        selectedCard.transform.SetParent(transform, false);

        // Remove the selected prefab from the availablePrefabs list
        availablePrefabs.RemoveAt(randomIndex);
        currentCard = selectedCard.GetComponent<CardValue>();

        if (currentCard != null)
        {
            currentCard.SetupCard();
        }
    }
        private IEnumerator ShowLoadingAndUpdateUI()
{
    if (loadingScreen != null)
    {
        
        loadingScreen.SetActive(true);
    }

   
    yield return new WaitForSeconds(loadingDuration);

    
    if (loadingScreen != null)
    {
        loadingScreen.SetActive(false);
    }

    
    UpdateUI();
}

     
}






