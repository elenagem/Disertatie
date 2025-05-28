using UnityEngine;
using UnityEngine.AI;

public class SceneRelocator : MonoBehaviour
{
    public Transform player;
    public Transform npc;

    public Transform playerSpawnPoint;
    public Transform npcSpawnPoint;

    public float delayBeforeTeleport = 4f;

    public void StartRelocationSequence()
    {
        Invoke(nameof(RelocateScene), delayBeforeTeleport);
    }

    void RelocateScene()
    {
        // Dezactivare control player
        var playerController = player.GetComponent<SimplePlayerController>();
        if (playerController != null)
            playerController.enabled = false;

        var cameraController = player.GetComponentInChildren<ManualThirdPersonCamera>();
        if (cameraController != null)
            cameraController.enabled = false;

        // Dezactivare follow NPC
        var npcFollower = npc.GetComponent<NPCFollower>();
        if (npcFollower != null)
            npcFollower.enabled = false;

        // Oprire NavMeshAgent NPC
        var npcAgent = npc.GetComponent<NavMeshAgent>();
        if (npcAgent != null)
        {
            npcAgent.isStopped = true;
            npcAgent.ResetPath();
        }

        // Teleportare player
        if (player != null && playerSpawnPoint != null)
        {
            player.position = playerSpawnPoint.position;
            player.rotation = playerSpawnPoint.rotation;
        }

        // Teleportare NPC
        if (npc != null && npcSpawnPoint != null)
        {
            npc.position = npcSpawnPoint.position;
            npc.rotation = npcSpawnPoint.rotation;
        }

        // Intoarce playerul spre NPC
        Vector3 lookDir = npc.position - player.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
            player.rotation = Quaternion.LookRotation(lookDir);

        // Intoarce NPC-ul spre player
        Vector3 npcLookDir = player.position - npc.position;
        npcLookDir.y = 0f;
        if (npcLookDir != Vector3.zero)
            npc.rotation = Quaternion.LookRotation(npcLookDir);

        // Seteaza animatia playerului pe idle
        var playerAnim = player.GetComponent<Animator>();
        if (playerAnim != null)
            playerAnim.SetFloat("Speed", 0f);

        // Opreste sunetul de scream
        var witchAudio = Object.FindFirstObjectByType<WitchBehavior>()?.screamAudio;
        if (witchAudio != null && witchAudio.isPlaying)
            witchAudio.Stop();

        // Pozitioneaza vrajitoarele in cerc si le opreste
        var witchManager = Object.FindFirstObjectByType<WitchCirclePositioner>();
        if (witchManager != null && witchManager.witches != null)
        {
            foreach (var witch in witchManager.witches)
            {
                if (witch == null) continue;

                var agent = witch.GetComponent<NavMeshAgent>();
                if (agent != null) agent.enabled = false;

                var anim = witch.GetComponent<Animator>();
                if (anim != null) anim.SetBool("isRunning", false);
            }

            // Pozitioneaza vrajitoarele si le face sa se uite la player
            witchManager.PositionWitchesInCircle(player);
        }

        // Afiseaza replica dupa o secunda
        Invoke(nameof(ShowHelpMeLine), 1f);
    }

    void ShowHelpMeLine()
    {
        SpeechBubbleManager.Instance?.ShowLine(
            "Help me!",
            SpeechBubbleManager.Speaker.NPC,
            3f
        );
    }
}
