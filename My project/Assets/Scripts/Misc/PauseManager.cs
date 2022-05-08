using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;  //if the game is paused
    private bool invOpened; //if the inventory is opened
    public GameObject pausePanel;   //the pause menu
    public GameObject invPanel; //the inventory menu

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        invOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the pause or inventoy button is pressed
        if (Input.GetButtonDown("pause"))
        {
            Resume();
        }
        else if (Input.GetButtonDown("inventory")) {
            OpenInv();
        }
    }

    public void OpenInv()
    {
        //changes the inventory to open or closed, depending on the current state
        invOpened = !invOpened;

        if (invOpened)
        {
            //pauses the game and openes the inventory menu
            invPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            //closes the inventory menu and resumes the game
            invPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        //changes the pause menu to open or closed, depending on the current state
        isPaused = !isPaused;

        if (isPaused)
        {
            //pauses the games and opens up the menu
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            //resumes the game and closes the menu
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitToMain()
    {
        //saves the game and opens up the title screen
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Title Screen");
        Time.timeScale = 1f;
    }
}
