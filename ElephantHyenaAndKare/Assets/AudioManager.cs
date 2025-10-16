using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header(" Audio Source ")]
    public AudioSource SFXSource;

    [Header(" Audio Clip ")]
    public AudioClip Bird; 
    public AudioClip WoodCreaking;
    public AudioClip Wind;
    public AudioClip Drink; 
    public AudioClip Elephant;
    public AudioClip Tree;
    public AudioClip Hyena;
    public AudioClip Rock;
    public AudioClip Stomping;
    public AudioClip Thump;
    public AudioClip Whip;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
