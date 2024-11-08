using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [Header("UI Elements")]
    public Image barImage;

    [Header("Resource Settings")]
    public int maxValue = 100; // The maximum value for the resource
    private int currentValue;  // The current value of the resource

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the current value (e.g., set to 50 or any starting value)
        currentValue = maxValue / 2;  // Start at half the max value (50)
        UpdateBar();  // Update the bar UI to reflect the initial value
    }

    // Method to set a new value for the resource and update the bar
    public void SetValue(int newValue)
    {
        currentValue = Mathf.Clamp(newValue, 0, maxValue); // Ensure the value stays within bounds
        UpdateBar();  // Update the bar when the value changes
    }

    // Method to update the bar fill based on the current value
    private void UpdateBar()
    {
        // Calculate the fill amount (current value as a fraction of the max value)
        float fillAmount = (float)currentValue / (float)maxValue;

        // Update the fill amount of the bar image
        if (barImage != null)
        {
            barImage.fillAmount = fillAmount;
        }
    }

    // Optionally, update the bar in Awake as well
    private void Awake()
    {
        UpdateBar();  // Ensure the bar is updated as soon as the script is loaded
    }
}