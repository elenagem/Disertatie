using UnityEngine;
using TMPro;
using System.Collections;

public class ForestEntrance : MonoBehaviour
{
    public GameObject player;
    public GameObject npc;
    public GameObject promptUI;
    public Transform campingSpawnPoint;
    public Transform npcCampingSpawnPoint;
    public KeyCode enterKey = KeyCode.X;

    private bool isPlayerInside = false;

    void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(enterKey))
        {
            EnterForest();
        }
    }

    void EnterForest()
    {
        // Move player
        if (player != null && campingSpawnPoint != null)
            player.transform.position = campingSpawnPoint.position;

        // Move NPC
        if (npc != null && npcCampingSpawnPoint != null)
            npc.transform.position = npcCampingSpawnPoint.position;

        // Re-enable player controller
        var controller = player.GetComponent<SimplePlayerController>();
        if (controller != null)
            controller.enabled = true;

        // Disable poster interaction
        var posterScript = FindObjectOfType<PosterCameraZoom>();
        if (posterScript != null)
            posterScript.enabled = false;

        if (promptUI != null)
            promptUI.SetActive(false);

        isPlayerInside = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerInside = true;

            if (promptUI != null)
                promptUI.SetActive(true);

            var controller = player.GetComponent<SimplePlayerController>();
            if (controller != null)
                controller.enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerInside = false;

            if (promptUI != null)
                promptUI.SetActive(false);

            var controller = player.GetComponent<SimplePlayerController>();
            if (controller != null)
                controller.enabled = true;
        }
    }
}
