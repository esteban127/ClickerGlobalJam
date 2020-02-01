using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Dictionary<string,AudioClip> sfx;
    [SerializeField] AudioSource sfxSource;

    public void PlaySfx(string key)
    {
        sfxSource.PlayOneShot(sfx[key]);
    }
    
}
