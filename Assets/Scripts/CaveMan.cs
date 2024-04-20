using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveMan : MonoBehaviour
{
    public async void Die()
    {
        await Awaitable.WaitForSecondsAsync(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}