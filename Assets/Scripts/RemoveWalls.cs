using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RemoveWalls : MonoBehaviour
{
    [SerializeField]
    PlayerInfo playerInfo;

    [SerializeField]
    ItemList item;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(!playerInfo.items[(int)item]);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(!playerInfo.items[(int)item]);
    }
}
