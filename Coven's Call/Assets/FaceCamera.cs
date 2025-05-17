using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            Vector3 camPos = Camera.main.transform.position;
            Vector3 lookDir = transform.position - camPos;
            lookDir.y = 0; // păstrează textul drept
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
