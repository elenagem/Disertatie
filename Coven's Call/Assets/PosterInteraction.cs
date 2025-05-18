using UnityEngine;

public class PosterInteraction : MonoBehaviour
{
    public GameObject interactionPrompt;   // Text 3D tip "Press X"
    public KeyCode interactionKey = KeyCode.X;
    public KeyCode exitKey = KeyCode.Escape;
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactionKey))
        {
            // Doar ascundem promptul — camera se ocupa separat de zoom
            interactionPrompt.SetActive(false);
        }

        if (Input.GetKeyDown(exitKey))
        {
            // La revenire, reafisam promptul daca e nevoie
            interactionPrompt.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(true);
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(false);
            isPlayerNear = false;
        }
    }
}
