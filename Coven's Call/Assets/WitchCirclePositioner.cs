using UnityEngine;
using System.Collections.Generic;

public class WitchCirclePositioner : MonoBehaviour
{
    public List<GameObject> witches;
    public Transform centerPoint; // NPC
    public float radius = 3f;

    // Nou: primeste si tinta catre care se uita
    public void PositionWitchesInCircle(Transform lookAtTarget)
    {
        if (witches.Count == 0 || centerPoint == null || lookAtTarget == null) return;

        float angleStep = 360f / witches.Count;

        for (int i = 0; i < witches.Count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
            Vector3 newPos = centerPoint.position + offset;

            witches[i].transform.position = newPos;

            // Le intoarcem spre jucator
            Vector3 lookDir = lookAtTarget.position - newPos;
            lookDir.y = 0f;
            if (lookDir != Vector3.zero)
                witches[i].transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
