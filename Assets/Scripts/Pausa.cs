using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour {
	public GameObject PanelPausa;
	private bool pause;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (!pause) {
				pause = true;
				Time.timeScale = 0f;
				PanelPausa.SetActive (true);
				StartCoroutine (timer());
			} else {
				PanelPausa.SetActive (false);
				Time.timeScale = 1;
				pause = false;
			}
		} 
	}
	IEnumerator timer(){
		yield return new WaitForSeconds (1);
	}
}

