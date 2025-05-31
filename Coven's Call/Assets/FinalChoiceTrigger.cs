using UnityEngine;

public class FinalChoiceTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject npc;
    public Transform runAwayTarget;
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
        // NPC-ul fuge spre runAwayTarget
        if (npc != null && runAwayTarget != null)
        {
            AutoMovePlayer npcMover = npc.GetComponent<AutoMovePlayer>();
            if (npcMover != null)
            {
                npcMover.MoveTo(runAwayTarget.position, () =>
                {
                    // Delay scurt ca sa vedem balonul de text
                    Invoke(nameof(TriggerJoinWitchesEnding), 2f);
                });
            }
            else
            {
                npc.SetActive(false);
                Invoke(nameof(TriggerJoinWitchesEnding), 2f);
            }
        }
    }

    void TriggerJoinWitchesEnding()
    {
        FindFirstObjectByType<EndingManager>()?.TriggerEnding("JoinWitches");
    }

    void OnRefuse()
    {
        FindFirstObjectByType<EndingManager>()?.TriggerEnding("Refuse");
    }
}
