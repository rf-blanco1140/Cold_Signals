using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderingOrder : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Indica si este objeto va renderizar al bosque sobre el jugador o no
    public bool isOverPlayer;

    // Referencia al TilemapRenderer de los arboles en el mapa
    private TilemapRenderer treesRenderer;

    // Objeto que contiene los colliders que renderizan los arboles al contrario que el actual
    public GameObject otherRenderizerDetector;


    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
        treesRenderer = GameObject.Find("TilemapArbolBajo").GetComponent<TilemapRenderer>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cambioOrdenRenderizadoArboles(collision);
        cambioColliderDeRenderizado();
    }

    /// <summary>
    /// Activa el collider contrario con el que se renderizan los arboles de manera inversa a la actual
    /// Desactiva el collider que los renderiza de la amnera ctual
    /// </summary>
    public void cambioColliderDeRenderizado()
    {
        otherRenderizerDetector.SetActive(true);
        this.transform.gameObject.SetActive(false);
    }

    /// <summary>
    /// Cambia el orden de renderizado de los arboles en el mapa
    /// </summary>
    /// <param name="collision"> Objeto con el que se va a comparar el tag para saber si se cambia el orden de renderizado </param>
    public void cambioOrdenRenderizadoArboles(Collider2D collision)
    {
        if (collision.tag == "Palyer")
        {
            if (isOverPlayer)
            {
                treesRenderer.sortingOrder = 4;

            }
            else
            {
                treesRenderer.sortingOrder = 2;
            }
        }
    }

}
