using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceptor : MonoBehaviour {

    public GameObject handle;

    AnimacionTexto texto;

    MusicManager musicManager;

    bool reproduciendo = false;

    float limite = 0.97f;

    float margen = 5f;

    int expectedFrequency = 0;

    string mensaje = "";

    void Start () {
        musicManager = FindObjectOfType<MusicManager>();
        texto = FindObjectOfType<AnimacionTexto>();
        texto.gameObject.SetActive(false);
        reproduciendo = false;
    }


    private void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            if (horizontalMove < 0 && handle.transform.rotation.z <= limite)
            {
                handle.transform.Rotate(new Vector3(0, 0, -horizontalMove));
            }
            else if (horizontalMove > 0 && handle.transform.rotation.z >= -limite)
            {
                handle.transform.Rotate(new Vector3(0, 0, -horizontalMove));
            }
            compararLimite();
        }
    }

    void OnDisable()
    {

    }

    void OnEnable()
    {
        handle.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void compararLimite()
    {
        if (Mathf.Abs(handle.transform.rotation.z*100 - expectedFrequency) <= margen)
        {
            if(!reproduciendo)
            {
                musicManager.reproducir();
                texto.setMessage(mensaje);
                texto.gameObject.SetActive(true);
                reproduciendo = true;
            }
        }
        else
        {
            if(reproduciendo)
            {
                texto.setMessage("");
                texto.gameObject.SetActive(false);
                musicManager.entraEnArea();
                reproduciendo = false;
            }
        }
    }

    public void actualizarFrecuencia(int frec, string elMensaje)
    {
        expectedFrequency = frec;
        mensaje = elMensaje;
    }

    public string darMensaje()
    {
        if(reproduciendo)
        {
            return mensaje;
        }
        return "";
    }
}
