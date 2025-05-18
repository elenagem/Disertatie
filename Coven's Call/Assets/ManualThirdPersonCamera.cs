using UnityEngine;

public class ManualThirdPersonCamera : MonoBehaviour
{
    public Transform target; // CameraPivot
    public Vector2 sensitivity = new Vector2(120f, 120f);
    public float distance = 4f;
    public Vector2 pitchClamp = new Vector2(-30f, 60f);

    float yaw = 0f;
    float pitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity.x * Time.unscaledDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity.y * Time.unscaledDeltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchClamp.x, pitchClamp.y);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
