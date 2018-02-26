using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaAreaConReja : MonoBehaviour
{
    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    public bool estaAdentro;



    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            jugadorEntroSalio();
            Debug.Log("esta adentro: " + estaAdentro);
        }
    }

    public void jugadorEntroSalio()
    {
        if(estaAdentro == false)
        {
            estaAdentro = true;
        }
        else
        {
            estaAdentro = false;
        }
    }
	
}
