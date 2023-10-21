using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
//using UnityEditor.SearchService;

public class ZoneSceneChange : MonoBehaviour {

    public CrossfadeController crossfade;

    /*public enum Scenes
    {
        CastleRoom1,
        BossRoom,
        Prison,
        BreakRoom,
        GreenRoom1demo,
        GreenRoom2demo,
        GreenRoom3demo,
        BlueRoom1,
        Lab1,
        Lab2,
        OldDungeon

    };*/


    public Scenes scene;

    public PlayerInfo playerInfo;
    public int nextSpawn;

    void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag("Player")) {
            playerInfo.tookPortal = false;
            playerInfo.spawnPos = nextSpawn;
            if (crossfade != null)
            {
                LoadNextLevel();
            }
            else
            {
                SceneManager.LoadScene(scene.ToString());
            }
        }

	}

    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        float duration = crossfade.startTransition();
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(scene.ToString());
    }
	
}
