using System;
using UnityEngine;

public class Crystal : MonoBehaviour, IBeamReactor
{
    private SpriteRenderer spriteRenderer;
    public Sprite blue;
    public Sprite orange;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void React()
    {
        spriteRenderer.sprite = orange;
    }

    public void End()
    {
        spriteRenderer.sprite = blue;
    }
}