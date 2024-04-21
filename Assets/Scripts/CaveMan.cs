using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveMan : MonoBehaviour
{
    private bool isDead;
    public async void Die()
    {
        if (isDead) return;
        isDead = true;
        await Awaitable.WaitForSecondsAsync(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}