using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackMvmt : MonoBehaviour
{

    public EnemyInfo EnemyInfo;
    public Vector2 hitBox;
    public float radius;
    public float disableTime;
    public float speed;


    public virtual void Target()
    {


    }

    void OnEnable()
    {

        if(disableTime > 0)
        StartCoroutine(DisableTimer());

    }


    IEnumerator DisableTimer()
    {


        yield return new WaitForSeconds(disableTime);

        gameObject.SetActive(false);

    }

    public void RemoveAttack()
    {
        gameObject.SetActive(false);

    }
}
