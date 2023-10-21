using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This follows tutorial: https://www.youtube.com/watch?v=aUi9aijvpgs
[System.Serializable]
public class GameData 
{
    public int deathCount;

    //the values defined in this constructor will be the default values the game starts
    public GameData()
    {
        this.deathCount = 0;
    }
}
