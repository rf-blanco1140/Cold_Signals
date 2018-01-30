using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {


    private Image fadeImage;

    void Awake(){
        fadeImage = GetComponent<Image>();
    }

    public void FadeIn(){
        StartCoroutine(FadeImage(true));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeImage(false));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
