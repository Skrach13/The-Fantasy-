using UnityEngine;

//TODO
public class SoundManagerPersone : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioColip;
    
    public void PlaySoundClip(int indexSound)
    {
      //  _audioSource.clip = _audioColip[indexSound];
      //  _audioSource.Play();
    }
 
    public void StopAudioClip()
    {
        _audioSource.Stop();
    }
}
