using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private NextStage _nextStage;
    [SerializeField] private Animator _crossfade;
    [SerializeField] private Animator _storyScreen;
    [SerializeField] private GameObject _tutorialScreen;

    [SerializeField] private string _startText;
    [SerializeField] private string _loadingText = "Loading...";

    [SerializeField] private TextMeshProUGUI _creditsText;
    [SerializeField] private TextMeshProUGUI _fakeLoadingText;
    [SerializeField] private GameObject _fakeLoadingIcon;

    private bool _canStartGame = false;
    private bool _submenuOpen = false;

    private void Update()
    {

        if (_canStartGame)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(1);
            }
        }

        if (_submenuOpen)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
                _submenuOpen = false;
            }
        }
    }

    public void StartFakeLoading()
    {
        StartCoroutine(FakeLoadingScreen());
    }

    private IEnumerator FakeLoadingScreen()
    {
        _crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        _tutorialScreen.SetActive(true);
        _fakeLoadingIcon.SetActive(true);
        _fakeLoadingText.text = _loadingText;
        _fakeLoadingText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        _fakeLoadingIcon.SetActive(false);
        _fakeLoadingText.text = _startText;
        
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
        _storyScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        _canStartGame = true;

    }

    public void StartControlsMenu()
    {
        StartCoroutine(OpenControlsMenu());
    }

    private IEnumerator OpenControlsMenu()
    {
        _crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        _fakeLoadingText.text = _startText;
        _fakeLoadingText.gameObject.SetActive(true);
        _tutorialScreen.SetActive(true);
        _submenuOpen = true;
    }

    public void StartCreditsMenu()
    {
        StartCoroutine(OpenCreditsMenu());
    }

    private IEnumerator OpenCreditsMenu()
    {
        _crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        _fakeLoadingText.text = _startText;
        _fakeLoadingText.gameObject.SetActive(true);
        _creditsText.gameObject.SetActive(true);
        _submenuOpen = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
