using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimplePlayerController : MonoBehaviour
{
    public float walkSpeed = 1.2f;     // Potrivit cu Walk
    public float runSpeed = 3.6f;      // Potrivit cu Run
    public float gravity = -9.81f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;
    private float yVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Directia camerei
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = (camForward * vertical + camRight * horizontal).normalized;

        // Sprint
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        Vector3 velocity = moveDirection * currentSpeed;

        // Gravitatie
        if (controller.isGrounded)
            yVelocity = 0f;
        else
            yVelocity += gravity * Time.deltaTime;

        velocity.y = yVelocity;

        // Miscare
        controller.Move(velocity * Time.deltaTime);

        // Roteste jucatorul in directia de mers
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        // Trimite valoare normalizata in Animator
        animator.SetFloat("Speed", moveDirection.magnitude);
    }
}
