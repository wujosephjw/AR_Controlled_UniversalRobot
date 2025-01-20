using UnityEngine;
using UnityEngine.UI;

public class SnapToPhone : MonoBehaviour
{
    public GameObject ikTarget;  // The IK target GameObject (e.g., robot's hand or foot)
    public Button snapButton;    // UI Button to trigger the snapping behavior
    public GameObject arCamera;  // Public reference to the AR Camera GameObject (assignable in the Inspector)

    private bool shouldSnap = false; // Flag to trigger snapping

    void Start()
    {
        // Ensure the AR Camera GameObject is assigned
        if (arCamera == null)
        {
            Debug.LogError("AR Camera GameObject is not assigned!");
        }

        // Set up button listener to enable snapping
        snapButton.onClick.AddListener(OnSnapButtonPressed);
    }

    void Update()
    {
        // If snapping is enabled, continuously update the IK target's position and rotation
        if (shouldSnap && arCamera != null)
        {
            UpdateIKTargetPosition();
        }
    }

    void OnSnapButtonPressed()
    {
        // Enable snapping when the button is pressed
        shouldSnap = true;
    }

    void UpdateIKTargetPosition()
    {
        // Update the IK target's position and rotation to match the AR Camera GameObject's transform
        if (ikTarget != null && arCamera != null)
        {
            ikTarget.transform.position = arCamera.transform.position;  // Update position to AR Camera's position
            ikTarget.transform.rotation = arCamera.transform.rotation;  // Update rotation to AR Camera's rotation
        }
    }
}



