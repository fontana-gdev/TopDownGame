using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChanger : MonoBehaviour
{
    
    [SerializeField] AudioClip bgmMusic;
    
    private AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayBGM(bgmMusic);
    }

}
