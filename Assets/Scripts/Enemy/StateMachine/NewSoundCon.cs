using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundCon", menuName = "ToT_AI/Conditions/New Sound")]
public class NewSoundCon : Condition
{
    public override bool Test(EnemyManager enemy)
    {

        return enemy.newSound;
    }
}
