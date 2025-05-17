using UnityEngine;

public class InitialDialogTrigger : MonoBehaviour
{
    public ChoiceManager choiceManager;

    void Start()
    {
        Invoke("ShowInitialDialog", 5f); // 5 secunde după startul scenei
    }

    void ShowInitialDialog()
    {
        choiceManager.ShowChoices(
            "Choose what to say to Ethan:",
            "I've been waiting for this trip forever.",
            "Let’s hope we don’t get eaten by bears.",
            "I've been waiting for this trip forever.",        // bubble pentru Player (stânga)
            "Let’s hope we don’t get eaten by bears.",         // bubble pentru Player (dreapta)
            OnFirstOption,
            OnSecondOption
        );
    }

    void OnFirstOption()
    {
        // Replica Player deja apare automat în ChoiceManager
        // Aici doar replica NPC-ului
        Invoke(nameof(ShowNpcReply), 2f); // după ce dispare balonul jucătorului
    }

    void OnSecondOption()
    {
        Invoke(nameof(ShowNpcReply), 2f);
    }

    void ShowNpcReply()
    {
        SpeechBubbleManager.Instance.ShowLine(
            "Come on, it'll be great. Just us and the nature. Finally, some peace.",
            SpeechBubbleManager.Speaker.NPC,
            5f
        );
    }
}
