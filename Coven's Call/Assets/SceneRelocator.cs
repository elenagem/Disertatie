using UnityEngine;
using UnityEngine.AI;

public class SceneRelocator : MonoBehaviour
{
    public Transform player;
    public Transform npc;

    public Transform playerSpawnPoint;
    public Transform npcSpawnPoint;

    public float delayBeforeTeleport = 5f;

    public AudioSource creepyAmbientSound;

    public void StartRelocationSequence()
    {
        Invoke(nameof(RelocateScene), delayBeforeTeleport);
    }

    void RelocateScene()
    {
        // Dezactiveaza controlul jucatorului
        var playerController = player.GetComponent<SimplePlayerController>();
        if (playerController != null)
            playerController.enabled = false;

        var cameraController = player.GetComponentInChildren<ManualThirdPersonCamera>();
        if (cameraController != null)
            cameraController.enabled = false;

        // Dezactiveaza urmarea NPC-ului
        var npcFollower = npc.GetComponent<NPCFollower>();
        if (npcFollower != null)
            npcFollower.enabled = false;

        // Opreste navmesh-ul de pe NPC
        var npcAgent = npc.GetComponent<NavMeshAgent>();
        if (npcAgent != null)
        {
            npcAgent.isStopped = true;
            npcAgent.ResetPath();
        }

        // Teleporteaza playerul
        if (player != null && playerSpawnPoint != null)
        {
            player.position = playerSpawnPoint.position;
            player.rotation = playerSpawnPoint.rotation;
        }

        // Teleporteaza NPC-ul
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

        // Pune animatia playerului pe idle
        var playerAnim = player.GetComponent<Animator>();
        if (playerAnim != null)
            playerAnim.SetFloat("Speed", 0f);

        // Opreste sunetul de scream daca e inca activ
        var witchAudio = Object.FindFirstObjectByType<WitchBehavior>()?.screamAudio;
        if (witchAudio != null && witchAudio.isPlaying)
            witchAudio.Stop();

        // Opreste vrajitoarele si le pozitioneaza in cerc
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

            witchManager.PositionWitchesInCircle(player);
        }

        // Porneste sunetul ambiental creepy
        if (creepyAmbientSound != null && !creepyAmbientSound.isPlaying)
            creepyAmbientSound.Play();

        // Afiseaza replica "Help me!" dupa 1 secunda
        Invoke(nameof(ShowHelpMeLine), 1f);
    }

    void ShowHelpMeLine()
    {
        SpeechBubbleManager.Instance?.ShowLine(
            "Help me!",
            SpeechBubbleManager.Speaker.NPC,
            3f
        );

        // Apeleaza alegerea dupa ce dispare balonul
        Invoke(nameof(TriggerHelpChoice), 2f);
    }

    void TriggerHelpChoice()
    {
        var helpTrigger = Object.FindFirstObjectByType<HelpChoiceTrigger>();
        if (helpTrigger != null)
            helpTrigger.BeginChoice();
    }
}
