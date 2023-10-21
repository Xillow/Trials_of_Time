using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    public PlayerInfo PlayerInfo;

    public DeathScreen DeathScreen;

    private float velocity = 0f;               // Current Horizontal velocity  

    private float climbVelocity = 0f;          // Current Climbing velocity 

    private bool sneaking = false;             // currently sneaking?

    private bool running = false;              // currently running?

    private float horzMvmtDirection = 0f;      //1f = right, -1f = left, 0 = neutral/Both Pressed

    private bool facing = true;                //true = right, false = left

    private float climbDirection = 0f;         //1f = up, -1 = down, 0 = neutral/Both Pressed

    private bool dashReady = true, attackReady = true, vulnerable = true, parryReady = true;

    public GameObject attackPoint, soundProducer;

    public float attackRadius = 0.5f, soundRadius = 0f;

    private Collider2D[] ladders, hitEnemies, parriedAtt;

    private Collider2D interactable;

    private RaycastHit2D leftGroundHit, midGroundHit, rightGroundHit;

    public LayerMask ladderLayer, groundLayer, enemyLayer;
    
    public Vector2 boxSize;

    public float horzDamageForce, vertDamageForce;

    public float baseSoundRadius;

    public GameObject counterPrefab;
    Queue<GameObject> counterPool;

    GameObject counterProjectile;


    private float dashDistance;

    private Rigidbody2D rb2d;                  // Reference to physics component on player

    //Slope calculation variables TODO Move these variables to their proper places once slopes are fully implemented
    private bool isOnSlope;
    private bool isJumping;
    private bool hasDied = false;

    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;
    
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float slopeSideAngle;

    [SerializeField]
    private Vector2 slopeNormalPerpendicular;

    private Vector2 colliderSize;
    private CapsuleCollider2D cc2d;            // Used for slope detection

    private CircleCollider2D soundProducerCC;

    public Animator player_animator;

    private GameObject healthBar;
    private float healthBarWidth;



    // Start is called before the first frame update

    private void Start()
    {
        PlayerInfo.lossOfControl = false;
        if (PlayerInfo.tookPortal)
        {
            player_animator.SetTrigger("TeleportIn");
            
        }
        
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        soundProducerCC = soundProducer.GetComponent<CircleCollider2D>();
        cc2d = GetComponent<CapsuleCollider2D>();

        colliderSize = cc2d.size;

        PlayerInfo.currentHealth = PlayerInfo.GetMaxHealth();

        healthBar = GameObject.Find("HealthBarValue");
        healthBarWidth = healthBar.GetComponent<RectTransform>().sizeDelta.x;

   

        DeathScreen = GameObject.Find("UI").GetComponent<DeathScreen>();


        counterPool = new Queue<GameObject>();
        //adds amountToPool to Object Pool for fireballs so preformance is set
        for (int i = 0; i < 3; i++)
        {
            counterProjectile = Instantiate(counterPrefab);
            counterProjectile.SetActive(false);
            counterPool.Enqueue(counterProjectile);
        }

        player_animator.SetBool("HasSword", PlayerInfo.items[(int)ItemList.Sword]);
    }

    //Fixed Movement has to be used as the new input system
    //does not have continous actions on press/hold
    void FixedUpdate()
    {


        ladders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0f, ladderLayer);

        interactable = Physics2D.OverlapBox(transform.position, boxSize, 0f, LayerMask.GetMask("Interactable"));
        
            if (interactable != null)
            {
                transform.Find("Interact").gameObject.SetActive(true);
            }
            else
            {
                transform.Find("Interact").gameObject.SetActive(false);
            }

        



        if (transform.localScale.x == 1)
        {
            facing = true;
        }
        else
        {

            facing = false;
        }




        if (!PlayerInfo.lossOfControl)
        {
            Movement();
        }

        if (ladders.Length >= 1 && !PlayerInfo.lossOfControl)
        {
            LadderMvmt();

        }
        else
        {
            player_animator.SetBool("Climbing", false);

        }
        SlopeCheck();
            
        


        //dash cooldown turn into a coroutine
        /*if (!dashReady)
        {
            dashTimer += 1f;
            if (dashTimer >= PlayerInfo.dashCooldownPeriod)
            {
                dashReady = true;
                Debug.Log("Dash Ready");
            }
            Debug.Log(dashTimer/PlayerInfo.dashCooldownPeriod);

        }*/


        /*
        if (!vulnerable)
        {
            invincibilityTimer += 1f;

            if(invincibilityTimer >= PlayerInfo.invincibilityPeriod)
            {
                invincibilityTimer = 0;
                vulnerable = true;
            }

            if (invincibilityTimer == PlayerInfo.invincibilityPeriod / 2)
            {
                PlayerInfo.lossOfControl = false;

            }

        }*/


        if (PlayerInfo.currentHealth <= 0 && !hasDied)
        {
            PlayerInfo.lossOfControl = true;
            rb2d.mass = 1000f;
            player_animator.SetTrigger("Death");

            hasDied = true;

            player_animator.SetBool("hasDied", hasDied);


        }

        player_animator.SetFloat("MoveSpeedX", rb2d.velocity.x);
        player_animator.SetFloat("MoveSpeedY", rb2d.velocity.y);
        player_animator.SetBool("OnSlope", isOnSlope);
        PlayerInfo.SetPos(transform.position);
    }

    public void Death()
    {

        rb2d.simulated = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Time.timeScale = 0f;
        DeathScreen.Startup();
        //this.enabled = false;

    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, (colliderSize.y / 2));
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }
    
    
    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, groundLayer);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, groundLayer);

        if (slopeHitFront)
        {
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

            if (slopeSideAngle >= 60.0f)
            {
                isOnSlope = false;
            }
            else
            {
                isOnSlope = true;
            }

        }
        else if (slopeHitBack)
        {
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);

            if (slopeSideAngle >= 60.0f)
            {
                isOnSlope = false;
            }
            else
            {
                isOnSlope = true;
            }
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
        

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundLayer);
        if (hit)
        {
            slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }
            slopeDownAngleOld = slopeDownAngle;

            //debug statements to draw the normal / perpendicular of the slope
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            Debug.DrawRay(hit.point, slopeNormalPerpendicular, Color.red);
        }

        if(isOnSlope && horzMvmtDirection == 0)
        {
            rb2d.sharedMaterial = fullFriction;
        }
        else
        {
            rb2d.sharedMaterial = noFriction;
        }
    }
    
    // Movment is On or Off can change how it functions through making it increment or not
    void Movement()
    {
        //Run/Walk/Sneak

        //Run if implemented and sneak could be set for toggle rather than hold

        float baseVelocity = PlayerInfo.baseVelocity;

        float vertVelocity = rb2d.velocity.y; //float to modify in case of slope

        float sneakVelocityMultiplier = PlayerInfo.sneakVelocityMultiplier;

        float runVelocityMultiplier = PlayerInfo.runVelocityMultiplier;

        if(rb2d.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if (horzMvmtDirection == 1.0 || horzMvmtDirection == -1.0)
        {
            soundProducerCC.enabled = true;

            if (sneaking)
            {
                velocity = horzMvmtDirection * baseVelocity * sneakVelocityMultiplier;

                soundRadius = baseSoundRadius * sneakVelocityMultiplier;  
            }
            else if (running)
            {
                velocity = horzMvmtDirection * baseVelocity * runVelocityMultiplier;

                soundRadius = baseSoundRadius * runVelocityMultiplier;
            }

            else if (isOnSlope)
            {
                
                /*if(baseVelocity == 0)
                {
                    vertVelocity = 0;
                }*/
                if (sneaking)
                {
                    velocity = baseVelocity * slopeNormalPerpendicular.x * -horzMvmtDirection * sneakVelocityMultiplier;
                    vertVelocity = baseVelocity * slopeNormalPerpendicular.y * -horzMvmtDirection * sneakVelocityMultiplier;

                    soundRadius = baseSoundRadius * sneakVelocityMultiplier;
                }
                else if (running)
                {
                    velocity = baseVelocity * slopeNormalPerpendicular.x * -horzMvmtDirection * runVelocityMultiplier;
                    vertVelocity = baseVelocity * slopeNormalPerpendicular.y * -horzMvmtDirection * runVelocityMultiplier;

                    soundRadius = baseSoundRadius * runVelocityMultiplier;
                }
                else
                {
                    velocity = baseVelocity * slopeNormalPerpendicular.x * -horzMvmtDirection;
                    vertVelocity = baseVelocity * slopeNormalPerpendicular.y * -horzMvmtDirection;

                    soundRadius = baseSoundRadius;
                }
                Debug.Log("Total velocity = " + (Math.Abs(velocity) + Math.Abs(vertVelocity)));
            }

            else
            {
                velocity = horzMvmtDirection * baseVelocity;

                soundRadius = baseSoundRadius;
            }


            soundProducerCC.radius = soundRadius;

            
            rb2d.velocity = new Vector2(velocity, vertVelocity);
            
        }  

        
        //else
        //{
        //   velocity = 0;
        //}

       

    }


    //needs adjustments may need tomove where the velocity is
    //updated so gravity works properly around ladders
    void LadderMvmt()
    {
        //transform player position rather than using rigidbody2D to adjust movement


        if (climbDirection == 1.0 || climbDirection == -1.0)
        {
            rb2d.gravityScale = 0f;
            rb2d.velocity = new Vector2(0, 0);
            climbVelocity = climbDirection * PlayerInfo.baseClimbVelocity;
            transform.position = new Vector3(/*ladders[0].transform.position.x*/ transform.position.x, climbVelocity + transform.position.y, transform.position.z);
            player_animator.SetBool("Climbing", true);
        }
        

    }

    /*void Flip()
    {
        //Reverse the X scale of player to make the face opposite direction.
        facing = !facing;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }*/


    // may utilize with Animator
     public void Dash()
     {

        if (facing)
        {
            transform.position = transform.position + new Vector3(dashDistance, 0, 0);
        }
        else
        {

            transform.position = transform.position + new Vector3(-dashDistance, 0, 0);
        }

        vulnerable = true;
        rb2d.gravityScale = 1;
        PlayerInfo.lossOfControl = false;

     }


    public void Attack()
    {
        hitEnemies = Physics2D.OverlapBoxAll(attackPoint.transform.position, new Vector2(1.6f, 0.5f),0, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponentInParent<EnemyManager>().TakeDamage();
        }
    }


    public void AttackReady() { attackReady = true; }

    public void Parry()
    {


        parriedAtt = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRadius + 4, LayerMask.NameToLayer("Attack"));


        foreach (Collider2D attacks in parriedAtt)
        {

            counterProjectile.GetComponent<AttackMvmt>().EnemyInfo = attacks.GetComponent<AttackMvmt>().EnemyInfo;
            attacks.GetComponent<AttackMvmt>().RemoveAttack();

        }

        if (parriedAtt[0] != null)
        {

            player_animator.SetTrigger("Counter");


        }

    }

    public void CounterAttack()
    {

        counterProjectile.transform.position = attackPoint.transform.position;

        counterProjectile.GetComponent<AttackMvmt>().Target();
        counterProjectile.SetActive(true);

        counterPool.Enqueue(counterProjectile);

    }


    public void TouchDamage(int damage)
    {

        if (vulnerable)
        {
            PlayerInfo.currentHealth -= damage;
            PlayerInfo.currentHealth = (int)Mathf.Clamp(PlayerInfo.currentHealth, 0f, PlayerInfo.GetMaxHealth());
            UpdateHealth();

            rb2d.velocity = new Vector2(0, 0);

            if (facing)
            {

                rb2d.AddForce(new Vector2(-horzDamageForce, vertDamageForce));

            }
            else
            {

                rb2d.AddForce(new Vector2(horzDamageForce, vertDamageForce));
            }

            player_animator.SetTrigger("Hit");
            StartCoroutine(InvincibilityTimer());
            StartCoroutine(LossOfControlTimer());

        }
    }

    public void LossOfControlOn() { PlayerInfo.lossOfControl = true; }


    public void LossOfControlOff() { PlayerInfo.lossOfControl = false; }

    public void GravEnable() { rb2d.gravityScale = 1; }


    //send message - event based functions 
    //Controls where the player character is facing and whether player is actively moving
    public void OnHorzMvmt(InputValue value)
    {
        
        horzMvmtDirection = value.Get<float>();
        

        if(horzMvmtDirection == 1.0 && !facing && !PlayerInfo.lossOfControl)
        {
            transform.localScale = new Vector3(horzMvmtDirection, 1, 1);
        }
        else if(horzMvmtDirection == -1.0 && facing && !PlayerInfo.lossOfControl)
        {
            transform.localScale = new Vector3(horzMvmtDirection, 1, 1);
            //Flip();
        }
        else if (horzMvmtDirection == 0 && !PlayerInfo.lossOfControl)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            soundRadius = 0;

            soundProducerCC.radius = soundRadius;

            //soundProducerCC.enabled = false;

        }

    }

    //movement modifier
    public void OnSneak(InputValue value){sneaking = value.isPressed; }

    //movement modifier
    public void OnRun(InputValue value){ running = value.isPressed; }

    //determines direction and whether or not gravity is on or not around ladders
    public void OnClimb(InputValue value){ climbDirection = value.Get<float>(); }

    
    //Blink dash, sends a Raycast forward at a set distance
    //set the dash to the maximum length of the raycast or if it's colliding set it for the raycasthit2d.distance
    //OnDash() starts dash animation 
    //dash animation will finish the blink dash.
    public void OnDash()
    {
        Debug.Log("Dashed");

        float dashForce = PlayerInfo.dashForce;
        if (PlayerInfo.items[(int)ItemList.TurboBoots] && dashReady && !PlayerInfo.lossOfControl)
        {

            RaycastHit2D dashLine;

            if (facing)
            {
                dashLine = Physics2D.Raycast((transform.position + new Vector3(0.5f, 0f, 0f)), Vector2.right, dashForce, groundLayer | enemyLayer);
            }
            else
            {
                dashLine = Physics2D.Raycast((transform.position + new Vector3(-0.5f, 0f, 0f)), Vector2.left, dashForce, groundLayer | enemyLayer);
            }
                
            if(dashLine.collider != null)
            {
                dashDistance = dashLine.distance;

            }
            else
            {
                dashDistance = dashForce;

            }
            vulnerable = false;
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            PlayerInfo.lossOfControl = true;

            player_animator.SetTrigger("Dash");
            StartCoroutine(DashTimer());
            
        }

    }

    //sends a vertical force
    public void OnJump()
    {
         if (!isJumping)
        {
            isJumping = true;
            Debug.Log("Jumped");
            if (rb2d.velocity.y == 0 && !PlayerInfo.lossOfControl)
            {
                rb2d.gravityScale = 1f;

                leftGroundHit = Physics2D.Raycast((transform.position + new Vector3(-0.5f, 0f, 0f)), -Vector2.up, 1.2f, groundLayer);
                midGroundHit = Physics2D.Raycast(transform.position, -Vector2.up, 1.2f, groundLayer);
                rightGroundHit = Physics2D.Raycast((transform.position + new Vector3(0.5f, 0f, 0f)), -Vector2.up, 1.2f, groundLayer);

                //if grounded
                if (leftGroundHit.collider != null || midGroundHit.collider != null || rightGroundHit.collider != null)
                {

                    rb2d.AddForce(new Vector2(0f, PlayerInfo.jumpForce));

                }

            }
        }

    }

    public void OnAttack()
    {
        if (PlayerInfo.items[(int)ItemList.Sword] && attackReady && !PlayerInfo.lossOfControl)
        {
            attackReady = false;
            player_animator.SetTrigger("Attack");

        }


    }


    public void OnParry()
    {
        if (PlayerInfo.items[(int) ItemList.GreenKeycard] && parryReady && !PlayerInfo.lossOfControl)
        {

            parryReady = false;
            StartCoroutine(ParryTimer());
            player_animator.SetTrigger("Parry");
            

        }
    }

    public void OnInteract()
    {
        //requires object interacting with to have interactable layer for it to function
        Collider2D interactable = Physics2D.OverlapCircle(transform.position, 0.5f, LayerMask.GetMask("Interactable"));

        if (interactable != null)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            interactable.GetComponent<Interactable>().PreformAction();

        }

    }



  /*  private void OnTriggerStay2D(Collider2D collision)
    {

    
        if (collision.gameObject.CompareTag("Interactable"))
        {            
            transform.Find("Interact").gameObject.SetActive(true);

        }
     } */

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {

            rb2d.gravityScale = 1f;
            
        }

        /*if (collision.CompareTag("Interactable"))
        {
            transform.Find("Interact").gameObject.SetActive(false);

        }*/
            


    }

    public void UpdateHealth()
    {
        RectTransform rectTransform = healthBar.GetComponent<RectTransform>();
        float mult = ((float)PlayerInfo.currentHealth / (float)PlayerInfo.GetMaxHealth());
        Vector2 newSize = new(healthBarWidth * mult, rectTransform.sizeDelta.y);

        rectTransform.sizeDelta = newSize;
    }

    IEnumerator DashTimer()
    {
        dashReady = false;

        yield return new WaitForSeconds(PlayerInfo.dashCooldownPeriod);

        dashReady = true;


    }

    IEnumerator InvincibilityTimer()
    {

        vulnerable = false;

        yield return new WaitForSeconds(PlayerInfo.invincibilityPeriod);

        vulnerable = true;

    }

    IEnumerator LossOfControlTimer()
    {
        PlayerInfo.lossOfControl = true;

        yield return new WaitForSeconds(PlayerInfo.invincibilityPeriod / 2);

        AttackReady();
        if (!hasDied)
        PlayerInfo.lossOfControl = false;

    }

    IEnumerator ParryTimer()
    {
        

        yield return new WaitForSeconds(2);

       parryReady = true;
      
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackPoint.transform.position, new Vector3(1.6f, 0.5f, 1f));
        //Gizmos.DrawWireSphere(transform.position, soundRadius);
        //Gizmos.DrawWireCube(transform.position, Vector3.one);
        Gizmos.DrawLine(transform.position, (transform.position + new Vector3(0f,-1.2f,0f)));
        Gizmos.DrawLine((transform.position + new Vector3(-0.5f, 0f, 0f)), (transform.position + new Vector3(-0.5f, -1.2f, 0f)));
        Gizmos.DrawLine((transform.position + new Vector3(0.5f, 0f, 0f)), (transform.position + new Vector3(0.5f, -1.2f, 0f)));

    }

}
