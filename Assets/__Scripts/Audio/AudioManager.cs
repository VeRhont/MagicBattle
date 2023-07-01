using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _buySound;
    [SerializeField] private AudioClip _cancelSound;
    [SerializeField] private AudioSource _stepsSource;

    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        TurnOffStepsSound();
    }

    public void TurnOnStepsSound()
    {
        _stepsSource.mute = false;
    }

    public void TurnOffStepsSound()
    {
        _stepsSource.mute = true;
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip == null) return;

        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.panStereo = Random.Range(-0.1f, 0.1f);
        _audioSource.PlayOneShot(audioClip);
    }

    public void PlayClickSound() => PlaySound(_clickSound);
    public void PlayBuySound() => PlaySound(_buySound);
    public void PlayCancelSound() => PlaySound(_cancelSound);
}
