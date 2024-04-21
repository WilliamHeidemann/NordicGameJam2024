using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bonfire : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private static int currentLevel = 0;
    private bool hasBeenLit = false;
    [SerializeField] private GameObject fire;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public async void LightUp()
    {
        if (hasBeenLit) return;
        hasBeenLit = true;
        fire.SetActive(true);
        await Awaitable.WaitForSecondsAsync(1.5f);
        SceneManager.LoadScene(++currentLevel);
    }
}
