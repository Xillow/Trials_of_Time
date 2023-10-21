using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemList {PurpleKeycard, GreenKeycard, BlueKeycard, RedKeycard, YellowKeycard, Sword, TurboBoots, SwordUpgrade};

[CreateAssetMenu(menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{

    private int maxHealth = 100;

    public int currentHealth;

    /*[SerializeField]
    private bool grounded = false;

    [SerializeField]
    private bool climbing = false;
    */

    
    public float baseVelocity = 10f;                  // Top velocity while walking

    public float baseClimbVelocity = 0.1f;

    public float sneakVelocityMultiplier = 0.5f;      // Multiplier for velocity while sneaking

    public float runVelocityMultiplier = 1.5f;        // Multiplier for velocity while running

    public float jumpForce = 250f;

    public float dashForce = 3f;

    public float dashCooldownPeriod = 100f;

    public float attackCooldownPeriod = 10f;

    public float invincibilityPeriod = 60f;

    public int weaponDamage = 1;

    public bool lossOfControl = false;

    public bool tookPortal = false;

    //enum at top of page shows item values 
    public bool[] items;

    public int spawnPos = 0;


    private Vector2 currPos;



    /*public bool IsGrounded()
    {
        return grounded;

    }

    public bool IsClimbing()
    {
       return climbing;
    }*/
  
    public void resetWeaponDamage()
    {


    }

    public int GetMaxHealth()
    {

        return maxHealth;
    }

    public Vector2 GetPos()
    {

        return currPos;

    }

    public void SetPos(Vector2 Position)
    {
        currPos = Position;

    }



}
