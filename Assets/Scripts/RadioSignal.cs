using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSignal : MonoBehaviour {

    public AudioClip audio;

    bool inArea;

    public AudioSource audioSource;

    float diametroTransmision = 0;

    GameObject jugador;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (inArea)
        {
            float distancia = Mathf.Abs((jugador.transform.position - this.transform.position).magnitude);
            float volumen = diametroTransmision / (distancia*10);
            audioSource.volume = volumen;
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
            audioSource.PlayOneShot(audio);
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
