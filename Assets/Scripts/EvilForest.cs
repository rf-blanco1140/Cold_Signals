using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvilForest : MonoBehaviour
{

    public GameObject eveilEye;

    public int secondsToDie;

    private int originalTime;


	// Use this for initialization
	void Start ()
    {
        originalTime = secondsToDie;
        restartTime();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(countDownToDie());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(countDownToDie());
            restartTime();
        }
    }

    public void restartTime()
    {
        secondsToDie = originalTime;
        eveilEye.GetComponent<Image>().color = new Color(eveilEye.GetComponent<Image>().color.r, eveilEye.GetComponent<Image>().color.g, eveilEye.GetComponent<Image>().color.b, 0);
    }

    public IEnumerator countDownToDie()
    {
        while(true)
        {
            if(secondsToDie == 0)
            {
                GameManager.instance.gameOver();
            }

            yield return new WaitForSeconds(1);
            secondsToDie--;
            eveilEye.GetComponent<Image>().color = new Color(eveilEye.GetComponent<Image>().color.r, eveilEye.GetComponent<Image>().color.g, eveilEye.GetComponent<Image>().color.b, eveilEye.GetComponent<Image>().color.a+0.1f);
        }
        
    }
}
