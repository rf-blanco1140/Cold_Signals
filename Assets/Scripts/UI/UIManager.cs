﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    public GameObject radio;

    public GameObject barras;

    public GameObject inventario;

    private GameObject raciones;

    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    // Use this for initialization
    void Start()
    {
        radio.SetActive(false);
        barras.SetActive(true);
        inventario = GameObject.Find("Inventario");
        //Inicializa las raciones
        raciones = GameObject.Find("RacionesUI");
        if (raciones.GetComponentInChildren<Text>() == null) { Debug.Log("raciones nulas mi capitan texto"); }
        raciones.GetComponentInChildren<Text>().text = "0";
    }
    
    /// <summary>
    /// Activa y desactiva la radio en base a su estado actual
    /// tambien deactiva las barra de calor y hambre
    /// </summary>
    public void toggleRadio()
    {
        radio.SetActive(!radio.activeInHierarchy);
        barras.SetActive(!barras.activeInHierarchy);
    }

    /// <summary>
    /// Agega el objeto pasado por parametro a uno de los espacios libres del inventario en pantalla
    /// </summary>
    /// <param name="imagenObjeto"> Sprite del objeto que se agrego al inventario </param>
    public void addItem(Sprite imagenObjeto)
    {
        bool slotLibre = false;

        for(int i=0; i < inventario.transform.childCount && !slotLibre; i++)
        {
            slotLibre = !inventario.transform.GetChild(i).GetComponent<ItemSlot>().tieneObjeto;

            if(slotLibre)
            {
                inventario.transform.GetChild(i).GetComponent<ItemSlot>().agregarImagenObjeto(imagenObjeto);
            }
        }
    }

    /// <summary>
    /// cambia el numero de raciones que tien el jugador a las pasadas por parametro
    /// </summary>
    /// <param name="numRations"></param>
    public void changeNumRations(int numRations)
    {
        raciones.GetComponentInChildren<Text>().text = "" + numRations + "";
    }

}
