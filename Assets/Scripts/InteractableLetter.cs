using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLetter : MonoBehaviour {
	public GameObject Pj;
	public int id;
	void Awake(){

	}
	void OnTriggerEnter2D(Collider2D obj){
		if (obj.tag == "Player" && Input.GetKeyDown(KeyCode.Q)) {
			Pj = obj.transform.gameObject;
			//Pj.GetComponent<NarrativeManager> ().ActivarColeccionable (id);
			//GetComponent<BoxCollider2D> ().enabled = false;
			Pj.GetComponent<NarrativeManager>().MostrarColeccionable(id);
		}
	}
}
