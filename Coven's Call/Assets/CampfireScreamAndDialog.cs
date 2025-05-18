using UnityEngine;

public class CampfireScreamAndDialog : MonoBehaviour
{
    public GameObject player;
    public AudioSource screamAudio;
    public ChoiceManager choiceManager;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.gameObject == player)
        {
            triggered = true;
            Invoke(nameof(PlayScream), 3f);
        }
    }

    void PlayScream()
    {
        if (screamAudio != null)
            screamAudio.Play();

        Invoke(nameof(StartDialog), screamAudio.clip.length); // dupa ce se termina tipatul
    }

    void StartDialog()
    {
        // Ethan spune replica
        SpeechBubbleManager.Instance.ShowLine(
            "Did you hear that?",
            SpeechBubbleManager.Speaker.NPC,
            3f
        );

        Invoke(nameof(ShowChoices), 3f); // dupa ce termina de vorbit Ethan
    }

    void ShowChoices()
    {
        choiceManager.ShowChoices(
            "Choose what to say to Ethan:",
            "We should check it out.",
            "Stay here. I’ll go alone.",
            "We should check it out.",
            "Stay here. I’ll go alone.",
            OnChooseTogether,
            OnChooseAlone
        );
    }

    void OnChooseTogether()
    {
        Invoke(nameof(PlayerSaysTogether), 0.1f);
    }

    void OnChooseAlone()
    {
        Invoke(nameof(PlayerSaysAlone), 0.1f);
    }

    void PlayerSaysTogether()
    {
        SpeechBubbleManager.Instance.ShowLine(
            "We should check it out.",
            SpeechBubbleManager.Speaker.Player,
            2f
        );
    }

    void PlayerSaysAlone()
    {
        SpeechBubbleManager.Instance.ShowLine(
            "Stay here. I’ll go alone.",
            SpeechBubbleManager.Speaker.Player,
            2f
        );
    }
}
