using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    private bool invOpened;
    public GameObject pausePanel;
    public GameObject invPanel;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        invOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        invOpened = !invOpened;

        if (invOpened)
        {
            invPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            invPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitToMain()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Title Screen");
        Time.timeScale = 1f;
    }
}
