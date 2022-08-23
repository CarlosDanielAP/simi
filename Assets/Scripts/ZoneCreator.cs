using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCreator : MonoBehaviour
{
    public int heigh = 10;
    public int width = 10;
    public float Spacing = 5f;
    public GameObject[,] Personas;
    public GameObject persona;
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
                
               Personas[x, y] = Instantiate(persona, transform.position, Quaternion.identity);
               Personas[x, y].transform.parent = transform;
               Personas[x, y].transform.localPosition = new Vector3(x * Spacing, transform.position.y, y * Spacing);

            }
        }
    }

}
