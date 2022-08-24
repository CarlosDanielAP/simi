using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class ZoneCreator : MonoBehaviour
{
    public int heigh = 10;
    public int width = 10;
    public float Spacing = 5f;
    public GameObject[,] Personas;
    public GameObject persona;
    public int numCatchers;
    //aumentar cada que llamemos desde el manager
    private int indexFila;
    //public GameObject[] catcherPersons;
    public List<int> chosens = new List<int>();
    public Transform mira;

    
  

    void Start()
    {
        Personas = new GameObject[heigh, width];
        CreateGrid();
        
    }

    public void CreateGrid()
    {
      
        for (int y = 0; y < heigh; y++)
        {
            for (int x = 0; x < width; x++)
            {
                
               Personas[y, x] = Instantiate(persona, transform.position, Quaternion.identity);
               Personas[y, x].transform.parent = transform;
               Personas[y, x].transform.localPosition = new Vector3(x * Spacing, transform.position.y, y * Spacing);
               
            }
        }
        ChooseCatchers();
    }

   
    public void ChooseCatchers()
    {
        //elegimos las filas que tendran un cachador
        //siempre incluiran la fila 0
       
        chosens.Add(0);
        
        
        //iniciamos desde uno para que sean la cantidad que se escogio en el editor
        for (int i = 1; i < numCatchers; i++)
        {
            //elegimos un numero random y si esta repetido elegimos otro
            int random = Random.Range(0, heigh);
            while (chosens.Contains(random))
            {
                random = Random.Range(0, heigh);
            }

            //catcherPersons[i] = Personas[0, i];
            chosens.Add(random); 
        }

        chosens.Sort();
        //calculamos el primer atrapador
        calcularTarget();
      
    }

    public void calcularTarget()
    {
        if (chosens.Count <= 0)
        {
            Debug.Log("vacio");
            GameManager.sharedInstance.NextZone();
        }

        else
        {
            int random = Random.Range(0, width);
                    while (Vector3.Distance(Personas[chosens[0],random].transform.position,mira.position)>10)
                    {
                        random = Random.Range(0, width);
                    }
                    GameManager.sharedInstance.Targets[0] = Personas[chosens[0], random];
                    Debug.Log(chosens[0]+"  -  " +random);
                    chosens.RemoveAt(0);
        }
        
      
        
       
    }

  
}
