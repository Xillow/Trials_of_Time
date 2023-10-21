using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch : Interactable
{
    GameObject RemovableLaser;


    // Start is called before the first frame update
    void Start()
    {
        RemovableLaser = GameObject.Find("RemovableLaser");

    }


    public override void PreformAction()
    {

       RemovableLaser.SetActive(false);
       gameObject.SetActive(false);


    }
}
