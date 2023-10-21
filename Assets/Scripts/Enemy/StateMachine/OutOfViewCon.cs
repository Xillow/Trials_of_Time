using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OutOfViewCon", menuName = "ToT_AI/Conditions/Out Of View")]
public class OutOfViewCon : Condition
{
    public override bool Test(EnemyManager enemy)
    {

        return (!enemy.inView && enemy.attFin);
    }
}
