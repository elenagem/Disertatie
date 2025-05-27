using UnityEngine;
using System.Collections;

public class BranchCrackTrigger : MonoBehaviour
{
    public AudioSource crackAudio;
    public AudioSource chantAudio;
    public GameObject player;
    public GameObject thirdPersonCamera;
    public Transform cameraTransform;
    public float moveDuration = 1.5f;
    public GameObject branchPrefab;

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

        Invoke("PlayCrackAndZoom", 0.1f);
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

        Vector3 playerFeet = player.transform.position + player.transform.forward * 0.4f + Vector3.down * 0.9f;
        Quaternion camRotation = Quaternion.Euler(90f, player.transform.eulerAngles.y, 0f);

        GameObject cameraPoint = new GameObject("DynamicZoomTarget");
        cameraPoint.transform.position = playerFeet + new Vector3(0f, 0.5f, 0f);
        cameraPoint.transform.rotation = camRotation;

        if (branchPrefab != null)
            Instantiate(branchPrefab, playerFeet, Quaternion.identity);

        StartCoroutine(MoveCamera(cameraPoint.transform.position, cameraPoint.transform.rotation));

        // Dupa ce a ajuns camera pe creanga, asteapta 2 secunde si revine
        Invoke("ExitBranchZoom", 2.5f);
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

    void ExitBranchZoom()
    {
        if (chantAudio != null)
            chantAudio.Stop();

        StartCoroutine(ResetCameraAndControl());
    }

    IEnumerator ResetCameraAndControl()
    {
        float elapsedTime = 0f;
        Vector3 startPos = cameraTransform.position;
        Quaternion startRot = cameraTransform.rotation;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            cameraTransform.position = Vector3.Lerp(startPos, originalPosition, t);
            cameraTransform.rotation = Quaternion.Slerp(startRot, originalRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = originalPosition;
        cameraTransform.rotation = originalRotation;

        if (cameraController != null)
            cameraController.enabled = true;

        var controller = player.GetComponent<SimplePlayerController>();
        if (controller != null)
            controller.enabled = true;

        // Activeaza comportamentul vrajitoarelor
        var witchBehavior = Object.FindFirstObjectByType<WitchBehavior>();
        if (witchBehavior != null)
        {
            witchBehavior.TriggerWitchTurn();
        }
    }
}
