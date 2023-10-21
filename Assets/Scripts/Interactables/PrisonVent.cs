using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonVent : Interactable
{

    GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
      player = FindObjectOfType<PlayerManager>().gameObject;

    }


    public override void PreformAction()
    {

        player.transform.position = new Vector3(24f, -14.15f, 0);
        

    }

}
