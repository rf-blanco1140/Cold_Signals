using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceptor : MonoBehaviour {

    public GameObject handle;

    public AudioClip estatica;

    AudioClip sonido;

    AnimacionTexto texto;

    public AudioSource audioSource;

    bool reproduciendo = false;

    float limite = 0.97f;

    float margen = 5f;

    int expectedFrequency = 0;

    string mensaje = "";

    void Start () {
        audioSource.volume = 5;
        texto = FindObjectOfType<AnimacionTexto>();
        texto.gameObject.SetActive(false);
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
            if (!reproduciendo)
            {
                audioSource.Stop();
                reproduciendo = true;
                audioSource.clip = sonido;
                texto.setMessage(mensaje);
                texto.gameObject.SetActive(true);
                audioSource.PlayOneShot(sonido);
            }
            else
            {

            }
        }
        else
        {
            texto.setMessage("");
            texto.gameObject.SetActive(false);
            if (audioSource.clip == null)
            {
                audioSource.clip = estatica;
                audioSource.PlayOneShot(estatica);
            }
            else if (audioSource.clip != estatica && audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.clip = estatica;
                audioSource.PlayOneShot(estatica);
            }

            reproduciendo = false;
        }
    }

    public void actualizarFrecuencia(int frec, AudioClip elSonido, string elMensaje)
    {
        expectedFrequency = frec;
        sonido = elSonido;
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
