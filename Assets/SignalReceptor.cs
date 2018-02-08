using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceptor : MonoBehaviour {

    public GameObject handle;

    public AudioClip estatica;

    public AudioClip sonido;

    public AudioSource audioSource;

    float limite = 0.97f;

    int expectedFrequency;
	// Use this for initialization
	void Start () {
        audioSource.volume = 1;

        expectedFrequency = (int) (Random.Range(-limite, limite)*100);
        Debug.Log(expectedFrequency);

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

    private void compararLimite()
    {
        if (handle.transform.rotation.z == expectedFrequency)
        {
            Debug.Log(":v");

            audioSource.Stop();
            audioSource.PlayOneShot(sonido);
        }
        else
        {
            if (audioSource.clip != estatica && audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(estatica);
            }
        }
    }
}
