using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject mapMenuUI;
    

    private void Update()
    {

        


        if (Input.GetKeyDown(KeyCode.Escape))

        { //escape button down logic

            if (isPaused)

            {
                Resume();
            }

            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // This will bring up the different panels that contain all the ui and stuff
        optionsMenuUI.SetActive(false);
        mapMenuUI.SetActive(false);


        Time.timeScale = 1f; // so that nothing is moving in the scene zwhen we pause 
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        mapMenuUI.SetActive(false);


        Time.timeScale = 0f; 
        isPaused = true;
    }


    public void OptionsTab()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        mapMenuUI.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MapTab()
    {

        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        mapMenuUI.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;

    }





    public void QuitGame()
    {



        Debug.Log("The quit Button is worlking");
        Application.Quit();

    }



    private void Dialogue() //We zill put here a the past diologue between npc and player
    {

    }


    private void QuestLogTab() //Guide the player to where we go next
    {




    }




}
