using UnityEngine;

//TODO
public class SoundManagerPersone : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioColip;
    [SerializeField] private AudioSource _audioSource;
    
    public void PlaySoundClip(int indexSound)
    {
        _audioSource.clip = _audioColip[indexSound];
        _audioSource.Play();
    }
 
    public void StopAudioClip()
    {
        _audioSource.Stop();
    }
}
