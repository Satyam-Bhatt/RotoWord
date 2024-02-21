using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaltuScripts : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private bool isPause = false;

    private void Start()
    {
        _pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            isPause = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
            isPause = false;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        Time.timeScale = 1;
        isPause = false;
        _pauseMenu.SetActive(false);
    }

}
