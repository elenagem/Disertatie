
using UnityEngine;

public class PosterExitDialog : MonoBehaviour
{
    public ChoiceManager choiceManager;

    public void TriggerSecondDialog()
    {
        Invoke(nameof(StartSecondDialog), 0.1f);
    }

    void StartSecondDialog()
    {
        // NPC întreabă
        SpeechBubbleManager.Instance.ShowLine(
            "Should we still go in?",
            SpeechBubbleManager.Speaker.NPC,
            3f
        );

        // După 3 secunde, apar opțiunile de alegere
        Invoke(nameof(ShowChoices), 3f);
    }

    void ShowChoices()
    {
        choiceManager.ShowChoices(
            "Choose what to say to Ethan:",
            "It's probably old... Let's go.",
            "This feels wrong.",
            "It's probably old... Let's go.",
            "This feels wrong.",
            OnChooseGoIn,
            OnChooseStayOut
        );
    }

    void OnChooseGoIn()
    {
        Invoke(nameof(ShowPlayerGoInReply), 0.1f);
    }

    void OnChooseStayOut()
    {
        Invoke(nameof(ShowPlayerStayOutReply), 0.1f);
    }

    void ShowPlayerGoInReply()
    {
        SpeechBubbleManager.Instance.ShowLine(
            "It's probably old... Let's go.",
            SpeechBubbleManager.Speaker.Player,
            2f
        );
    }

    void ShowPlayerStayOutReply()
    {
        SpeechBubbleManager.Instance.ShowLine(
            "This feels wrong.",
            SpeechBubbleManager.Speaker.Player,
            2f
        );
    }
}
