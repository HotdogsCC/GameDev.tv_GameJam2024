using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private int speedLvl = 1;
    [SerializeField] private List<int> speedValues = new List<int>();
    [SerializeField] private List<int> speedPrices = new List<int>();

    [Header("Fire Rate")]
    [SerializeField] private int fireRateLvl = 1;
    [SerializeField] private List<int> fireRateValues = new List<int>();
    [SerializeField] private List<int> fireRatePrices = new List<int>();

    [Header("Damage")]
    [SerializeField] private int damageLvl = 1;
    [SerializeField] private List<int> damageValues = new List<int>();
    [SerializeField] private List<int> damagePrices = new List<int>();

    [Header("Object References")]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private ModeManager modeManager;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI fireRateText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private GameObject speedUpgrader;
    [SerializeField] private GameObject fireRateUpgrader;
    [SerializeField] private GameObject damageUpgrader;
    [SerializeField] private CurrencyManager currencyManager;

    private void Start()
    {
        player.moveSpeed = speedValues[speedLvl - 1];
        modeManager.firingRate = 1 / (float)fireRateValues[fireRateLvl - 1];
        modeManager.gunDamage = damageValues[damageLvl - 1];

        currencyManager = FindObjectOfType<CurrencyManager>();

        speedText.text = "Upgrade Speed<br>$" + speedPrices[speedLvl].ToString();
        fireRateText.text = "Upgrade Fire Rate<br>$" + fireRatePrices[fireRateLvl].ToString();
        damageText.text = "Upgrade Damage<br>$" + damagePrices[damageLvl].ToString();

        speedUpgrader = GameObject.FindGameObjectWithTag("speed");
        fireRateUpgrader = GameObject.FindGameObjectWithTag("fireRate");
        damageUpgrader = GameObject.FindGameObjectWithTag("damage");
    }

    public void SpeedBtn()
    {
        Debug.Log("SHPEED");

        if (currencyManager.GetCurrentMoneyBalance() >= speedPrices[speedLvl])
        {
            currencyManager.UpdateMoney(-speedPrices[speedLvl]);
            speedLvl++;
            player.moveSpeed = speedValues[speedLvl - 1];
            if (speedLvl == 4)
            {
                Destroy(speedUpgrader);
            }
            else
            {    
                speedText.text = "Upgrade Speed<br>$" + speedPrices[speedLvl].ToString();
            }
        }
        
    }

    public void FireRateBtn()
    {
        Debug.Log("Fire Rate Increased");

        if (currencyManager.GetCurrentMoneyBalance() >= fireRatePrices[fireRateLvl])
        {
            currencyManager.UpdateMoney(-fireRatePrices[fireRateLvl]);
            fireRateLvl++;
            modeManager.firingRate = 1 / (float)fireRateValues[fireRateLvl - 1];
            if (fireRateLvl == 4)
            {
                Destroy(fireRateUpgrader);
            }
            else
            {
                fireRateText.text = "Upgrade Fire Rate<br>$" + fireRatePrices[fireRateLvl].ToString();
            }
        }
    }

    public void DamageBtn()
    {
        Debug.Log("Damage Increased");

        if (currencyManager.GetCurrentMoneyBalance() >= damagePrices[damageLvl])
        {
            currencyManager.UpdateMoney(-damagePrices[damageLvl]);
            damageLvl++;
            modeManager.gunDamage = damageValues[damageLvl - 1];
            if (damageLvl == 4)
            {
                Destroy(damageUpgrader);
            }
            else
            {
                damageText.text = "Upgrade Damage<br>$" + damagePrices[damageLvl].ToString();
            }
        }
    }
}
