using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLock : MonoBehaviour
{
    [SerializeField] int targetFPS = 60;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }

    private void Update()
    {
        if (Application.targetFrameRate != targetFPS)
        {
            Application.targetFrameRate = targetFPS;
        }
    }
}
