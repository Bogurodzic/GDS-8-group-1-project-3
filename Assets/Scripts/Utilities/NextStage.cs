﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    [SerializeField] private String sceneName;
    
    public void LoadNextStage()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
        
        SceneManager.LoadScene(sceneName);
    }
}
