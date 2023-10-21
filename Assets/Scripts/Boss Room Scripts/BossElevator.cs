using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElevator : MonoBehaviour
{
    bool isActive;

    Vector3 delta;

    float finalYPos;

    [SerializeField]
    GameObject elevator;
    // Start is called before the first frame update
    private void Start()
    {
        isActive = false;

        finalYPos = -14.5f;

        delta = new Vector3(0f, -0.1f, 0f);
    }


    // Update is called once per frame

    private void FixedUpdate()
    {
        if (isActive)
        {

            elevator.transform.position = elevator.transform.position + delta;

        }

        if (elevator.transform.position.y < finalYPos)
        {
            isActive = false;

            GetComponent<BossElevator>().enabled = false;

        }

    }

}
