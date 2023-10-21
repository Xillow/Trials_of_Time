using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{

    [SerializeField]
    PlayerInfo playerInfo;

    [SerializeField]
    ItemList item;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(!playerInfo.items[(int)item]);
    }

    private void Update()
    {
        transform.GetChild(0).gameObject.SetActive(playerInfo.items[(int)item]);
    }
}
