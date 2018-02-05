using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestDoors : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Instancia del manager de los bosques
    private EvilForest forestInstance;

    // El contrario a la entrada/salida actual
    public GameObject pairEntrance;


    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
        forestInstance = EvilForest.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cambioDePuerta();
        }
    }

    /// <summary>
    /// Activa el collider de la puerta contraria a esta
    /// Se considera una puerta contraria la puerta de salida si esta es la de entrada o viceversa
    /// </summary>
    public void cambioDePuerta()
    {
        //pairEntrance.GetComponent<BoxCollider2D>().enabled = true;
        //this.GetComponent<BoxCollider2D>().enabled = false;
        pairEntrance.SetActive(true);
        this.transform.parent.gameObject.SetActive(false); //gameObject.SetActive(false);
    }


}
