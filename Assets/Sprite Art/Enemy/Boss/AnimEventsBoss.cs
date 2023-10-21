using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventsBoss : MonoBehaviour
{
    public GameObject BeamPoint, Beam;

    public Animator boss_animator;

    public Transform bossStartPos, bossPlatStartPos, bossPlatform;

    public void BeamPointActive()
    {

        BeamPoint.SetActive(true);
    }


    public void BeamActive()
    {

        Beam.SetActive(true);
    }

    public void StartBossAnim()
    {

        boss_animator.SetTrigger("CSStart");
    }

    public void ChangePosition()
    {

      transform.position = bossStartPos.position;

    }

    public void MovePlatform()
    {

        bossPlatform.position = bossPlatStartPos.position;

    } 



}
