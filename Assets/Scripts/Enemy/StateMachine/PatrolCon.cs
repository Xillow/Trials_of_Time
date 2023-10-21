using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PatrolCon", menuName = "ToT_AI/Conditions/Patroling")]
public class PatrolCon : Condition
{
    public override bool Test(EnemyManager enemy)
    {

        return enemy.patroling;
    }
}
