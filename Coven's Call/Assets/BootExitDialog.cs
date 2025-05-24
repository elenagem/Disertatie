using UnityEngine;

public class BootExitDialog : MonoBehaviour
{
    public void TriggerBootDialog()
    {
        // Afiseaza replica NPC-ului dupa o scurta pauza
        Invoke(nameof(ShowBootComment), 0.1f);
    }

    void ShowBootComment()
    {
        SpeechBubbleManager.Instance.ShowLine(
            "Looks like someone's been here recently",
            SpeechBubbleManager.Speaker.NPC,
            3f
        );
    }
}
