using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    public Image barraHambre;
    public bool estaTeniendoHambre;
    public float tiempoMaxHambruna;
    

    // Numero que indica cual es el valor del hambre en esta unidad de la barra
    public float cantidadPerdidaPorRonda;

    // Cantidad de la barra de hambre que se va a disminuir
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

//    // Use this for initialization
//    void Start ()
//    {
//		
//	}
	
	// Update is called once per frame
	void Update ()
    {
        fillingBarraHambre();

        if(barraHambre.fillAmount <= 0)
        {
            GameManager.instance.gameOver();
        }
    }

    public void fillingBarraHambre()
    {
        if (estaTeniendoHambre == true)
        {
            //Debug.Log("is going down");
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
