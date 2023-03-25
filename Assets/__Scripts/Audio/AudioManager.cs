using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.panStereo = Random.Range(-0.2f, 0.2f);
        _audioSource.Play();
    }
}
