using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bonfire : MonoBehaviour
{
    public static int currentLevel = 1;
    private bool hasBeenLit = false;
    [SerializeField] private GameObject fire;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip daveMadeFireSound;

    public async void LightUp()
    {
        if (hasBeenLit) return;
        hasBeenLit = true;
        fire.SetActive(true);
        audioSource.PlayOneShot(daveMadeFireSound);
        await Awaitable.WaitForSecondsAsync(3f);
        SceneManager.LoadScene(++currentLevel);
    }
}
