﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadableController : MonoBehaviour
{
    [SerializeField] private GameObject _interactGameObject;
    [SerializeField] private String _readableText;
    [SerializeField] private GameObject _readableTextGameObject;
    [SerializeField] private TMPro.TextMeshProUGUI _readableTextMeshPro;
    
    private bool _canInteract = false;
    private bool _textDisplayed = false;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_textDisplayed)
        {
            ShowReadableText();
        }
        else if (Input.GetKeyDown(KeyCode.R) && _textDisplayed)
        {
            HideReadableText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowInteract();
            EnableInteract();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideInteract();
            DisableInteract();
            HideReadableText();
        }    
    }

    private void ShowInteract()
    {
        _interactGameObject.SetActive(true);
    }

    private void HideInteract()
    {
        _interactGameObject.SetActive(false);
    }

    private void EnableInteract()
    {
        _canInteract = true;
    }

    private void DisableInteract()
    {
        _canInteract = false;
    }

    private void ShowReadableText()
    {
        _readableTextGameObject.SetActive(true);
        _readableTextMeshPro.text = _readableText;
        _textDisplayed = true;
    }

    private void HideReadableText()
    {
        _readableTextGameObject.SetActive(false);
        _readableTextMeshPro.text = "";      
        _textDisplayed = false;
    }
}