using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class RobotPositionController : MonoBehaviour
{
    public GameObject robot;  // Reference to your robot GameObject
    private ARSessionOrigin arSessionOrigin;

    void Start()
    {
        // Find ARSessionOrigin in the scene
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {
        // Ensure the robot is positioned in front of the AR camera in world space
        // Adjust the robot's position 1 meter in front of the AR camera
        Vector3 worldPosition = arSessionOrigin.transform.position + arSessionOrigin.transform.forward * 1f;

        // Set the robot's position in world coordinates
        robot.transform.position = worldPosition;
    }
}
