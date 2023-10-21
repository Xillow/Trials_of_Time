using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{

    public override void AwakeExt()
    {
        base.AwakeExt();



    }

    public override void FixedUpdateExt()
    {
        base.FixedUpdateExt();

        if (enemy.attFin)
        {
            enemy.enemy_animator.SetTrigger("Attack Start");
        }
        //animationtrigger for attack

        
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        enemy.attFin = true;

    }




}
