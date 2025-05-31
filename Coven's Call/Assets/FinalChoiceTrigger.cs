using UnityEngine;

public class FinalChoiceTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject npc;

    public Transform joinWitchesPosition;
    public float delayBeforeFinalChoice = 1.5f;

    public void StartFinalChoice()
    {
        Invoke(nameof(TriggerFinalChoice), delayBeforeFinalChoice);
    }

    void TriggerFinalChoice()
    {
        ChoiceManager choiceManager = FindAnyObjectByType<ChoiceManager>();

        if (choiceManager != null)
        {
            choiceManager.ShowChoices(
                "You have a choice. Become one of us... or join him in death.",
                "Join the witches",
                "Refuse",
                "Let him go. I'll do it. Just spare him.",
                "No. I'm not like you.",
                OnJoinWitches,
                OnRefuse
            );
        }
    }

    void OnJoinWitches()
    {
        AutoMovePlayer mover = player.GetComponent<AutoMovePlayer>();
        if (mover != null)
        {
            mover.MoveTo(joinWitchesPosition.position, () =>
            {
                // Ethan fuge
                if (npc != null)
                    npc.SetActive(false); // sau animatie de fuga

                FindFirstObjectByType<EndingManager>()?.TriggerEnding("JoinWitches");
            });
        }
    }

    void OnRefuse()
    {
        FindFirstObjectByType<EndingManager>()?.TriggerEnding("Refuse");
    }
}
