using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMirror : MonoBehaviour, IBeamReactor
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    public void React()
    {
        source.PlayOneShot(clip);
    }
    
    public void End()
    {
    }
}
