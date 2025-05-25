using UnityEngine;
using System.Collections;

public class BranchCrackTrigger : MonoBehaviour
{
    public AudioSource crackAudio;
    public GameObject player;
    public GameObject thirdPersonCamera;
    public Transform cameraTransform;
    public Transform zoomTarget;
    public float moveDuration = 1.5f;

    private ManualThirdPersonCamera cameraController;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool triggered = false;

    void Start()
    {
        if (cameraTransform != null)
        {
            originalPosition = cameraTransform.position;
            originalRotation = cameraTransform.rotation;
        }

        if (thirdPersonCamera != null)
            cameraController = thirdPersonCamera.GetComponent<ManualThirdPersonCamera>();
    }

    public void TriggerBranchSequence()
    {
        if (triggered) return;
        triggered = true;

        Invoke(nameof(PlayCrackAndZoom), 0.1f); // delay dupa replica playerului
    }

    void PlayCrackAndZoom()
    {
        if (crackAudio != null)
            crackAudio.Play();

        if (cameraController != null)
            cameraController.enabled = false;

        var controller = player.GetComponent<SimplePlayerController>();
        if (controller != null)
            controller.enabled = false;

        StartCoroutine(MoveCamera(zoomTarget.position, zoomTarget.rotation));
    }

    IEnumerator MoveCamera(Vector3 targetPos, Quaternion targetRot)
    {
        float elapsedTime = 0f;
        Vector3 startPos = cameraTransform.position;
        Quaternion startRot = cameraTransform.rotation;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            cameraTransform.position = Vector3.Lerp(startPos, targetPos, t);
            cameraTransform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = targetPos;
        cameraTransform.rotation = targetRot;

    }
}
