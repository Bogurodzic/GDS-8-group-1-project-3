using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    [SerializeField] private String sceneName;
    [SerializeField] private Animator _transition;

    [SerializeField] private float _transitionTime = 1f;
    
    public void LoadNextStage()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }

        StartCoroutine(LoadLevel(sceneName));

        ReadableCount.value = 0;
    }

    private IEnumerator LoadLevel(string sceneName)
    {
        _transition.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
