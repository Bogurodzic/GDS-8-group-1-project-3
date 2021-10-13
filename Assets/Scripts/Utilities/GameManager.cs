using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingletonClass<GameManager>
{

    public void Start()
    {
        
    }

    public void Update()
    {
        ListenToKeys();
    }

    private void ListenToKeys()
    {
        ListenToGameQuit();
    }

    private void ListenToGameQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } 
    }
}
