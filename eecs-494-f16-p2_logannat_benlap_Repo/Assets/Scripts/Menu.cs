using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public SpriteRenderer ui;
    public Sprite main;
    public Sprite controls;
    public Sprite select;
    public GameObject Logo;
    
    
    private bool isMainMenu = true;
    private bool isControls = false;
    // Update is called once per frame
    void Update()
    {
        var inputDevice = InputManager.ActiveDevice;
        if (isMainMenu)
        {
            if (inputDevice.AnyButton.WasPressed)
            {
                ui.sprite = controls;
                isMainMenu = false;
                isControls = true;
                Logo.SetActive(false);
            }
        }
        else if (isControls)
        {
            if (inputDevice.AnyButton.WasPressed)
            {
                ui.sprite = select;
                isMainMenu = false;
                isControls = false;
                Logo.SetActive(false);
            }
        }
        else
        {
            Logo.SetActive(false);
            if (inputDevice.Action3.WasPressed)
            {
                GameLogic.S.numPlayers = 2;
                GameLogic.S.StartMatch();
            }
            if (inputDevice.Action1.WasPressed)
            {
                GameLogic.S.numPlayers = 3;
                GameLogic.S.StartMatch();
            }
            if (inputDevice.Action2.WasPressed)
            {
                GameLogic.S.numPlayers = 4;
                GameLogic.S.StartMatch();
            }
            if (inputDevice.Action4.WasPressed)
            {
				GameLogic.S.numPlayers = 1;
				GameLogic.S.StartMatch();
                //ui.sprite = main;
                //isMainMenu = true;
            }
        }
    }
}
