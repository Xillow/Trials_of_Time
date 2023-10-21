using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    public EnemyManager EnemyManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Attack()
    {
        EnemyManager.Attack();

    }


    public void AttackFin()
    {
        EnemyManager.AttackFin();
    }

    public void Death()
    {
        EnemyManager.EnemyDeath();

    }
}
