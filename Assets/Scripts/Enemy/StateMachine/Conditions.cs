using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : ScriptableObject
{

    //create a child class in another file to handle the condition
    //test using components found in EnemyManager and return whether the condition is met or not
    public virtual bool Test(EnemyManager enemy)
    {
        return false;
    }


}
