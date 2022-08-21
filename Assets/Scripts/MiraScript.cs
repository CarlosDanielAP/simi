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
    public float zDist = 5f;

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
            zDist += lanzador.transform.position.z;
            //normalizamos la posicion final y la multiplicamos por zDist para que se pase un poco.
            limitPos = target.position-lanzador.position;
            transform.position = Vector3.MoveTowards(transform.position,limitPos.normalized*zDist, speed * Time.deltaTime);

        }
        
        
        
    }

    

   
}
