using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    public GameObject radio;

    public GameObject barras;


    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    // Use this for initialization
    void Start () {
        radio.SetActive(false);
        barras.SetActive(true);
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
}
