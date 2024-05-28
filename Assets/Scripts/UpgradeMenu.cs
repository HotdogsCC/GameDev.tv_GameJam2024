using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private int speedLvl = 1;
    [SerializeField] private List<int> speedValues = new List<int>();

    [Header("Fire Rate")]
    [SerializeField] private int fireRateLvl = 1;
    [SerializeField] private List<int> fireRateValues = new List<int>();

    [Header("Damage")]
    [SerializeField] private int damageLvl = 1;
    [SerializeField] private List<int> damageValues = new List<int>();

    [Header("Object References")]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private ModeManager modeManager;

    private void Start()
    {
        player.moveSpeed = speedValues[speedLvl - 1];
        modeManager.firingRate = 1 / (float)fireRateValues[fireRateLvl - 1];
        modeManager.gunDamage = damageValues[damageLvl - 1];
    }

    public void SpeedBtn()
    {
        Debug.Log("SHPEED");
        speedLvl++;
        if(speedLvl > 4)
        {
            speedLvl = 4;
        }
        player.moveSpeed = speedValues[speedLvl - 1];
    }

    public void FireRateBtn()
    {
        Debug.Log("Fire Rate Increased");
        fireRateLvl++;
        if (fireRateLvl > 4)
        {
            fireRateLvl = 4;
        }
        modeManager.firingRate = 1 / (float)fireRateValues[fireRateLvl - 1];
    }

    public void DamageBtn()
    {
        Debug.Log("Damage Increased");
        damageLvl++;
        if (damageLvl > 4)
        {
            damageLvl = 4;
        }
        modeManager.gunDamage = damageValues[damageLvl - 1];
    }
}
