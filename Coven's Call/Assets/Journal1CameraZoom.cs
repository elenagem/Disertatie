using UnityEngine;
using System.Collections;

public class Journal1CameraZoom : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform cameraTarget;
    public Transform cameraOriginalParent;
    public GameObject player;
    public KeyCode interactKey = KeyCode.X;
    public KeyCode exitKey = KeyCode.Escape;
    public float moveDuration = 1.5f;
    public GameObject thirdPersonCamera;

    private bool isZoomedIn = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private ManualThirdPersonCamera cameraController;

    void Start()
    {
        originalPosition = cameraTransform.position;
        originalRotation = cameraTransform.rotation;

        if (thirdPersonCamera != null)
            cameraController = thirdPersonCamera.GetComponent<ManualThirdPersonCamera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey) && !isZoomedIn)
        {
            if (cameraController != null)
                cameraController.enabled = false;

            var controller = player.GetComponent<SimplePlayerController>();
            if (controller != null)
                controller.enabled = false;

            StartCoroutine(MoveCamera(cameraTarget.position, cameraTarget.rotation));
            isZoomedIn = true;
        }

        if (Input.GetKeyDown(exitKey) && isZoomedIn)
        {
            StartCoroutine(MoveCamera(originalPosition, originalRotation));
            isZoomedIn = false;

            var controller = player.GetComponent<SimplePlayerController>();
            if (controller != null)
                controller.enabled = true;

            if (cameraController != null)
                cameraController.enabled = true;
        }
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
