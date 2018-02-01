using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvilForest : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Intancia del bosque
    public static EvilForest instance = null;

    // Imagen que cubre al jugador cuando entra a un bosque
    public GameObject eveilEye;

    // Tiempo que le quedan al jugador antes de morir por el monstruo del bosque
    public int secondsToDie;

    // Tiempo incial que va atener un jugador antes de entrar a un bosque
    private int originalTime;

    public bool dentroDelBosque;

    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        originalTime = secondsToDie;
        restartTime();
	}

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isOnForest)
        {
            StartCoroutine(countDownToDie());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(countDownToDie());
            restartTime();
        }
    }*/

    public void cambioEstadoDentroDeBosque()
    {
        if(dentroDelBosque)
        {
            Debug.Log("salir");
            dentroDelBosque = false;
            StopCoroutine(countDownToDie());
            dentroDelBosque = false;
            EvilForest.instance.restartTime();
        }
        else
        {
            Debug.Log("entrar");
            dentroDelBosque = true;
            StartCoroutine(countDownToDie());
        }
    }


    /// <summary>
    /// Metodo que reinicia el tiempo que un jugador puede pasar en el bosque al original
    /// </summary>
    public void restartTime()
    {
        secondsToDie = originalTime;
        eveilEye.GetComponent<Image>().color = new Color(eveilEye.GetComponent<Image>().color.r, eveilEye.GetComponent<Image>().color.g, eveilEye.GetComponent<Image>().color.b, 0);
    }

    /// <summary>
    /// Metodo que cuenta el tiempo hacia atras que le queda al jugador en el bosque
    /// </summary>
    /// <returns></returns>
    public IEnumerator countDownToDie()
    {
        while(dentroDelBosque)
        {
            if(secondsToDie == 0)
            {
                GameManager.instance.gameOver();
            }

            yield return new WaitForSeconds(1);
            secondsToDie--;
            eveilEye.GetComponent<Image>().color = new Color(eveilEye.GetComponent<Image>().color.r, eveilEye.GetComponent<Image>().color.g, eveilEye.GetComponent<Image>().color.b, eveilEye.GetComponent<Image>().color.a+0.1f);
        }

        restartTime();
    }
}
