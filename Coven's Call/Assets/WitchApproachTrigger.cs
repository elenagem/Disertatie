using UnityEngine;

public class WitchApproachTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject npc;
    public BoxCollider invisibleWall;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            // Blocheaza trecerea mai departe
            if (invisibleWall != null)
                invisibleWall.enabled = true;

            // NPC spune replica
            SpeechBubbleManager.Instance.ShowLine(
                "That's her... the girl from the poster.",
                SpeechBubbleManager.Speaker.NPC,
                3f
            );

            // Dupa ce dispare replica lui, playerul vorbeste
            Invoke(nameof(PlayerLine), 3f);
        }
    }

    void PlayerLine()
    {
        SpeechBubbleManager.Instance.ShowLine(
            "What are they doing?",
            SpeechBubbleManager.Speaker.Player,
            3f
        );

        // Dupa replica jucatorului - trosnet creanga + zoom
        Invoke(nameof(TriggerCrack), 3f);
    }

    void TriggerCrack()
    {
        Object.FindFirstObjectByType<BranchCrackTrigger>()?.TriggerBranchSequence();
    }
}
