using UnityEngine;

namespace Tourism.AudioManager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        public void PlaySound(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void StopSound()
        {
            audioSource.Stop();
        }
    }
}