using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public enum GameState
{
    inicioJuego,esperar,calcular,lanzar,volando,atrapar,noatrapar, atrapaPlayer,siguienteZona
}
public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.inicioJuego;
    public GameObject Mira;
    public GameObject SimiProyectil;
    private Rigidbody SimiRB;
    private Collider SimiCollider;
    public GameObject Origen;
   // public List<GameObject> Targets = new List<GameObject>();
   public GameObject[] Targets;
    public static GameManager sharedInstance;
    private PlayerControlls _playerControlls;
    public  float alturaMaxima = 10f;
    public float gravity = -9.8f;
    public ZoneCreator []zonas;
    private int zoneIndex;
    private void Awake()
    {
        _playerControlls = new PlayerControlls();
        if (sharedInstance==null)
        {
            sharedInstance=this;
        }

        SimiRB = SimiProyectil.GetComponent<Rigidbody>();
        SimiCollider = SimiProyectil.GetComponent<Collider>();
    }
    
    private void Start()
    {
        _playerControlls.Touch.TouchInput.started += ctx =>StartTouch(ctx);
        _playerControlls.Touch.TouchInput.canceled += ctx =>EndTouch(ctx);
        //Esperamos para dar tiempo al creador de zonas de crear el grid
        Invoke("StartGame",0.1f); 
       Invoke("WaitingPress",0.2f); 
       
        
    }
    private void OnEnable()
    {
        _playerControlls.Enable();
    }

    private void OnDisable()
    {
        _playerControlls.Disable();
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        //solo podemos calcular cuando el estado sea esperar asi solo se tira cuando el jugador tenga el propyectil
        if (currentGameState==GameState.esperar)
        {
            Calculate();  
        }
       
    }
    private void EndTouch(InputAction.CallbackContext context)
    {
      //solo si vengo del estado calcular puedo lanzar un simi
      if (currentGameState == GameState.calcular)
      {
          ThrowSimi();
          //despues de lanzar pasamos al estado de volar para activar el collider
          FlyingSimi();
      }
    }

   

    public void StartGame()
    {
        setGameState(GameState.inicioJuego);
    }
    public void WaitingPress()
    {
        setGameState(GameState.esperar);
    }

    public void Calculate()
    {
        setGameState(GameState.calcular);
    }
    public void ThrowSimi()
    {
        setGameState(GameState.lanzar);
    }
    public void FlyingSimi(){
        setGameState(GameState.volando);
    }

    public void Catched()
    {
        setGameState(GameState.atrapar);
    }
    public void CatchedbyPlayer()
    {
        setGameState(GameState.atrapaPlayer);
    }

    public void NotCatched()
    {
        setGameState(GameState.noatrapar);
    }

    public void NextZone()
    {
        setGameState(GameState.siguienteZona);
    }
    //cambiar estados
    private void setGameState(GameState newGameState)
    {
        if (newGameState == GameState.esperar)
        {
            Debug.Log("esperar");
        }
        
        else if (newGameState== GameState.inicioJuego)
        {
            Debug.Log("inicio");
            zonas[zoneIndex].calcularTarget(Origen.transform);
          

        }
        else if (newGameState==GameState.calcular)
        {
            //al presionar pantalla empieza a moverse la mira
            
        }
        else if (newGameState==GameState.lanzar)
        {
            Physics.gravity = Vector3.up * gravity;
            SimiRB.useGravity = true;
            SimiRB.isKinematic = false;
            SimiRB.velocity = CalcularVelocidad();
           
        }
        
        else if (newGameState == GameState.volando)
        {
            SimiCollider.enabled = true;
            
        }
        else if (newGameState==GameState.atrapar)
        {
           
            //el primer lanzador (Origen )deja de ser el Player(TAG)
            Origen.tag = "Untagged";
            //el primer target de la lista pasa a ser el Origen
            Origen = Targets[0];
            //lo sacamos de la lista porque ya no es un target
           // Targets.RemoveAt(0);
           Targets[0] = null;
            //quitamos la gravedad al simi para que no caiga al piso
            SimiRB.isKinematic = true;
            SimiRB.useGravity = false;
            
            
                //resetear la posicion de la mira hacia el nuevo jugador
            ResetPosition();
            //elegimos un nuevo target en la zona
            zonas[zoneIndex].calcularTarget(Origen.transform);
        }
        else if (newGameState == GameState.atrapaPlayer)
        {
            //si solo subio y bajo
            
            //quitamos la gravedad al simi para que no caiga al piso
            SimiRB.isKinematic = true;
            SimiRB.useGravity = false;
            
            
            //resetear la posicion de la mira hacia el nuevo jugador
            ResetPosition();
        }

        else if (newGameState == GameState.noatrapar)
        {
            
        }
        
        else if (newGameState == GameState.siguienteZona)
        {
            zoneIndex++;
            if (zoneIndex==zonas.Length)
            {
                Debug.Log("llegaste al escenario");
                
            }
            else
            zonas[zoneIndex].calcularTarget(Origen.transform);
        }

        this.currentGameState = newGameState;
    }
    //calcular donde tiramos el simi.
    Vector3 CalcularVelocidad()
    {
        Vector3 desplazamiento = Mira.transform.position - Origen.transform.position;

        float velocidadX, velodidadY, velocidadZ;
        velodidadY = Mathf.Sqrt(-2 * gravity * alturaMaxima);
        velocidadX = desplazamiento.x / ((-velodidadY / gravity) + Mathf.Sqrt(2 * (desplazamiento.y - alturaMaxima) / gravity));
        velocidadZ = desplazamiento.z / ((-velodidadY / gravity) + Mathf.Sqrt(2 * (desplazamiento.y - alturaMaxima) / gravity));

        return new Vector3(velocidadX, velodidadY, velocidadZ);
        
    }
    //resetear la posicion de la mira
    void ResetPosition()
    {
        /*GameObject PlayerPos;
        PlayerPos=GameObject.FindWithTag("Player");
        Mira.transform.position = PlayerPos.transform.position;*/
        Mira.transform.position = Origen.transform.position;
    }
}
