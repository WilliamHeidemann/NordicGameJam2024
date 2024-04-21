using System;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public static Prompt Instance;
    [SerializeField] private GameObject prefab;
    private GameObject e;

    private void Awake()
    {
        Instance = this;
        e = Instantiate(prefab);
        HideE();
    }

    public void ShowE(Vector3 position)
    {
        e.transform.position = position;
        e.SetActive(true);
    }

    public void HideE()
    {
        e.SetActive(false);
    }
}