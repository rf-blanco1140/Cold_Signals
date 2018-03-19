using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerBunkerFInal : MonoBehaviour
{

    public GameObject puertaCongelada;

    public GameObject puertaCerrada;

    public GameObject lectorActivado;



	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Si la puerta esta congelada el Lector Activado tiene el collider desactivado
    // Si se activa primero el Lector que descongelar la puerte --> Collider desactivado
    // De lo contrario se activa el lector y su collider


    public void descongelarPuerta()
    {
        lectorActivado.GetComponent<BoxCollider2D>().enabled = true;
        puertaCerrada.SetActive(true);
        puertaCongelada.SetActive(false);
    }
}
