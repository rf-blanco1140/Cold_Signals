using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Instancia del GameManager
    public static GameManager instance = null;

    // Numero de dia en el que va el juagdor
    public int numeroDeDia;

    /* Id de la franja horaria en la que se esta en el dia
    1: Amanecer
    2: Medio dia
    3: Atardecer
    4: Noche
    */
    public int idFranjaHoraria;

    // Timmer usado para saber la hora del dia en la que se esta
    public float reloj;

    // Duracion en tiempo real del dia
    public float duracionRealDia;

    // Booleano usado en corutina para saber si detiene o no el paso del tiempo
    public bool mismoDia;

    // Booleano que indica si hay tormenta o no
    public bool hayTormenta;

    // Referencia a la luz que funciona como el sol en el juego
    public Light sol;

    public GameObject gameOverScreen;

    public GameObject player;

    public ColdBar coldBarReference;

    public HungerBar hungerBarRefrence;



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
        reloj = duracionRealDia;
        StartCoroutine(pasoDelTiempo());
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(reloj);
	}

    

    // Metodo que maneja el paso del tiempo de cada dia
    public IEnumerator pasoDelTiempo()
    {
        while(mismoDia)
        {
            reloj--;
            if(reloj == 0)
            {
                mismoDia = false;
            }

            cambioFranjaHoraria();

            yield return new WaitForSeconds(1);
        }
        cambiarNumeroDia();
    }

    

    // Metodo que cambia la franja horaria
    public void cambioFranjaHoraria()
    {
        if (reloj <= duracionRealDia * 0.25)
        {
            idFranjaHoraria = 4;
            sol.intensity = 0.1f;

        }
        else if (reloj <= duracionRealDia * 0.5)
        {
            idFranjaHoraria = 3;
            sol.intensity = 0.5f;
        }
        else if (reloj <= duracionRealDia * 0.75)
        {
            idFranjaHoraria = 2;
            sol.intensity = 1f;
        }
        else
        {
            idFranjaHoraria = 1;
            sol.intensity = 1;
        }
    }

    // Metodo que cambia de dia
    public void cambiarNumeroDia()
    {
        numeroDeDia++;
        reloj = duracionRealDia;
        mismoDia = true;
        StartCoroutine(pasoDelTiempo());
    }

    // Metodo que pasa al dia siguiente
    public void pasarDiaSiguiente()
    {
        numeroDeDia++;
        reloj = duracionRealDia;
        mismoDia = true;
    }

    // Metodo que cambia visualmente el estado del mundo en base al momento horario

    // Metodo que define el final en base al dia

    // Metodo que actiav el game over
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        player.SetActive(false);
    }

    // Metodo que reinicia el juego
    public void restartGame()
    {
        numeroDeDia = 0;
        reloj = duracionRealDia;
        mismoDia = true;
        StartCoroutine(pasoDelTiempo());
        // Poner jugador en el area inicial
        // Limpiar el item bag
    }

}
