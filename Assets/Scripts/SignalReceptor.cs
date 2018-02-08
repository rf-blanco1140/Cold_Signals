using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceptor : MonoBehaviour {

    public GameObject handle;

    public AudioClip estatica;

    AudioClip sonido;

    public AudioSource audioSource;

    bool reproduciendo = false;

    float limite = 0.97f;

    float margen = 3f;

    int expectedFrequency = 0;

    void Start () {
        audioSource.volume = 1;
    }


    private void FixedUpdate()
    {
        if (gameObject.activeInHierarchy && expectedFrequency != 0)
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
                audioSource.PlayOneShot(sonido);
            }
            else
            {

            }
        }
        else
        {
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

    public void actualizarFrecuencia(int frec, AudioClip elSonido)
    {
        expectedFrequency = frec;
        sonido = elSonido;
    }
}
