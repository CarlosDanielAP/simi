using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public enum GameState
{
    esperar,calcular,lanzar,volando,atrapar,noatrapar
}
public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.esperar;
    public GameObject Mira;
    public GameObject SimiProyectil;
    private Rigidbody SimiRB;
    private Collider SimiCollider;
    public GameObject Origen;
    public static GameManager sharedInstance;
    private PlayerControlls _playerControlls;
    public  float alturaMaxima = 10f;
    public float gravity = -9.8f;
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

    private void Start()
    {
        _playerControlls.Touch.TouchInput.started += ctx =>StartTouch(ctx);
        _playerControlls.Touch.TouchInput.canceled += ctx =>EndTouch(ctx);
        
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

    public void NotCatched()
    {
        setGameState(GameState.noatrapar);
    }
    //cambiar estados
    private void setGameState(GameState newGameState)
    {
        if (newGameState == GameState.esperar)
        {
            
        }
        else if (newGameState==GameState.calcular)
        {
            //al presionar pantalla empieza a moverse la mira
            
        }
        else if (newGameState==GameState.lanzar)
        {
            Physics.gravity = Vector3.up * gravity;
            SimiRB.useGravity = true;
            SimiRB.velocity = CalcularVelocidad();
           
        }
        
        else if (newGameState == GameState.volando)
        {
            SimiCollider.enabled = true;
        }
        else if (newGameState==GameState.atrapar)
        {
          
        }
        
        else if (newGameState == GameState.noatrapar)
        {
            
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
}
