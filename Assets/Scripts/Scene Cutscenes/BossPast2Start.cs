using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPast2Start : MonoBehaviour
{
    public Animator Player_Animator;
    public PlayerInfo playerInfo;
    public Rigidbody2D playerMvmt;



    // Start is called before the first frame update
    void Awake()
    {
        playerInfo.lossOfControl = true;
        playerMvmt.gravityScale = 0;
        Player_Animator.SetTrigger("TeleportIn");
        Player_Animator.SetTrigger("CutsceneStart");

    }



}
