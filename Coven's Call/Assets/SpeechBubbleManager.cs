using System.Collections;
using UnityEngine;
using TMPro;

public class SpeechBubbleManager : MonoBehaviour
{
    public static SpeechBubbleManager Instance;

    public GameObject playerBubble;
    public GameObject npcBubble;

    public TextMeshProUGUI playerText;
    public TextMeshProUGUI npcText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        HideAll(); // ascunde baloanele la start
    }

    public enum Speaker { Player, NPC }

    public void ShowLine(string text, Speaker speaker, float duration)
    {
        if (speaker == Speaker.Player)
        {
            playerText.text = text;
            playerBubble.SetActive(true);
            StartCoroutine(HideBubbleAfterSeconds(playerBubble, duration));
        }
        else
        {
            npcText.text = text;
            npcBubble.SetActive(true);
            StartCoroutine(HideBubbleAfterSeconds(npcBubble, duration));
        }
    }

    private IEnumerator HideBubbleAfterSeconds(GameObject bubble, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bubble.SetActive(false);
    }

    public void HideAll()
    {
        playerBubble.SetActive(false);
        npcBubble.SetActive(false);
    }
}
