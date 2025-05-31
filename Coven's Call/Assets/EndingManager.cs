using TMPro;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject endingCanvas; // <-- nou
    public CanvasGroup blackScreen;
    public TextMeshProUGUI endingText;
    public float fadeDuration = 2f;

    private void Awake()
    {
        if (blackScreen != null)
        {
            blackScreen.alpha = 0f;
            blackScreen.gameObject.SetActive(false);
        }

        if (endingText != null)
            endingText.text = "";
    }

    public void TriggerEnding(string endingType)
    {
        if (endingCanvas != null)
            endingCanvas.SetActive(true); // <-- activare canvas

        string message = "";

        switch (endingType)
        {
            case "RunAway":
                message = "You left Ethan behind. No one ever saw him again.";
                break;
            case "JoinWitches":
                message = "You joined the witches. Ethan was spared... for now.";
                break;
            case "Refuse":
                message = "You refused their offer. You shared Ethan's fate.";
                break;
            default:
                message = "An unknown ending occurred.";
                break;
        }

        StartCoroutine(PlayEndingSequence(message));
    }

    private System.Collections.IEnumerator PlayEndingSequence(string message)
    {
        blackScreen.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        if (endingText != null)
        {
            endingText.text = message;
            endingText.color = new Color(endingText.color.r, endingText.color.g, endingText.color.b, 1f);
        }

        yield return new WaitForSeconds(5f);
    }
}
