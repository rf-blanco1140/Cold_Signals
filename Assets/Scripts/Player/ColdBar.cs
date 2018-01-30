using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColdBar : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    public Image barraEnfriamiento;
    public bool seEstaEnfriando;
    public float tiempoMaxEnfriamiento = 120.0f;

    // Valor minimo de enfriamiento que el jugador sufre
    public float enfriamientoBajo;
    
    // Valor normal de enfriamiento del jugador
    public float enfriameintoMedio;

    // Valor avanzado de enfriamiento del jugador
    public float enfriamientoAlto;

    // Valor de enfriamiento que se esta usando actualmente
    public float valorEnfriamientoActual;

    // Valor usado para calentar al jugador
    public float valorCalentamiento;

    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        fillingBarraTemperatura();

        if(barraEnfriamiento.fillAmount <= 0)
        {
            GameManager.instance.gameOver();
        }
    }

    public void fillingBarraTemperatura()
    {
        if (seEstaEnfriando == true)
        {
            //Debug.Log("is going down");
            barraEnfriamiento.fillAmount -= valorEnfriamientoActual / tiempoMaxEnfriamiento * Time.deltaTime;
        }
        else
        {
            barraEnfriamiento.fillAmount += valorCalentamiento / tiempoMaxEnfriamiento * Time.deltaTime;
        }
    }

    public void selectValorFrio(int idTipoFrio)
    {
        switch (idTipoFrio)
        {
            case 1:
                valorEnfriamientoActual = enfriamientoBajo;
                break;

            case 2:
                valorEnfriamientoActual = enfriameintoMedio;
                break;

            case 3:
                valorEnfriamientoActual = enfriamientoAlto;
                break;
        }
    }

    // Metodo que cambia el color de la barra dependiendo del valor de esta
    public void cambiaColorBarra()
    {

    }

    public void cambiarEstadoEnfriamiento(bool nuevoEstado)
    {
        seEstaEnfriando = nuevoEstado;
    }

}
