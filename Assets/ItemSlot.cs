using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    public Image objetcImage;

    public bool tieneObjeto;

	// Use this for initialization
	void Start ()
    {
        //objetcImage = this.GetComponentInChildren<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void agregarImagenObjeto(Sprite itemSprite)
    {
        tieneObjeto = true;

        objetcImage.sprite = itemSprite;
    }

    public void obscurecerItem()
    {
        objetcImage.color = Color.gray;
    }
}
