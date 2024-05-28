using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrader : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private UpgradeMenu upgradeMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 3))
            {
                if (hit.transform.tag == "speed")
                {
                    upgradeMenu.SpeedBtn();
                }
                if (hit.transform.tag == "fireRate")
                {
                    upgradeMenu.FireRateBtn();
                }
                if (hit.transform.tag == "damage")
                {
                    upgradeMenu.DamageBtn();
                }
            }
        }
    }
}
