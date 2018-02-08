using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject radio;

    public GameObject barras;
	// Use this for initialization
	void Start () {
        radio.SetActive(false);
        barras.SetActive(true);
    }
    
    public void toggleRadio()
    {
        radio.SetActive(!radio.activeInHierarchy);
        barras.SetActive(!barras.activeInHierarchy);
    }
}
