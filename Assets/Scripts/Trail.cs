using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Trail : MonoBehaviour
{
    [SerializeField] Vector3 targetPosition;
    [SerializeField] private int moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if(transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }

    public void SetTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
    }
}
