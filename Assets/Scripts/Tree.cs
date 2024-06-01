using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    [SerializeField] private int health = 100;
    [SerializeField] private SceneLoader loader;
    void Start()
    {
        loader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = health;

        if(health <= 0)
        {
            loader.LoadLoseScreen();
        }
    }

    public void Damaged()
    {
        health--;
    }
}
