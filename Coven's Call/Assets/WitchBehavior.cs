using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class WitchBehavior : MonoBehaviour
{
    public List<GameObject> witches;
    public GameObject player;
    public GameObject npc;
    public AudioSource screamAudio;

    public void TriggerWitchTurn()
    {
        foreach (var witch in witches)
        {
            Vector3 lookTarget = new Vector3(player.transform.position.x, witch.transform.position.y, player.transform.position.z);
            witch.transform.LookAt(lookTarget);
        }

        StartCoroutine(DelayedChase());
    }

    IEnumerator DelayedChase()
    {
        yield return new WaitForSeconds(1f);

        if (screamAudio != null)
            screamAudio.Play();

        foreach (var witch in witches)
        {
            NavMeshAgent agent = witch.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }

            Animator anim = witch.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("isRunning", true);
            }
        }

        // NPC spune "Run!" dupa 1 secunda
        Invoke(nameof(ShowRunLine), 1f);

        // Porneste secventa de relocare a scenei (dupa 3-4 secunde)
        Object.FindFirstObjectByType<SceneRelocator>()?.StartRelocationSequence();
    }

    void ShowRunLine()
    {
        SpeechBubbleManager.Instance.ShowLine(
            "Run!",
            SpeechBubbleManager.Speaker.NPC,
            2f
        );
    }
}
