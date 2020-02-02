using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] sfx;
    [SerializeField] AudioSource sfxSource;

    public void PlaySfx(int key,float pitch,float volume)
    {
        sfxSource.pitch = pitch;
        sfxSource.volume = volume;
        sfxSource.PlayOneShot(sfx[key]);
    }
    
}
