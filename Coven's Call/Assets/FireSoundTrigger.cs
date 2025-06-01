using UnityEngine;

public class FireSoundTrigger : MonoBehaviour
{
    public AudioSource fireAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fireAudio != null && !fireAudio.isPlaying)
            {
                fireAudio.Play();
            }
        }
    }
}
