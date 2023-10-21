using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Interactable
{

    bool isActive;

    Vector3 delta;

    float finalYPos;


  [SerializeField]
    GameObject player, elevator;

    [SerializeField]
    PlayerInfo playerData;

    private void Start()
    {
        isActive = false;
        
        finalYPos =  -14.5f;

        delta = new Vector3(0f, -0.1f, 0f);
    }

    public override void PreformAction()
    {
        isActive = true;

        player.transform.position = new Vector3(-8f, 7.9f, 1f);

        player.transform.localScale = new Vector3(1f, 1f, 1f);

        playerData.lossOfControl = true;

    }

    private void FixedUpdate()
    {
       if(isActive) {

            elevator.transform.position = elevator.transform.position + delta;

            player.transform.position = player.transform.position + delta;
        
        }

        if (elevator.transform.position.y < finalYPos)
        {
            isActive = false;
            playerData.lossOfControl = false;
            GetComponent<Elevator>().enabled = false;

        }
        

    }
}
