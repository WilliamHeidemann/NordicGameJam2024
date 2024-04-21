using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private Image fade;
    void Start()
    {
        LeanTween.alpha(fade.rectTransform, 0f, 10f)
            .setEase(LeanTweenType.easeOutQuad);
    }
}
