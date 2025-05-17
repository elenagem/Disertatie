using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent == null || animator == null) return;

        float speed = agent.velocity.magnitude;

        animator.SetFloat("Speed", speed);
    }

    // event "OnFootstep" 
    public void OnFootstep()
    {
     
    }
}
