using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RobotPositionController : MonoBehaviour
{
    public GameObject robot;  // Reference to your robot GameObject
    private ARSessionOrigin arSessionOrigin;

    // Public XYZ offset parameters to modify in the Inspector
    public float xOffset = 0f;  // X axis offset
    public float yOffset = 0f;  // Y axis offset
    public float zOffset = 1f;  // Z axis offset

    void Start()
    {
        // Find ARSessionOrigin in the scene
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();

        if (arSessionOrigin == null)
        {
            Debug.LogError("ARSessionOrigin not found in the scene!");
        }
    }

    void Update()
    {
        // Ensure the robot is positioned relative to the AR camera in world space
        // Add the offset to the AR camera's position and rotation for the final position
        Vector3 worldPosition = arSessionOrigin.transform.position + arSessionOrigin.transform.forward * zOffset;
        worldPosition += new Vector3(xOffset, yOffset, 0f); // Apply the custom X, Y offset

        // Set the robot's position with the offset applied
        robot.transform.position = worldPosition;
        //robot.transform.rotation = arSessionOrigin.transform.rotation;  // Optional: keep robot's rotation aligned with AR Camera
    }
}

