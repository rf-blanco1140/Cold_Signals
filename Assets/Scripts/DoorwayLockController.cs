using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;


public class DoorwayLockController : MonoBehaviour {

    public string keyName;
    public UnityEvent openEvents;


    public void DoEvents(){
        openEvents.Invoke();
    }

    public void ChangeDoorSprite(Sprite newSprite){
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        sr.sprite = newSprite;
    }
}
