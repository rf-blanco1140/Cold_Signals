using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class RadioSelector : MonoBehaviour {
	[Range(0,359.9f)]
	private float selector;

	//private float frq=0f;

	private int enigma;
	public Image knob;
	public GameObject PJ;
	public GameObject antenaRecibida;
	public Slider freq;
	public bool zonaCobertura; //true si está en área
	public bool cerca;
    public AudioClip[] radioSounds;

    void Awake(){
		PJ = GameObject.FindGameObjectWithTag ("Player");
	}
	void OnEnable () {
		if (zonaCobertura)
			enigma = (int)Random.Range (0, 359);
		else
			enigma = 5000;
		PJ.GetComponent<PlayerManager> ().enabled = false;
		//StartCoroutine (ComprobarFrecuencia ());
	}

	void OnDisable()
    {
		PJ.GetComponent<PlayerManager>().enabled = true;
        if(antenaRecibida != null)
        {
            antenaRecibida.GetComponent<AntenaTransmission>().stopSound();
        }
        //antenaRecibida.GetComponent<AntenaTransmission>().stopSound();
    }
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		freq.GetComponent<Slider> ().value +=moveHorizontal;
		knob.GetComponent<Image> ().transform.eulerAngles -= new Vector3 (0f,0f,moveHorizontal);
		if (moveHorizontal != 0f) {
			ComprobarFrecuencia ();
		}
		if(Input.GetKeyDown(KeyCode.R) && PJ.GetComponent<PlayerManager>().canRadio)
        {
            closeRadio();
            //StartCoroutine(PJ.GetComponent<PlayerManager>().waitToRadio());
        }
		
	}

	void ComprobarFrecuencia(){
		int posicion = (int)knob.GetComponent<Image> ().transform.eulerAngles.z;
		if (posicion >= enigma-5 && posicion <= enigma + 5)
        {
            if (this.GetComponent<AudioSource>().isPlaying)
            {
                //Debug.Log("intenta pausar");
                this.GetComponent<AudioSource>().Stop();//Pause();
            }
            
			if(antenaRecibida!=null)CargarMensaje ();

		} else {
            // play ruido
            if (this.GetComponent<AudioSource>().clip == radioSounds[1] || this.GetComponent<AudioSource>().clip == radioSounds[2] || this.GetComponent<AudioSource>().clip == null)
            {
                //Debug.Log("quiere hcerlo bien");
                this.GetComponent<AudioSource>().clip = radioSounds[0];
                //this.GetComponent<AudioSource>().Play();
            }
            reproducirSOnidoRadio();
            // no mostrar texto
        }
	}
	void CargarMensaje(){
		string mensaje;
		if (cerca) {
			//Audio completo
			mensaje = antenaRecibida.GetComponent<AntenaTransmission> ().mensaje;

            if (this.GetComponent<AudioSource>().clip == radioSounds[0] || this.GetComponent<AudioSource>().clip == radioSounds[1] || this.GetComponent<AudioSource>().clip == null)
            {
                Debug.Log("quiere hcerlo bien");
                this.GetComponent<AudioSource>().clip = radioSounds[2];
                //this.GetComponent<AudioSource>().Play();
            }

        } else {
            //Audio Entrecortado
            if (this.GetComponent<AudioSource>().clip == radioSounds[0] || this.GetComponent<AudioSource>().clip == radioSounds[2] || this.GetComponent<AudioSource>().clip == null)
            {
                Debug.Log("quiere hcerlo bien");
                this.GetComponent<AudioSource>().clip = radioSounds[1];
                //this.GetComponent<AudioSource>().Play();
            }
            mensaje = antenaRecibida.GetComponent<AntenaTransmission> ().mensajeRoto;
		}

        reproducirSOnidoRadio();

		PJ.GetComponent<DialogController> ().ShowDialog (mensaje);
		StartCoroutine( timer ());
	}

    public void reproducirSOnidoRadio()
    {
        if(!this.GetComponent<AudioSource>().isPlaying)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }

    public void closeRadio()
    {
        //StartCoroutine(PJ.GetComponent<PlayerManager>().waitToRadio());
        PJ.GetComponent<PlayerManager>().WaitForRadio();
        this.gameObject.SetActive(false);
    }

	IEnumerator timer(){
		yield return new WaitForSeconds (5);
		PJ.GetComponent<DialogController> ().ShowDialog ("");

	}

}
