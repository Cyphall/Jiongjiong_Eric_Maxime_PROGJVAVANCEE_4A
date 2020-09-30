using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SetSound : MonoBehaviour
{
    public static AudioClip BombCharge, BombExplosion;
    public static AudioSource AudioSrc;

    private void Start()
    {
        BombCharge = Resources.Load<AudioClip>("BombCharge");
        BombExplosion = Resources.Load<AudioClip>("Explosion");
        AudioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string Clip)
    {
        switch (Clip)
        {
            case "BombCharge" :
                AudioSrc.PlayOneShot(BombCharge);
                break;
            case "Explosion" :
                AudioSrc.PlayOneShot(BombExplosion);
                break;
        }
            
    }

    public static void StopSound()
    {
        AudioSrc.Stop();
    }
    
    
}
