using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimacionTexto : MonoBehaviour {

    float letterTime = 0.01f;

    string message;

    Text objetoText;

	// Use this for initialization
	void Start () {
        objetoText = GetComponent<Text>();
        message = objetoText.text;
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnEnable()
    {
        objetoText.text = "";
        StartCoroutine("Typing");
    }

    void OnDisable()
    {
        StopCoroutine("Typing");
    }

    IEnumerator Typing()
    {
        char[] letras = message.ToCharArray();
        foreach (char letter in letras)
        {
            objetoText.text += letter;
            yield return new WaitForSeconds(letterTime);
        }
    }

    public void setMessage(string pMessage)
    {
        message = pMessage;
    }
}
