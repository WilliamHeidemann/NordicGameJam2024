using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOver : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}