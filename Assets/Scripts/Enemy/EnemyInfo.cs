using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private int attackStrength;

    [SerializeField]
    private int bodyDamage;

    [SerializeField]
    private float moveSpeed;

    public int projectileCount = 1;

    public float attackRate = 2;

    public float projectileSpeed;

    [Range(0.1f, 1)]
    public float arriveDistance = 1;
    public float waitTime = 0.5f;


    public Transform currPos;
    


    public int defense = 0;

    public int GetMaxHealth()
    {

        return maxHealth;
    }

   public int GetBodyDamage()
    {

        return bodyDamage;
    }

    public int GetAttackStrength()
    {

        return attackStrength;
    }

    public float GetMoveSpeed()
    {

        return moveSpeed;
    }



}
