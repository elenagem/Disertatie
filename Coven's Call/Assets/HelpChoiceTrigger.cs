using UnityEngine;

public class HelpChoiceTrigger : MonoBehaviour
{
    public Transform runAwayTarget;
    public Transform helpEthanTarget;
    public GameObject player;

    public float delayBeforeChoice = 2f;

    public void BeginChoice()
    {
        Invoke(nameof(TriggerHelpChoice), delayBeforeChoice);
    }

    void TriggerHelpChoice()
    {
        ChoiceManager choiceManager = FindAnyObjectByType<ChoiceManager>();

        if (choiceManager != null)
        {
            choiceManager.ShowChoices(
                "Choose what to do:",
                "Help Ethan",
                "Run Away",
                "I'm coming!",
                "Sorry Ethan...",
                OnHelpEthanChosen,
                OnRunAwayChosen
            );
        }
    }

    void OnHelpEthanChosen()
    {
        AutoMovePlayer mover = player.GetComponent<AutoMovePlayer>();
        if (mover != null)
        {
            mover.MoveTo(helpEthanTarget.position, () =>
            {
                FindFirstObjectByType<FinalChoiceTrigger>()?.StartFinalChoice();
            });
        }
    }

    void OnRunAwayChosen()
    {
        AutoMovePlayer mover = player.GetComponent<AutoMovePlayer>();
        if (mover != null)
        {
            mover.MoveTo(runAwayTarget.position, () =>
            {
                Debug.Log("TriggerEnding RunAway apelat!");
                FindFirstObjectByType<EndingManager>()?.TriggerEnding("RunAway");
            });
        }
    }
}
