using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColdBar : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Imagen del filler de la barra de frio
    public Image barraEnfriamiento;

    // Indica si el jugador esta perdiendo calor
    public bool seEstaEnfriando;

    // Maximo tiempo que dura el jugador antes de morir de firo de manera antural
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
	
	// Update is called once per frame
	void Update ()
    {
        fillingBarraTemperatura();

        if(barraEnfriamiento.fillAmount <= 0)
        {
            GameManager.instance.gameOver();
        }
    }

    /// <summary>
    /// Metod que vacia o llena la barra de frio/calor
    /// </summary>
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

    /// <summary>
    /// Selecciona con que valor el jugador va a perder calor
    /// </summary>
    /// <param name="idTipoFrio"> identificador para saber con que valor va a perder calor </param>
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

    /// <summary>
    /// Metodo con el que se le cambia el valor al booleano que indica si el jugador tiene frio o no
    /// </summary>
    /// <param name="nuevoEstado"> El nuevo valor del parametro que indica si se tiene frio </param>
    public void cambiarEstadoEnfriamiento(bool nuevoEstado)
    {
        seEstaEnfriando = nuevoEstado;
    }

}
