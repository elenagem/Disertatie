using UnityEngine;
using System;

public class AutoMovePlayer : MonoBehaviour
{
    public float speed = 3f;
    private Vector3 target;
    private bool isMoving = false;
    private Action onReached;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Actualizeaza animatia
        if (animator != null)
            animator.SetFloat("Speed", speed); // declanseaza "Run"

        // Verifica daca a ajuns (toleranta marita)
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            isMoving = false;

            if (animator != null)
                animator.SetFloat("Speed", 0f); // revine la "Idle"

            Debug.Log("Ajuns la target, se apeleaza callback.");
            onReached?.Invoke();
        }
    }

    public void MoveTo(Vector3 destination, Action callback = null)
    {
        target = destination;
        isMoving = true;
        onReached = callback;

        // Intoarce jucatorul spre directia de mers
        Vector3 direction = target - transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }
}
