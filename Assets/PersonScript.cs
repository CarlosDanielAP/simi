using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonScript : MonoBehaviour
{
    public Material chosenColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TheChosenOne()
    {
        this.gameObject.tag = "Target";
        this.GetComponent<Renderer>().material = chosenColor;
       
    }
}
