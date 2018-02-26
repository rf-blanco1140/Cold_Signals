using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaReja : MonoBehaviour
{
    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Intancia del bosque
    public static AreaReja instance = null;





    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
