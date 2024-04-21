using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] private Image fade;
    // Start is called before the first frame update
    async void Start()
    {
        await Awaitable.WaitForSecondsAsync(3f);
        LeanTween.alpha(fade.rectTransform, 1f, 1f)
            .setEase(LeanTweenType.easeInQuad);
        await Awaitable.WaitForSecondsAsync(1f);
        SceneManager.LoadScene(1);
    }
}
