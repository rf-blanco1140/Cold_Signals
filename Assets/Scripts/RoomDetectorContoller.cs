using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetectorContoller : MonoBehaviour {


    public GameObject parentScene;

    public GameObject objectiveScene;

    public RoomDetectorContoller destinationRoom;

    public DoorEntranceSide entranceSide;

    public Sprite openDoorSprite;

    public bool destinationIsWarm;


    public void HideScenes(){
        parentScene.SetActive(false);
        objectiveScene.SetActive(true);
    }

    public void OpenDoor()
    {
        

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sprite = openDoorSprite;
    }
    
    public void changeColdStatus()
    {
        GameManager.instance.coldBarReference.cambiarEstadoEnfriamiento(!destinationIsWarm);
    }

}

public enum DoorEntranceSide{ Left, Right, Up, Down}