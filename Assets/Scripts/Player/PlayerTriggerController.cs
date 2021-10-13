using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("NextStage"))
       { 
           other.gameObject.GetComponent<NextStage>().LoadNextStage();
       }
    }
}
