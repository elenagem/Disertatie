using UnityEngine;

public class CampfireScreamTrigger : MonoBehaviour
{
    public GameObject player;           // Setezi Playerul in Inspector
    public AudioSource screamAudio;     // AudioSource cu tipatul
    public float delayBeforeScream = 3f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.gameObject == player)
        {
            triggered = true;
            Invoke("PlayScream", delayBeforeScream);
        }
    }

    void PlayScream()
    {
        if (screamAudio != null)
            screamAudio.Play();
    }
}
