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
    private Sprite sprite;
    
    void Awake(){
      
        
            if (sprite == null && type != InteractableObjectType.LockedDoor)
            {
                sprite = GetComponent<SpriteRenderer>().sprite;
            }

            piInfo = new PickableItemInfo();
            piInfo.name = name;
            piInfo.type = type;
            piInfo.sprite = sprite;
        }
}

[System.Serializable]
public class PickableItemInfo : ScriptableObject
{

    public string name;
    public InteractableObjectType type;
    public Sprite sprite;
}


