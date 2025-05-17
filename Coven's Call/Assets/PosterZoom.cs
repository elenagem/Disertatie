
using UnityEngine;
using Cinemachine;

public class PosterZoom : MonoBehaviour
{
    public CinemachineVirtualCamera zoomCamera;
    public GameObject triggerObject;
    public KeyCode interactionKey = KeyCode.X;
    public KeyCode exitKey = KeyCode.Escape;

    private bool isZoomed = false;
    private bool isPlayerNear = false;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactionKey) && !isZoomed)
        {
            zoomCamera.Priority = 20; // Zoom in
            isZoomed = true;
        }

        if (isZoomed && Input.GetKeyDown(exitKey))
        {
            zoomCamera.Priority = 0; // Zoom out
            isZoomed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
