using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPast1Start : MonoBehaviour
{
    public Animator Boss_Animator;
    public PlayerInfo playerInfo;
    public Rigidbody2D playerMvmt;


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player")) 
        {

            playerInfo.lossOfControl = true;
            playerInfo.items[(int)ItemList.Sword] = false;

            playerMvmt.velocity = new Vector3(0f, playerMvmt.velocity.y, 0f);


            Boss_Animator.SetTrigger("InRange");
            
        }
    }
}
