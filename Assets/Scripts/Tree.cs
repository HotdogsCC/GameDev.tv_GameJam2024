using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    [SerializeField] private int health = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = health;
    }

    public void Damaged()
    {
        health--;
    }
}
