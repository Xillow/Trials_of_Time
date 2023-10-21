using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimEventsPlayer : MonoBehaviour
{
    public CrossfadeController crossfade;
    public Scenes scene;
    public PlayerInfo playerInfo;
    public int nextSpawn;   

    public PlayerManager PlayerManager;
    // Start is called before the first frame update
    public void Attack()
    {
        PlayerManager.Attack();

    }

    public void AttackReady()
    {

        PlayerManager.AttackReady();
    }

    public void Death()
    {

        PlayerManager.Death(); 

    }

    public void Dash()
    {
        PlayerManager.Dash();
    }

    public void LossOfControlOn()
    {
        PlayerManager.LossOfControlOn();

    }

    public void LossOfControlOff()
    {
        PlayerManager.LossOfControlOff();

    }

    public void Parry()
    {

        PlayerManager.Parry();

    }

    public void CounterAttack()
    {
        PlayerManager.CounterAttack();
    }


    public void ForcedTP()
    {
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

    public void GravEnable()
    {
        PlayerManager.GravEnable();


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
