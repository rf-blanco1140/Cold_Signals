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

    public GameObject inventario;


    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    // Use this for initialization
    void Start () {
        radio.SetActive(false);
        barras.SetActive(true);
        inventario = GameObject.Find("Inventario");
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


    public void addItem(Sprite imagenObjeto)
    {
        //if (imagenObjeto == null) { Debug.Log("la imagen en nula"); }
        //else { Debug.Log("la imagen es no nula"); }

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
}
