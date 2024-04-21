using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : MonoBehaviour
{
    public bool musicIsPlaying;
    void Start()
    {
        if (musicIsPlaying) Destroy(gameObject);
        musicIsPlaying = true;
        DontDestroyOnLoad(gameObject);        
    }
}
