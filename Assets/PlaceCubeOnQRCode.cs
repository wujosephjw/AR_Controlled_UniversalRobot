using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceCubeOnQRCode : MonoBehaviour
{
    public GameObject cubePrefab; // Cube prefab to place
    private ARTrackedImageManager trackedImageManager;
    private GameObject instantiatedCube; // Single instance of the cube

    void Awake()
    {
        // Get ARTrackedImageManager from AR Session Origin
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        // Subscribe to the tracked image events
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        // Unsubscribe from the tracked image events
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // For each newly detected or updated image
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateCube(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateCube(trackedImage);
        }
    }

    private void UpdateCube(ARTrackedImage trackedImage)
    {
        // Ensure the tracked image is being tracked
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            // If the cube has not been instantiated, instantiate it
            if (instantiatedCube == null)
            {
                instantiatedCube = Instantiate(cubePrefab);
            }

            // Update the cube's position and rotation to match the tracked image
            instantiatedCube.transform.position = trackedImage.transform.position;
            instantiatedCube.transform.rotation = trackedImage.transform.rotation;

            // Ensure the cube is active
            instantiatedCube.SetActive(true);
        }
        else
        {
            // Hide the cube if the image is not being tracked
            if (instantiatedCube != null)
            {
                instantiatedCube.SetActive(false);
            }
        }
    }
}



