using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
public class MenuController : MonoBehaviour {
	public GameObject tipito;
	// Use this for initialization
	public void Exit(){
		Application.Quit ();
	}
	public void Cambio(){
		SceneManager.LoadScene ("Principal");
	}

	void Start(){
		StartCoroutine (timer1());
	}
	IEnumerator timer1(){
		yield return new WaitForSeconds (10);
		tipito.SetActive (true);
		StartCoroutine (timer2());
	}
	IEnumerator timer2(){
		yield return new WaitForSeconds (2);
		tipito.SetActive (false);

	}
}
