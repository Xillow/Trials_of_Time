using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EnemyManager : MonoBehaviour
{
    public EnemyInfo EnemyInfo;

    public PlayerInfo PlayerInfo;
    
    private Collider2D player;
    
    public bool interested, newSound, inView, patroling, attFin;

    public GameObject target, body, attackPoint;
    
    public Vector2 hitBox;

    [HideInInspector]
    public Vector2 soundPosition;

    private Vector2 force;

    private Rigidbody2D rb2d;
        
    [SerializeField]
    private int currentHealth;

    public Animator enemy_animator;


    public GameObject attackPrefab;
    Queue<GameObject> attackPool;

    GameObject attackInstance;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = EnemyInfo.GetMaxHealth();
        rb2d = GetComponentInChildren<Rigidbody2D>();
    }

    private void Awake()
    {
        attackPool = new Queue<GameObject>();
        //adds amountToPool to Object Pool for fireballs so preformance is set
        for (int i = 0; i < EnemyInfo.projectileCount; i++)
        {
            attackInstance = Instantiate(attackPrefab);
            attackInstance.SetActive(false);
            attackPool.Enqueue(attackInstance);
        }

    }


    private void FixedUpdate()
    {
        player = Physics2D.OverlapBox(body.transform.position, hitBox, 0f, LayerMask.GetMask("Player"));

        if (player != null)
        {
            player.GetComponent<PlayerManager>().TouchDamage(EnemyInfo.GetBodyDamage());
          
        }

        enemy_animator.SetBool("inView", inView);
        enemy_animator.SetFloat("MoveSpeedX", rb2d.velocity.x);

        if (currentHealth <= 0)
        {
            enemy_animator.SetBool("Death", true);


        }

        EnemyInfo.currPos = transform;

    }

    public void TakeDamage()
    {
        currentHealth -= PlayerInfo.weaponDamage - EnemyInfo.defense;

        GetComponentInChildren<SpriteRenderer>().color = Color.black;

        StartCoroutine(HitFlash());


    }

    public void TakeDamageCounter(int damage)
    {
        currentHealth -= PlayerInfo.weaponDamage + damage - EnemyInfo.defense;

        GetComponentInChildren<SpriteRenderer>().color = Color.black;

        StartCoroutine(HitFlash());


    }


    public void EnemyDeath()
    {
        this.gameObject.SetActive(false);

    }

    public void MoveEnemy(Vector2 direction)
    {
        force = EnemyInfo.GetMoveSpeed() * Time.deltaTime * direction;

        rb2d.AddForce(force);

        if(rb2d.velocity.x > 0f) {
            Vector2 theScale = body.transform.localScale;
            theScale.x = Mathf.Abs(body.transform.localScale.x);
            body.transform.localScale = theScale;
        
        }else if(rb2d.velocity.x < 0f){

            Vector2 theScale = body.transform.localScale;
            theScale.x = Mathf.Abs(body.transform.localScale.x) * -1;
            body.transform.localScale = theScale;
        }

    }

    public void Attack()
    {
        attFin = false;
        attackInstance = attackPool.Dequeue();
        attackInstance.transform.position = attackPoint.transform.position;
        attackInstance.transform.localScale = body.transform.localScale;
        attackInstance.GetComponent<AttackMvmt>().EnemyInfo = EnemyInfo;
        attackInstance.GetComponent<AttackMvmt>().Target();
        attackInstance.GetComponent<AttackMvmt>().speed = EnemyInfo.projectileSpeed;
        attackInstance.SetActive(true);

        attackPool.Enqueue(attackInstance);



    }

    public void AttackFin()
    {

        StartCoroutine(AttackTimer());

    }


    IEnumerator AttackTimer()
    {

        yield return new WaitForSeconds(EnemyInfo.attackRate);
        attFin = true;
    }

    IEnumerator HitFlash()
    {

        yield return new WaitForSeconds(0.3f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;

    }


}
