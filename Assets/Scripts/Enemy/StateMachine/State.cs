using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//create a child class in another file to handle the state

[RequireComponent(typeof(EnemyManager))]
public class State : MonoBehaviour
{
    public EnemyManager enemy;
    public List<Transistion> transistions = new();


    private void Awake()
    {
        enemy = GetComponent<EnemyManager>();

        AwakeExt();

    }

    //enable anything you want to have active when the child class activates
    public virtual void OnEnable()
    {


    }

    //disable anything you want to have active when the child class activates
    public virtual void OnDisable()
    {

    }

    public virtual void FixedUpdateExt()
    {


    }

    public virtual void AwakeExt()
    {


    }

    //necesary for checking which state to transition to
    private void FixedUpdate()
    {
      foreach ( Transistion transistion in transistions )
        {
            if(transistion.condition.Test(enemy))
            {
                transistion.target.enabled = true;
                this.enabled = false;



            }



        }
      
      FixedUpdateExt();

    }
    [Serializable]
    public struct Transistion 
    {
        public Condition condition;
        public State target;


    }


}
