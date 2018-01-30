using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NarrativeManager : MonoBehaviour {
	public Button[] coleccionables;
	public Sprite[] cartas;
	public GameObject panelCartas;
	public int cant;
	// Use this for initialization
	void Start () {
		foreach (Button boton in coleccionables) {
			boton.gameObject.SetActive (false);
		}
		if(panelCartas!=null)panelCartas.SetActive (false);
	}
	

	public void ActivarColeccionable(int idCarta){
		coleccionables [idCarta].gameObject.SetActive (true);
	}

	public void MostrarColeccionable(int idCarta){
		panelCartas.GetComponent<Image> ().sprite = cartas [idCarta];
		panelCartas.SetActive (true);
	}
}
