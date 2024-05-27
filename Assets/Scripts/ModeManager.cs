using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float rayDistance = 10f;

    [Header("Object References")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private GameObject wallPrefab;

    [Header("Materials")]
    [SerializeField] private Material canPlaceMat;
    [SerializeField] private Material cantPlaceMat;
    [SerializeField] private Material towerMat;

    private bool lookingAtSomwthingICanSpawn = false;
    private GameObject currentSelection;

    // Start is called before the first frame update
    void Start()
    {
        currentSelection = Instantiate(wallPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, rayDistance))
        {
            lookingAtSomwthingICanSpawn = true;
            currentSelection.GetComponent<MeshRenderer>().material = canPlaceMat;
            currentSelection.transform.position = hit.point;
            currentSelection.transform.eulerAngles = new Vector3(0, playerCamera.transform.eulerAngles.y - 90, 0);
        }
        else
        {
            lookingAtSomwthingICanSpawn = false;
            currentSelection.GetComponent<MeshRenderer>().material = cantPlaceMat;
        }

        //Left Click Input
        if(Input.GetMouseButtonDown(0))
        {
            //Checks player can spawn in the thing
            if (lookingAtSomwthingICanSpawn)
            {
                //Checks player has enough money
                if (currencyManager.GetCurrentMoneyBalance() >= currencyManager.wallCost)
                {
                    currencyManager.UpdateMoney(-currencyManager.wallCost);
                    currentSelection.GetComponent<MeshRenderer>().material = towerMat;
                    currentSelection = Instantiate(wallPrefab);
                }
            }

        }
    }
}
