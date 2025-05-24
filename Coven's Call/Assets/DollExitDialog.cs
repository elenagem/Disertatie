using UnityEngine;

public class DollExitDialog : MonoBehaviour
{
    public void TriggerDollDialog()
    {
        // Afiseaza replica jucatorului dupa o scurta pauza
        Invoke(nameof(ShowDollComment), 0.1f);
    }

    void ShowDollComment()
    {
        SpeechBubbleManager.Instance.ShowLine(
            "This thing... it's handmade. With hair?",
            SpeechBubbleManager.Speaker.Player,
            3f
        );
    }
}
