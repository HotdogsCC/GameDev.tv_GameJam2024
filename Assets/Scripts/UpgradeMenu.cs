using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject UpgradeBtns;
    public void SpeedBtn()
    {
        Debug.Log("SHPEED");
    }

    public void FireRateBtn()
    {
        Debug.Log("Fire Rate Increased");
    }

    public void Damage()
    {
        Debug.Log("Damage Increased");
    }
}
