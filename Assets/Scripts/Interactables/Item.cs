using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : Interactable
{
    [SerializeField]    
    PlayerInfo playerInfo;

    [SerializeField]
     ItemList item;

    [SerializeField]
    int damageIncrease;

    private void Start()
    {
      

            gameObject.SetActive(!playerInfo.items[(int)item]);
      
    }

    public override void PreformAction()
    {
        base.PreformAction();

        playerInfo.items[(int)item] = true;

        playerInfo.weaponDamage += damageIncrease;

        gameObject.SetActive(false);

    }


}
