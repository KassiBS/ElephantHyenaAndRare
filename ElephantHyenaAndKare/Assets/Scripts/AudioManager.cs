using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header(" Audio Source ")]
    public AudioSource SFXSource;
    public AudioSource SS2;

    [Header(" Audio Clip ")]
    public AudioClip Bird;
    public AudioClip WoodCreaking;
    public AudioClip Wind;
    public AudioClip Drink;
    public AudioClip ElephantLL;
    public AudioClip ElephantLo;
    public AudioClip Tree;
    public AudioClip HyenaLaugh;
    public AudioClip HyenaWhine;
    public AudioClip Rock;
    public AudioClip Stomping;
    public AudioClip Thump;
    public AudioClip Whip;
    public AudioClip Swim;

    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource.clip != clip)
        {
            SFXSource.clip = clip;
            SFXSource.Play();
        }
    }

    public void PlaySFX2(AudioClip clip)
    {
        if (SS2.clip != clip)
        {
            SS2.clip = clip;
            SS2.Play();
        }
    }
}
