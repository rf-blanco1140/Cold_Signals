using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaAreaConReja : MonoBehaviour
{
    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    public bool estaAdentro;

    public bool esDentroDeLaReja;

    public GameObject pairEntrance;

    public GameObject thisRejaColliders;

    private GameObject ladoDer;

    private GameObject ladoIzq;

    private GameObject ladoSup;

    private GameObject ladoInfIzq;

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
