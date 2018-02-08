using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSignal : MonoBehaviour {

    public AudioClip sonidoCercania;

    public AudioClip sonidoAntena;

    bool inArea;

    bool closeEnough;

    public AudioSource audioSource;

    float diametroTransmision = 0;

    int expectedFrequency;

    float limite = 0.97f;

    float radioInterior = 80f;

    GameObject jugador;

    public SignalReceptor elRadio;

	// Use this for initialization
	void Start () {

        expectedFrequency = (int)(Random.Range(-limite, limite) * 100);
    }
	
	// Update is called once per frame
	void Update () {
        if (inArea)
        {
            float distancia = Mathf.Abs((jugador.transform.position - this.transform.position).magnitude);
            float volumen = diametroTransmision / (distancia*10);
            audioSource.volume = volumen;
            if (distancia < radioInterior)
            {
                elRadio.actualizarFrecuencia(expectedFrequency, sonidoAntena);
            }
            else {
                elRadio.actualizarFrecuencia(0, null);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = true;
            jugador = other.gameObject;
            if (diametroTransmision == 0)
            {
                diametroTransmision = Mathf.Abs((jugador.transform.position - this.transform.position).magnitude);
            }
            audioSource.PlayOneShot(sonidoCercania);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = false;
            jugador = null;
            audioSource.Stop();
        }
    }
    
}
