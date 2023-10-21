using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAnimEvents : MonoBehaviour
{
    [SerializeField]
    Animator player_animator;

    public void BeamHit()
    {
        player_animator.SetTrigger("BeamHit");

    }


}
