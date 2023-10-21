using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearPlayer : AttackMvmt
{


    private Collider2D enemy;
    //Get  PlayerInfo and set speed to ProjectileSpeed
    
    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * transform.right, Space.World);
        transform.Rotate(5f * Time.deltaTime * transform.forward, Space.World);
        enemy = Physics2D.OverlapBox(transform.position, hitBox, 0, LayerMask.GetMask("Enemy"));

        if (enemy != null)
        {
            enemy.GetComponent<EnemyManager>().TakeDamageCounter(EnemyInfo.GetAttackStrength());

        }


    }

    public override void Target()
    {
        transform.right = EnemyInfo.currPos.position - transform.position;
        transform.right = new Vector3(transform.right.x, transform.right.y, 0);

    }

}
