using UnityEngine;
using UnityEngine.UI;

public class PosterInteraction : MonoBehaviour
{
    public GameObject interactionPrompt;
    public GameObject posterUI;
    public KeyCode interactionKey = KeyCode.X;
    public KeyCode exitKey = KeyCode.Escape;
    private bool isPlayerNear = false;

    public PosterExitDialog exitDialog; // Adaugă referința aici

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactionKey))
        {
            posterUI.SetActive(true);
            interactionPrompt.SetActive(false);
            Time.timeScale = 0f; // pauzează jocul
        }

        if (posterUI.activeSelf && Input.GetKeyDown(exitKey))
        {
            posterUI.SetActive(false);
            Time.timeScale = 1f; // reia jocul

            // Începem dialogul de după poster
            if (exitDialog != null)
            {
                exitDialog.TriggerSecondDialog();
            }
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
