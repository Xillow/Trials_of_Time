using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InViewCon", menuName = "ToT_AI/Conditions/In View")]
public class InViewCon : Condition
{
    public override bool Test(EnemyManager enemy)
    {

        return enemy.inView;
    }
}