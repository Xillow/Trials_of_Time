using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUIFunctions : MonoBehaviour
{
    public enum UIPanel
    {
        MainMenu,
        Continue,
        Settings,
        Gallery,
        Credits
    }

    public UIPanel panel;

    public GameObject MainMenuPanel, ContinuePanel, SettingsPanel, GalleryPanel, CreditsPanel;

    public PlayerInfo player;

    bool isMainMenu = true;

    public void Start()
    {
        MainMenuPanel.SetActive(true);
        ContinuePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        GalleryPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void doExitGame()
    {
        Application.Quit(); 
        Debug.Log("Application.Quit() does not work in the Unity Editor.");
    }

    public void loadNewGame()
    {
        player.items[(int)ItemList.PurpleKeycard] = false;
        player.items[(int)ItemList.GreenKeycard] = false;
        player.items[(int)ItemList.BlueKeycard] = false;
        player.items[(int)ItemList.RedKeycard] = false;
        player.items[(int)ItemList.YellowKeycard] = false;
        player.items[(int)ItemList.Sword] = true;
        player.items[(int)ItemList.TurboBoots] = false;
        player.items[(int)ItemList.SwordUpgrade] = false;
        player.resetWeaponDamage();
        player.spawnPos = 0;
        player.tookPortal = false;
        SceneManager.LoadScene("CastleRoomPast");

    }
    public void loadUI()
    {
        if (isMainMenu)
        {
            // Disable Main Menu Panel
            MainMenuPanel.SetActive(false);

            // Enable panel based on button name
            string buttonName = EventSystem.current.currentSelectedGameObject.name;
            switch (buttonName)
            {
                case "ContinueButton":
                    ContinuePanel.SetActive(true);
                    break;
                case "SettingsButton":
                    SettingsPanel.SetActive(true);
                    break;
                case "GalleryButton":
                    GalleryPanel.SetActive(true);
                    break;
                case "CreditsButton":
                    CreditsPanel.SetActive(true);
                    break;
                default:
                    // If no matching button name found, go back to main menu
                    MainMenuPanel.SetActive(true);
                    isMainMenu = true;
                    break;
            }

            // Set visible panel as no longer main menu
            isMainMenu = false;
        }
        else
        {
            // Disable current panel based on button name
            string buttonName = EventSystem.current.currentSelectedGameObject.name;
            switch (buttonName)
            {
                case "ContinueReturnButton":
                    ContinuePanel.SetActive(false);
                    break;
                case "SettingsReturnButton":
                    SettingsPanel.SetActive(false);
                    break;
                case "GalleryReturnButton":
                    GalleryPanel.SetActive(false);
                    break;
                case "CreditsReturnButton":
                    CreditsPanel.SetActive(false);
                    break;
                default:
                    // If no matching button name found, go back to main menu
                    MainMenuPanel.SetActive(true);
                    isMainMenu = true;
                    break;
            }

            // Enable Main Menu Panel
            MainMenuPanel.SetActive(true);

            // Set visible panel as main menu
            isMainMenu = true;
        }
    }
}
