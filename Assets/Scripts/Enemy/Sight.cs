using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public string targetTag = "Player";
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
        
        Vector2 enemyPosition = gameObject.transform.position;
        Vector2 targetPosition = collision.gameObject.transform.position;
        Vector2 direction = targetPosition - enemyPosition;

        //Ray ray = new(enemyPosition, direction.normalized);
        RaycastHit2D hit;
        Debug.DrawRay(enemyPosition, direction);

        //needs layer mask
        
        if (hit = Physics2D.Raycast(enemyPosition, direction, direction.magnitude, LayerMask.GetMask("Player"), 0, 0))
        {
            
            if (hit.collider.CompareTag(targetTag))
            {
                enemy.inView = true;
                enemy.target = hit.collider.gameObject;
                return;
            }
        }
        //enemy.target = null;
    }

    //This will need to be changed
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemy.target != null && collision.gameObject == enemy.target)
        {
            enemy.target = null;
            enemy.inView = false;
        }
    }


}
