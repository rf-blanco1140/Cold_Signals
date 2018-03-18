using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSignal : MonoBehaviour {

    bool inArea;

    bool closeEnough;

    float diametroTransmision = 0;

    int expectedFrequency;

    float limite = 0.97f;

    float radioInterior = 80f;

    public string mensajeCortado;

    public string mensajeCompleto;

    public bool cumbion;

    GameObject jugador;

    SignalReceptor elRadio;

    MusicManager musicManager;

	// Use this for initialization
	void Start () {

        elRadio = FindObjectOfType<SignalReceptor>();
        expectedFrequency = (int)(Random.Range(-limite, limite) * 100);
        musicManager = FindObjectOfType<MusicManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (inArea)
        {
            float distancia = Mathf.Abs((jugador.transform.position - this.transform.position).magnitude);
            float volumen = diametroTransmision / (distancia*10);
            musicManager.cambiarVolumen(volumen);
            if (distancia < radioInterior)
            {
                musicManager.marcarRango(true, cumbion);
                elRadio.actualizarFrecuencia(expectedFrequency, mensajeCompleto);
            }
            else {
                elRadio.actualizarFrecuencia(expectedFrequency, mensajeCortado);
                musicManager.marcarRango(false, cumbion);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = true;
            jugador = other.gameObject;
            musicManager.entraEnArea();
            if (diametroTransmision == 0)
            {
                diametroTransmision = Mathf.Abs((jugador.transform.position - this.transform.position).magnitude);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = false;
            jugador = null;
            musicManager.saleDeArea();
            elRadio.actualizarFrecuencia(-100, "");
        }
    }
    
}
