using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetectorContoller : MonoBehaviour
{
    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Area del mapa en la que actualmente se esta 
    public GameObject actualArea;

    // Area del mapa a la que se quiere dirigir
    public GameObject objectiveArea;

    // Puerta objetivo por la que se va a aparecer en la otra habitacion
    public RoomDetectorContoller destinationDoor;

    // Lado de la puerta por el que se accede a esta
    public DoorEntranceSide entranceSide;

    // Indica si el destino de la puerta es caliente o le va a dar frio al personaje
    public bool destinationIsWarm = false;



    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------


    /// <summary>
    /// Esconde la escena actual y activa la escena objetivo
    /// </summary>
    public void HideScenes()
    {
        actualArea.SetActive(false);
        objectiveArea.SetActive(true);
    }

    /// <summary>
    /// Cambia el estado de recivir o no calor del personaje a su opuesto
    /// </summary>
    public void changeColdStatus()
    {
        GameManager.instance.coldBarReference.cambiarEstadoEnfriamiento(!destinationIsWarm);
    }

}

/// <summary>
/// ENUM que contiene los lados por los que se puede entrar a la puerta
/// </summary>
public enum DoorEntranceSide{ Left, Right, Up, Down}