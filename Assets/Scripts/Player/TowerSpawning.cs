using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawning : MonoBehaviour
{
    [SerializeField] float rayDistance = 10f;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private GameObject wallPrefab;

    private GameObject currentSelection;

    // Start is called before the first frame update
    void Start()
    {
        currentSelection = Instantiate(wallPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red, 0);

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, rayDistance))
        {
            currentSelection.transform.position = hit.point;
            currentSelection.transform.eulerAngles = new Vector3(0, playerCamera.transform.eulerAngles.y - 90, 0);
        }

        if(Input.GetMouseButtonDown(0))
        {
            currentSelection = Instantiate(wallPrefab);
        }
    }
}
