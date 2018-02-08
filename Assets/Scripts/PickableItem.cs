using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickableItem : MonoBehaviour
{
    [HideInInspector]   
    public PickableItemInfo piInfo;

    [Tooltip("Its the key name if its a locked door")]
    public string name;
    public InteractableObjectType type;
    public Sprite spriteItem;
    
    void Awake(){
      
        
            if (spriteItem == null && type != InteractableObjectType.LockedDoor)
            {
                spriteItem = GetComponent<SpriteRenderer>().sprite;
            }

            piInfo = new PickableItemInfo();
            piInfo.name = name;
            piInfo.type = type;
            piInfo.sprite = spriteItem;
        }
}

[System.Serializable]
public class PickableItemInfo : ScriptableObject
{

    public string name;
    public InteractableObjectType type;
    public Sprite sprite;
}


