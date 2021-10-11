using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private NextStage _nextStage;
    [SerializeField] private Animator _crossfade;
    [SerializeField] private GameObject _tutorialScreen;

    [SerializeField] private string _startText;

    [SerializeField] private TextMeshProUGUI _fakeLoadingText;
    [SerializeField] private GameObject _fakeLoadingIcon;

    private bool _canStartGame = false;

    private void Start()
    {
        _fakeLoadingText.text = "Wczytywanie...";
    }

    private void Update()
    {
        if (_canStartGame)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(1);
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

        yield return new WaitForSeconds(4f);

        _fakeLoadingIcon.SetActive(false);
        _fakeLoadingText.text = _startText;
        _canStartGame = true;
    }

}
