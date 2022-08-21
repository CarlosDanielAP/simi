using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimiScript : MonoBehaviour
{
    public Transform theZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            GameManager.sharedInstance.NotCatched();
        }

        if (collision.gameObject.CompareTag("Target"))
        {  
            collision.gameObject.tag = "Player";
            
            GameManager.sharedInstance.Catched();
            transform.position = theZone.position;
            GameManager.sharedInstance.WaitingPress();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.sharedInstance.CatchedbyPlayer();
            transform.position = theZone.position;
            GameManager.sharedInstance.WaitingPress();
        }
    }

   
}
