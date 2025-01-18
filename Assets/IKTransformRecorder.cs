using System.Collections.Generic;
using UnityEngine;

public class IKTransformRecorder : MonoBehaviour
{
    [System.Serializable]
    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }


    public Transform targetTransform; // The transform to record

    private List<TransformData> recordedTransforms = new List<TransformData>();
    private bool isRecording = false;
    private bool isReplaying = false;
    private int replayIndex = 0;

    void Update()
    {
        if (isRecording && targetTransform != null)
        {
            // Record the current position and rotation
            recordedTransforms.Add(new TransformData(targetTransform.position, targetTransform.rotation));
        }

        if (isReplaying && recordedTransforms.Count > 0)
        {
            ReplayTransform();
        }

        // Debugging shortcuts (optional):
        if (Input.GetKeyDown(KeyCode.R)) StartRecording();
        if (Input.GetKeyDown(KeyCode.S)) StopRecording();
        if (Input.GetKeyDown(KeyCode.P)) StartReplay();
    }

    public void StartRecording()
    {
        if (isReplaying)
        {
            Debug.LogWarning("Cannot record while replaying.");
            return;
        }

        isRecording = true;
        recordedTransforms.Clear(); // Clear any previously recorded data
        Debug.Log("Recording started.");
    }

    public void StopRecording()
    {
        isRecording = false;
        Debug.Log($"Recording stopped. Recorded {recordedTransforms.Count} frames.");
    }

    public void StartReplay()
    {
        if (isRecording)
        {
            Debug.LogWarning("Cannot replay while recording.");
            return;
        }

        if (recordedTransforms.Count == 0)
        {
            Debug.LogWarning("No transforms to replay.");
            return;
        }

        isReplaying = true;
        replayIndex = 0;
        Debug.Log("Replay started.");
    }

    private void ReplayTransform()
    {
        if (replayIndex < recordedTransforms.Count && targetTransform != null)
        {
            // Apply the recorded position and rotation to the target
            TransformData data = recordedTransforms[replayIndex];
            targetTransform.position = data.position;
            targetTransform.rotation = data.rotation;

            replayIndex++;
        }
        else
        {
            // Stop replaying when all frames are replayed
            isReplaying = false;
            Debug.Log("Replay finished.");
        }
    }
}
