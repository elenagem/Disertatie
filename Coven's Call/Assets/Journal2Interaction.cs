using UnityEngine;

public class Journal2Interaction : MonoBehaviour
{
    public GameObject interactionPrompt;
    public KeyCode interactionKey = KeyCode.X;
    public KeyCode exitKey = KeyCode.Escape;
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactionKey))
        {
            interactionPrompt.SetActive(false);
        }

        if (Input.GetKeyDown(exitKey))
        {
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
