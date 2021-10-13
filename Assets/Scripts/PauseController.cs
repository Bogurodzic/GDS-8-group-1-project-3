using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Animator _crossfade;

    private bool gamePaused = false;

    // Update is called once per frame
    void Update()
    {
        if (!gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                gamePaused = true;
            }
        }
        else if (gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Unpause();
            }
        }
    }

    public void Unpause()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
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
}
