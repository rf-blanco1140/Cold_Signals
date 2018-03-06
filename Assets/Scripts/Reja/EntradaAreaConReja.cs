using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaAreaConReja : MonoBehaviour
{
    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Indica si esta dentro de la reja
    public bool estaAdentro;

    // Indica si el script se esta usando para dentro o para afuera de la reja
    public bool esDentroDeLaReja;

    // Objeto que contiene el comportamiento de renderizado y colliders de la reja opuesto al de este objeto
    public GameObject pairEntrance;

    // Los colliders usados para el comportamiento actual de la reja
    public GameObject thisRejaColliders;

    // Objeto que contiene todos los sprites del lado derecho de la reja
    private GameObject ladoDer;

    // Objeto que contiene todos los sprites del lado izquierdo de la reja
    private GameObject ladoIzq;

    // Objeto que contiene todos los sprites del lado superior de la reja
    private GameObject ladoSup;

    // Objeto que contiene todos los sprites del lado inferior izquierdo de la reja
    private GameObject ladoInfIzq;

    // Objeto que contiene todos los sprites del lado inferior derecho de la reja
    private GameObject ladoInfDer;


    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    private void Start()
    {
        ladoDer = GameObject.Find("ladoDerecho");
        ladoIzq = GameObject.Find("ladoIzquierdo");
        ladoSup = GameObject.Find("LadoSuperior");
        ladoInfIzq = GameObject.Find("BajaIzq");
        ladoInfDer = GameObject.Find("BajaDer");

        layerFueraDeReja();

        if(esDentroDeLaReja)
        {
            thisRejaColliders.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cambioDePuerta();
            jugadorEntroSalio();
        }
    }

    /// <summary>
    /// Metodo que cambia las variables para saber que esta fuera de la reja
    /// Adicionalmente activa y descativa los collides correspondientes para que
    /// el jugador parezaca dentro o fuera de la reja
    /// </summary>
    public void jugadorEntroSalio()
    {
        if(estaAdentro == false)
        {
            estaAdentro = true;
            layerDentroReja();
            if(esDentroDeLaReja)
            {
                thisRejaColliders.SetActive(true);
                pairEntrance.GetComponent<EntradaAreaConReja>().thisRejaColliders.SetActive(false);
            }
        }
        else
        {
            estaAdentro = false;
            layerFueraDeReja();
            if (!esDentroDeLaReja)
            {
                thisRejaColliders.SetActive(true);
                pairEntrance.GetComponent<EntradaAreaConReja>().thisRejaColliders.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Activa el collider de la puerta contraria a esta
    /// Se considera una puerta contraria la puerta de salida si esta es la de entrada o viceversa
    /// </summary>
    public void cambioDePuerta()
    {
        pairEntrance.SetActive(true);
        this.transform.gameObject.SetActive(false);
    }

    /// <summary>
    /// Cambia el layer de las rejas para que parezca que el personaje esta fuera de la reja
    /// </summary>
    public void layerFueraDeReja()
    {
        for(int i =0; i<ladoInfIzq.transform.childCount; i++)
        {
            GameObject esteHijo = ladoInfIzq.transform.GetChild(i).gameObject;
            esteHijo.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }

        for (int i = 0; i < ladoInfDer.transform.childCount; i++)
        {
            GameObject esteHijo = ladoInfDer.transform.GetChild(i).gameObject;
            esteHijo.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }

        for (int i = 0; i < ladoSup.transform.childCount; i++)
        {
            GameObject esteHijo = ladoSup.transform.GetChild(i).gameObject;
            esteHijo.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
    }

    /// <summary>
    /// Cambia el layer de las rejas para que parezca que el personaje esta dentro de la reja
    /// </summary>
    public void layerDentroReja()
    {
        for (int i = 0; i < ladoInfIzq.transform.childCount; i++)
        {
            GameObject esteHijo = ladoInfIzq.transform.GetChild(i).gameObject;
            esteHijo.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }

        for (int i = 0; i < ladoInfDer.transform.childCount; i++)
        {
            GameObject esteHijo = ladoInfDer.transform.GetChild(i).gameObject;
            esteHijo.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }

        for (int i = 0; i < ladoSup.transform.childCount; i++)
        {
            GameObject esteHijo = ladoSup.transform.GetChild(i).gameObject;
            esteHijo.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

}
