using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedLigthSound : MonoBehaviour
{


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PuzzleNo2")
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PuzzleNo2")
        {
            collision.gameObject.GetComponent<AudioSource>().Stop();
        }
    }
}
