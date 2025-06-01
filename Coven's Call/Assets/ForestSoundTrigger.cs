using UnityEngine;

public class ForestSoundTrigger : MonoBehaviour
{
    public GameObject fireSound;
    public AudioSource windSound;
    public AudioSource woodCreekSound;
    public AudioSource crowSound;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            // Stop fire sound
            if (fireSound != null)
                fireSound.SetActive(false);

            // Play wind sound (loop)
            if (windSound != null && !windSound.isPlaying)
            {
                windSound.loop = true;
                windSound.Play();
            }

            // Play wood creek sound (loop)
            if (woodCreekSound != null && !woodCreekSound.isPlaying)
            {
                woodCreekSound.loop = true;
                woodCreekSound.Play();
            }

            // Start repeating crow sound every 10 seconds
            if (crowSound != null)
            {
                InvokeRepeating("PlayCrowSound", 0f, 20f);
            }
        }
    }

    void PlayCrowSound()
    {
        if (crowSound != null)
        {
            crowSound.Play();
        }
    }
}
