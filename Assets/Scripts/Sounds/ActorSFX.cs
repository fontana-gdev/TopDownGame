using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSFX : MonoBehaviour
{
    
    [SerializeField] AudioSource audioSource;

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
}
