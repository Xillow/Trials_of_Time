using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stationary : AttackMvmt
{
    private Collider2D player;
      
    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {

        player = Physics2D.OverlapBox(transform.position, hitBox, 0 , LayerMask.GetMask("Player"));

        if (player != null)
        {
            player.GetComponent<PlayerManager>().TouchDamage(EnemyInfo.GetAttackStrength());

        }


    }
}
