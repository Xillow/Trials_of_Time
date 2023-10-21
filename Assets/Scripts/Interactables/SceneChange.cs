using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public enum Scenes
{
    CastleRoomPast,
    CastleRoomFuture,
    BossRoomPast1,
    BossRoomPast2,
    BossRoomFuture,
    Prison,
    BreakRoom,
    GreenRoom1demo,
    GreenRoom2demo,
    GreenRoom3demo,
    BlueRoom1,
    Lab1,
    Lab2,
    OldDungeon,
    MainMenuScene,
    ToBeContinued

};


public class SceneChange : Interactable
{
    public CrossfadeController crossfade;

    public Animator player_animator;

    public Scenes scene;

    public int nextSpawn;

    public PlayerInfo playerInfo;

    public override void PreformAction()
    {
        base.PreformAction();

        playerInfo.spawnPos = nextSpawn;
        playerInfo.tookPortal = true;        
        player_animator = FindObjectOfType<PlayerManager>().gameObject.GetComponentInChildren<Animator>();

        if (crossfade != null)
        {
            LoadNextLevel();
        }
        else
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }

    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        player_animator.SetTrigger("TeleportOut");
        float duration = crossfade.startTransition();
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(scene.ToString());
    }
}
