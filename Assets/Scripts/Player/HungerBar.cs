using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Imagen de el filler de la barra de hambre
    public Image barraHambre;

    // Indica si el jugador esta teniendo hambre en este momento
    public bool estaTeniendoHambre;

    // Indica el tiempo maximo que el jugador pude durar sin comer
    // Es el valor que se vacia y llenass
    public float tiempoMaxHambruna;
    
    // Numero que indica la cantidad de hambre ganada en esta ronda
    // Se usa para saber en que punto disminur la barra discreta de el hambre
    public float cantidadPerdidaPorRonda;

    // Indica en que punto se va a disminuir la barra de hambre en base al contador que disminuye constantemente
    // es un valor entre 0 y 1
    public float unidadDeHambre;

    // Valor minimo de enfriamiento que el jugador sufre
    public float hambreBajo;

    // Valor normal de enfriamiento del jugador
    public float hambreMedio;

    // Valor avanzado de enfriamiento del jugador
    public float hambreAlto;

    // Valor de enfriamiento que se esta usando actualmente
    public float valorHambreActual;



    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

	// Update is called once per frame
	void Update ()
    {
        fillingBarraHambre();

        if(barraHambre.fillAmount <= 0)
        {
            GameManager.instance.gameOver();
        }
    }

    /// <summary>
    ///     Metodo que disminuye la barra de hambre/llenura del jugador
    ///     cantidadPerdidaPorRonda y unidadDeHambre van de 0 a 1
    /// </summary>
    public void fillingBarraHambre()
    {
        if (estaTeniendoHambre == true)
        {
            //Calcula la cantidad perdida hasta el momento en forma porcentual en base al valor de hambre total
            cantidadPerdidaPorRonda += valorHambreActual / tiempoMaxHambruna * Time.fixedDeltaTime;

            //Debug.Log("unidad de hambre es: " + unidadDeHambre);

            if (cantidadPerdidaPorRonda >= unidadDeHambre)
            {
                barraHambre.fillAmount -= cantidadPerdidaPorRonda;
                cantidadPerdidaPorRonda = 0;
            }
        }

    }

    /// <summary>
    /// Llena la barra de hambre  en base a un valor pasado
    /// </summary>
    /// <param name="valorAlimenticio"> Valor de la comida </param>
    public void alimentarEnBaseAValor()//float valorAlimenticio)
    {
        barraHambre.fillAmount += unidadDeHambre;
        //cantidadPerdidaPorRonda += unidadDeHambre;
    }

    public void selectHungerLevel(int idTipoHambre)
    {
        switch (idTipoHambre)
        {
            case 1:
                valorHambreActual = hambreBajo;
                break;

            case 2:
                valorHambreActual = hambreMedio;
                break;

            case 3:
                valorHambreActual = hambreAlto;
                break;
        }
    }
}
