using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CaveMan : MonoBehaviour
{
    private bool isDead;
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public async void Die()
    {
        if (isDead) return;
        isDead = true;
        deathAudioSource.Play();
        GetComponent<PlayerInput>().enabled = false;
        spriteRenderer.color = Color.red;
        await Awaitable.WaitForSecondsAsync(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}