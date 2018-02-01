using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectForest : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // indica si el jugador esta en el bosque
    public bool dentroDelBosque;


    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Forest")
        {
            EvilForest.instance.cambioEstadoDentroDeBosque();
        }
    }
}
