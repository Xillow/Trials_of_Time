using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linear : AttackMvmt
{

    
    
    private Collider2D player;
    //Get  PlayerInfo and set speed to ProjectileSpeed
    
    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * transform.right, Space.World);
        transform.Rotate(5f * Time.deltaTime * transform.forward, Space.World);
        player = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));

        if (player != null)
        {
            player.GetComponent<PlayerManager>().TouchDamage(EnemyInfo.GetAttackStrength());

        }

        
    }


    public override void Target()
    {
        transform.right = GameObject.Find("Player").transform.position - transform.position;
        transform.right = new Vector3(transform.right.x, transform.right.y, 0);

    }


    
}
