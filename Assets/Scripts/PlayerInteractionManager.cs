using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{

    [SerializeField]
    private bool canInteract;

    [SerializeField]
    private bool canGoDoor;

    private FadeController fadeController;

    private PlayerInventoryController pic;

    public PickableItem currentItem;

    public RoomDetectorContoller currentRoomDetector;

    public DoorwayLockController doorwayLockDetector;
    
    public PlayerManager pm;

    public GameObject interactIcon;

    void Awake()
    {
        pic = GetComponent<PlayerInventoryController>();
        pm = FindObjectOfType<PlayerManager>();
        fadeController = FindObjectOfType<FadeController>();    
    }

    void Update()
    {
        Inputs();
        if (canInteract && interactIcon != null)
        {
            interactIcon.SetActive(true);
        }
        else if(!canInteract && interactIcon !=null){
            interactIcon.SetActive(false);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            PickableItem pim = other.GetComponent<PickableItem>();
            currentItem = pim;

            canInteract = true;
        }
        else if(other.tag == "RoomDetector"){

            if (other.gameObject.GetComponent<PickableItem>() != null) {

                //La puerta tiene un candado  
                currentItem = other.GetComponent<PickableItem>();
                canInteract = true;
            }
            else if (other.GetComponent<RoomDetectorContoller>() != null)
            {
                currentRoomDetector = other.GetComponent<RoomDetectorContoller>();
                canGoDoor = true;
            }
            else if (other.GetComponent<DoorwayLockController>() != null) {
                doorwayLockDetector = other.GetComponent<DoorwayLockController>();
                canInteract = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Item")
        {
            PickableItem pim = other.GetComponent<PickableItem>();
            if (currentItem != null && pim == currentItem)
            {
                currentItem = null;
                canInteract = false;
            }
        }

        else if (other.tag == "RoomDetector")
        {

            if (other.gameObject.GetComponent<PickableItem>() != null)
            {

                PickableItem pim = other.GetComponent<PickableItem>();
                if (currentItem != null && pim == currentItem)
                {
                    currentItem = null;
                    canInteract = false;
                }
            }
            else if(other.gameObject.GetComponent<RoomDetectorContoller>() != null)
            {
                currentRoomDetector = null;
                canGoDoor = false;
            }

            else if (other.gameObject.GetComponent<DoorwayLockController>() != null)
            {
                doorwayLockDetector = null;
                canInteract = false;
            }
        }
    }

    void Inputs()
    {
        if (canInteract && Input.GetButton("Fire1"))
        {
            if(currentItem != null){
                if (currentItem.type == InteractableObjectType.Food)
                {
                    pic.AddFoodRation();
                    Destroy(currentItem.gameObject);
                }
                else if (currentItem.type == InteractableObjectType.Item)
                {
                    pic.AddItem(currentItem);
                }
                else if (currentItem.type == InteractableObjectType.LockedDoor)
                {
                    if (CheckDoor())
                    {
                        currentRoomDetector = currentItem.GetComponent<RoomDetectorContoller>();
                        currentRoomDetector.OpenDoor();
                        canInteract = false;
                        canGoDoor = true;
                        Destroy(currentItem);
                    }
                    else
                    {
                        canInteract = false;
                        //No tienes la llave papu

                    }
                }
            }
            else if (doorwayLockDetector != null) {
            if(CheckDoorway()){
                    canInteract = false;
                    doorwayLockDetector.DoEvents();
                }
            else
                canInteract = false;
            }
        }

        double horizontalMove = Input.GetAxis("Horizontal");
        double verticalMove = Input.GetAxis("Vertical");

        if (pm.canMove && canGoDoor && (horizontalMove != 0 || verticalMove != 0)) {
            DoorEntranceSide entrance = currentRoomDetector.entranceSide;

            if ((entrance == DoorEntranceSide.Up && verticalMove > 0)|| (entrance == DoorEntranceSide.Down && verticalMove < 0)|| (entrance == DoorEntranceSide.Left && horizontalMove < 0) || (entrance == DoorEntranceSide.Right && horizontalMove > 0))
            {
                pm.canMove = false;                
                StartCoroutine(GoInDoor());
            }
            
        }
    }
    bool CheckDoor(){
        string keyName = currentItem.name;
        List<PickableItemInfo> currentInventory = pic.GetInventory();

        bool unlocked = false;

        for (int i =0; i<currentInventory.Count && !unlocked; i++){
        
            if(keyName == currentInventory[i].name){
                unlocked = true;
            }
            
        }

        return unlocked;
    }

    bool CheckDoorway()
    {
        string keyName = doorwayLockDetector.keyName;
        List<PickableItemInfo> currentInventory = pic.GetInventory();

        bool unlocked = false;

        for (int i = 0; i < currentInventory.Count && !unlocked; i++)
        {

            if (keyName == currentInventory[i].name)
            {
                unlocked = true;
            }

        }

        return unlocked;
    }

    public void changeColdStatus(bool destinationIsWarm)
    {

        GameManager.instance.coldBarReference.cambiarEstadoEnfriamiento(!destinationIsWarm);
    }

    IEnumerator GoInDoor()
    {
        bool destinationIsWarm = currentRoomDetector.destinationIsWarm;
        changeColdStatus(destinationIsWarm);

        fadeController.FadeOut();


        yield return new WaitForSeconds(1f);
        pm.transform.position = currentRoomDetector.destinationRoom.transform.position;
        Camera.main.gameObject.transform.position = new Vector3(currentRoomDetector.destinationRoom.transform.position.x, currentRoomDetector.destinationRoom.transform.position.y, -10f);


        currentRoomDetector.HideScenes();

        yield return new WaitForSeconds(1f);
        fadeController.FadeIn();
        pm.canMove = true;
    }

    
}

public enum InteractableObjectType { Food, Item, LockedDoor }
