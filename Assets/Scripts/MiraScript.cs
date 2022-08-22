using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class MiraScript : MonoBehaviour
{
    public Transform target;
    public Transform lanzador;
    public float speed =1.5f;
    public float zDist = 1f;
    private float finalZDist;
    private Vector3 limitPos;
    

    // Update is called once per frame
    void Update()
    {
        //obtener la posicion final.
        
        
        //girar hacia donde esta el objetivo
        //transform.forward = target.position-lanzador.position;
    
        //si estamos precionando mover
        if (GameManager.sharedInstance.currentGameState==GameState.calcular)
        {
            lanzador = GameManager.sharedInstance.Origen.transform;
            target = GameManager.sharedInstance.Targets[0].transform;
            //normalizamos la posicion final y la multiplicamos por zDist para que se pase un poco.

            if (Vector3.Distance(lanzador.position,transform.position)<zDist)
            {
                limitPos = (target.position-lanzador.position).normalized*speed;
                transform.position += limitPos * Time.deltaTime;
            }
            
            
          
            
            
           // transform.position = Vector3.MoveTowards(transform.position,target.transform.position,2);
           
           
          

        }
        
        
        
    }

    

   
}
