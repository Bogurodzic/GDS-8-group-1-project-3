using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Animator _crossfade;
    [SerializeField] private GameObject _tutorial;

    private bool _gamePaused = false;

    // Update is called once per frame
    void Update()
    {
        if (!_gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                _gamePaused = true;
            }
        }
        else if (_gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Unpause();
            }

            if (_tutorial.activeSelf && Input.anyKeyDown)
            {
                _tutorial.SetActive(false);
            }
        }
    }

    public void Unpause()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _gamePaused = false;
    }

    public void QuitToMenu()
    {
        StartCoroutine(LoadMenu());
    }

    private IEnumerator LoadMenu()
    {
        _crossfade.SetTrigger("Start");
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(1f);

        SceneManager.LoadScene(0);
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ShowTutorial()
    {
        _tutorial.SetActive(true);
    }
}
