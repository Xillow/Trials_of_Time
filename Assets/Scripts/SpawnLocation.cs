using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
   

    public List<Transform> spawnPoints = new();
    public PlayerInfo playerInfo;
    GameObject player;
    GameObject sceneCamera;

    [Header("Gizmos parameters")]
    public Color pointsColor = Color.green;
    public float pointSize = 0.875f;
    



    void Start()
    {
        player = FindObjectOfType<PlayerManager>().gameObject;
        sceneCamera = FindObjectOfType<Camera>().gameObject;

        player.transform.position = spawnPoints[playerInfo.spawnPos].position;
        player.transform.localScale = spawnPoints[playerInfo.spawnPos].localScale;
        sceneCamera.transform.position = spawnPoints[playerInfo.spawnPos].position + new Vector3(0,0,-10);

    }



    private void OnDrawGizmos()
    {
        if (spawnPoints.Count == 0)
            return;
        for (int i = spawnPoints.Count - 1; i >= 0; i--)
        {
            if (i == -1 || spawnPoints[i] == null)
                return;

            Gizmos.color = pointsColor;
            Gizmos.DrawSphere(spawnPoints[i].position, pointSize);

            if (spawnPoints.Count == 1 || i == 0)
                return;


        }
    }

}
