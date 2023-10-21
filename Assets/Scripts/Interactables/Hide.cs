using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : Interactable
{

    GameObject player, soundProducer;
    Collider2D playerCollider;
    Rigidbody2D rb2d;
    public PlayerInfo playerInfo;

    private void Start()
    {
        player = FindObjectOfType<PlayerManager>().gameObject;
        soundProducer = player.GetComponentInChildren<CircleCollider2D>().gameObject;
        playerCollider = player.GetComponent<Collider2D>();
        rb2d = player.GetComponent<Rigidbody2D>();
    }

    public override void PreformAction()
    {
        if(player.CompareTag("Player"))
        {
            playerInfo.lossOfControl = true;
            player.transform.position = transform.position + new Vector3(0, 0, 102);
            player.tag = "Hidden";
            player.layer = LayerMask.NameToLayer("Hidden");
            rb2d.gravityScale = 0;
            rb2d.velocity = new Vector2 (0,0);
            playerCollider.enabled = false;
            soundProducer.SetActive(false);
            player.GetComponent<PlayerManager>().player_animator.SetBool("Hidden", true);

        }
        else
        {

            playerInfo.lossOfControl = false;
            player.transform.position = transform.position + new Vector3(0, -0.2f, 0); ;
            player.tag = "Player";
            player.layer = LayerMask.NameToLayer("Player");
            playerCollider.enabled = true;
            rb2d.gravityScale = 1; 
            soundProducer.SetActive(true);
            player.GetComponent<PlayerManager>().player_animator.SetBool("Hidden", false);
        }


    }


    }
