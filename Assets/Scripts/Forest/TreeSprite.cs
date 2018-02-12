using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSprite : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Lista que contiene los sprites posibles que puede tener un arbol
    public Sprite[] treeSpriteOptions;



    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    // Use this for initialization
    void Start()
    {
        int treeId = Random.Range(0,2);
        Sprite treeSprite = treeSpriteOptions[treeId];
        this.GetComponent<SpriteRenderer>().sprite = treeSprite;
    }
}
