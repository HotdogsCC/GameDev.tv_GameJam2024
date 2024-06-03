using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    [SerializeField] private int health = 100;
    [SerializeField] private SceneLoader loader;
    [SerializeField] private Volume volume;
    [SerializeField] private ColorAdjustments colorAdjustments;
    void Start()
    {
        loader = FindObjectOfType<SceneLoader>();
        if(volume.profile.TryGet<ColorAdjustments>(out ColorAdjustments comp))
        {
            colorAdjustments = comp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = health;

        colorAdjustments.saturation.value = (12 * health / 10) - 100;

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
