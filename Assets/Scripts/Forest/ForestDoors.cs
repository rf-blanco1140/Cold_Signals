using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestDoors : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Indica si este es una entarda o una salida al bosque
    public bool estaEntrando;

    // Instancia del manager de los bosques
    private EvilForest forestInstance;



    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    // Use this for initialization
    void Start ()
    {
        forestInstance = EvilForest.instance;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (estaEntrando)
            {
                forestInstance.dentroDelBosque = true;
                StartCoroutine(forestInstance.countDownToDie());
                //Debug.Log("Entro al bosquesito");
            }
            else
            {
                //StopCoroutine(forestInstance.countDownToDie());
                forestInstance.dentroDelBosque = false;
                forestInstance.restartTime();
                //Debug.Log("Quiso salir del bosquesito");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (estaEntrando)
            {
                forestInstance.dentroDelBosque = true;
                StartCoroutine(forestInstance.countDownToDie());
                //Debug.Log("Entro al bosquesito");
            }
            else
            {
                //StopCoroutine(forestInstance.countDownToDie());
                forestInstance.dentroDelBosque = false;
                forestInstance.restartTime();
                //Debug.Log("Quiso salir del bosquesito");
            }
        }
    }
}
