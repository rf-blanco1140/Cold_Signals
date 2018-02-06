using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;


public class DoorwayLockController : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Nombre de la llave que abre esta puerta
    public string keyName;

    // Los eventos que se van a activar una ves se use la llave
    public UnityEvent openEvents;

    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    public void DoEvents()
    {
        openEvents.Invoke();
    }

    /// <summary>
    /// Cambia el sprite de la puerta por el pasado por aprametro
    /// </summary>
    /// <param name="newSprite"> El nuevo sprite de la puerta </param>
    public void ChangeDoorSprite(Sprite newSprite)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        sr.sprite = newSprite;
    }
}
