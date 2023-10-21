using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearing : MonoBehaviour
{
    public string targetTag = "Sound";
    public EnemyManager enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyManager>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag) == false)
        {
            return;
        }
        
        enemy.interested = true;
        enemy.newSound = true;

        enemy.soundPosition = collision.gameObject.transform.position;
        //enemy.soundPosition = collision.ClosestPoint(transform.position);


    }
}
