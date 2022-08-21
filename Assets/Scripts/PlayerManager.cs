using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public  float alturaMaxima = 10f;
    public float gravity = -9.8f;
    private Rigidbody simiRb;
    public Transform target;
    public GameObject simiProj;
    
    
    
    void Start()
    {
        simiRb = simiProj.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.lanzar)
        {
            Physics.gravity = Vector3.up * gravity;
            simiRb.useGravity = true;
            simiRb.velocity = CalcularVelocidad();
            GameManager.sharedInstance.FlyingSimi();
        }
    }

    Vector3 CalcularVelocidad()
    {
        Vector3 desplazamiento = target.position- transform.position;

        float velocidadX, velodidadY, velocidadZ;
        velodidadY = Mathf.Sqrt(-2 * gravity * alturaMaxima);
        velocidadX = desplazamiento.x / ((-velodidadY / gravity) + Mathf.Sqrt(2 * (desplazamiento.y - alturaMaxima) / gravity));
        velocidadZ = desplazamiento.z / ((-velodidadY / gravity) + Mathf.Sqrt(2 * (desplazamiento.y - alturaMaxima) / gravity));

        return new Vector3(velocidadX, velodidadY, velocidadZ);
    }
}
