using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class AntenaTransmission : MonoBehaviour {
	public float distMedia; //Distancia para saber si se está a la distancia mínima para escuchar la transmisión
	//La transmisión se escucha clara al ser la distancia del pj a la antena menor que la distMedia
	private bool zonaCobertura;
	public GameObject PJ;
	public GameObject radio;
	public string mensaje; // mensaje de la transmisión completo
	public string mensajeRoto; // mensaje entrecortado

    public AudioClip[] radioSounds; 


	void Awake () {
		PJ = GameObject.FindGameObjectWithTag ("Player");

		//radio.SetActive (false);
	}
//	void Start(){
//		radio = GameObject.FindGameObjectWithTag ("Radio");
//		radio.SetActive (false);
//		Debug.Log (radio);
//	}

	void OnTriggerStay2D(Collider2D col){

		if (col.tag == "Player") {
			zonaCobertura = true;
			radio.GetComponent<RadioSelector> ().zonaCobertura = true;
			if(radio.activeInHierarchy)
            {
				radio.GetComponent<RadioSelector>().antenaRecibida = this.gameObject;
            }
            bool cercania = CalculoDistancia (col.transform.gameObject);
			//Debug.Log (cercania);
			if (cercania) {
				radio.GetComponent<RadioSelector>().cerca=true;
                //poder reproducir el sonido correcto sonidoactivo=...
                /*if ((this.GetComponent<AudioSource>().clip == radioSounds[0] || this.GetComponent<AudioSource>().clip == null) && radio.activeSelf)
                {
                    Debug.Log("quiere hcerlo bien");
                    this.GetComponent<AudioSource>().clip = radioSounds[1];
                    this.GetComponent<AudioSource>().Play();
                }*/

			}
            else
            {
				radio.GetComponent<RadioSelector>().cerca=false;
                //Sonido entrecortado, sonidoactivo= crrrgggg holi crgg
                /*if ((this.GetComponent<AudioSource>().clip == radioSounds[1] || this.GetComponent<AudioSource>().clip == null) && radio.activeSelf)
                {
                    Debug.Log("quiere tratar de hablar");
                    this.GetComponent<AudioSource>().clip = radioSounds[0];
                    this.GetComponent<AudioSource>().Play();
                }
*/
            }
		} else
			radio.GetComponent<RadioSelector> ().zonaCobertura = false;
	}

	//false: media //true: cerca
	bool CalculoDistancia(GameObject col){
		Vector2 fixPosition = new Vector2(this.transform.position.x,this.transform.position.y-10f);
		float dist = Vector2.Distance (fixPosition,PJ.transform.position);
		if (dist < distMedia)
			return true;
		else
			return false;
	}

    public void stopSound()
    {
        this.GetComponent<AudioSource>().Stop();
    }

}
