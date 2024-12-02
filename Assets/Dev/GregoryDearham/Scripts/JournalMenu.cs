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

        {

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
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);



        Time.timeScale = 0f; // so that nothing is moving in the scene zwhen we pause 
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);



        Time.timeScale = 0f; 
        isPaused = false;
    }


    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);

    }

    public void Map()
    {



    }





    public void QuitGame()
    {



        Debug.Log("The quit Button is worlking");
        Application.Quit();

    }



    private void Dialogue()
    {

    }


    private void QuestLog()
    {




    }




}
