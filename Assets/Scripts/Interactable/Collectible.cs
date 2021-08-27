using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private String _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private String _description;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public String GetName()
    {
        return _name;
    }

    public String GetDescription()
    {
        return _description;
    }

    public Sprite GetSprite()
    {
        return _sprite;
    }
}
