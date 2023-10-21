using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LossOfInterestCon", menuName = "ToT_AI/Conditions/Loss Of Interest")]
public class LossOfInterestCon : Condition
{
    public override bool Test(EnemyManager enemy)
    {

        return !enemy.interested;
    }
}
