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
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public async void LightUp()
    {
        if (hasBeenLit) return;
        hasBeenLit = true;
        spriteRenderer.color = Color.red;
        // Play happy animation
        await Awaitable.WaitForSecondsAsync(3f);
        SceneManager.LoadScene(++currentLevel);
    }
}
