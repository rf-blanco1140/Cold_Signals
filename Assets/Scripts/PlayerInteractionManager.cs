using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    //  Indica si el personaje puede interactuar con un objeto
    [SerializeField]
    private bool canInteract;

    // Indica si el personaje puede atravezar una puerta
    [SerializeField]
    private bool canGoDoor;

    // Referencia al FadeController dentro de la escena
    private FadeController fadeController;

    // Referencia al PlayerInventoryController dentro de la escena
    private PlayerInventoryController playerInverntoryControlerReference;

    // Item con el que esta interactuando en el momento el usuario
    // cuando no esta interactuando con nada este es null
    public PickableItem currentItem;

    // Referencia al RoomDetectorContoller de la puerta con la que esta interectuando en el momento
    public RoomDetectorContoller currentRoomDetector;

    // Referencial al DoorwayLockController con el que esta interactuando en el momento
    public DoorwayLockController doorwayLockDetector;
    
    // Referencia al PlayerManager en la escena
    public PlayerManager pm;

    // Icono que aparece cuando el personaje puede interactuar con algo en sus cercanias
    public GameObject interactIcon;



    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    void Awake()
    {
        playerInverntoryControlerReference = GetComponent<PlayerInventoryController>();
        pm = FindObjectOfType<PlayerManager>();
        fadeController = FindObjectOfType<FadeController>();    
    }

    void Update()
    {
        Inputs();
        /*if (canInteract && interactIcon != null)
        {
            interactIcon.SetActive(true);
        }
        else if(!canInteract && interactIcon !=null){
            interactIcon.SetActive(false);
        }*/
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item") // entra en contacto con un iterm y se activa la capacidad de interactuar con este
        {
            PickableItem pim = other.GetComponent<PickableItem>();
            currentItem = pim;

            canInteract = true;
        }
        else if(other.tag == "RoomDetector")
        {

            if (other.gameObject.GetComponent<PickableItem>() != null)
            {
                //La puerta tiene un candado  
                currentItem = other.GetComponent<PickableItem>();
                canInteract = true;
            }
            else if (other.GetComponent<RoomDetectorContoller>() != null)
            {
                currentRoomDetector = other.GetComponent<RoomDetectorContoller>();
                canGoDoor = true;
            }
            else if (other.GetComponent<DoorwayLockController>() != null)
            {
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

    /// <summary>
    /// Metodo que maneja el recibimiento de inputs dentro del juego
    /// </summary>
    void Inputs()
    {
        // Interaccion con el boton de interactuar y un objeto interactuable
        if (canInteract && Input.GetButton("Fire1"))
        {
            if(currentItem != null)
            {
                if (currentItem.type == InteractableObjectType.Food)
                {
                    //Debug.Log("quiere comer");
                    playerInverntoryControlerReference.AddFoodRation();
                    Destroy(currentItem.gameObject);
                }
                else if (currentItem.type == InteractableObjectType.Item)
                {
                    playerInverntoryControlerReference.AddItem(currentItem);
                }
                else if (currentItem.type == InteractableObjectType.LockedDoor)
                {
                    Debug.Log("WTF");
                    if (CheckDoor())
                    {
                        currentRoomDetector = currentItem.GetComponent<RoomDetectorContoller>();
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
            else if (doorwayLockDetector != null)
            {
                if(CheckDoorway())
                {
                    canInteract = false;
                    doorwayLockDetector.DoEvents();
                }
            else
                canInteract = false;
            }
        }

        // parte que maneja el entrar a una puerta desbloqueada o que no tenia seguro
        double horizontalMove = Input.GetAxis("Horizontal");
        double verticalMove = Input.GetAxis("Vertical");

        enterDoor(horizontalMove, verticalMove);
    }

    /// <summary>
    /// Metodo que ingresa al jugador a un puerta en base a donde esta el lado por el que se entra y por donde esta entrando el jugador
    /// </summary>
    /// <param name="horizontalMove"> valor que indica si se mueve por la izq, por la der o simplemente no se mueve horizontalmente </param>
    /// <param name="verticalMove"> valor que indica si se mueve por arriba, por abajo o simplemente no se mueve verticalmente </param>
    public void enterDoor(double horizontalMove, double verticalMove)
    {
        if (pm.canMove && canGoDoor && (horizontalMove != 0 || verticalMove != 0))
        {
            DoorEntranceSide entrance = currentRoomDetector.entranceSide;

            if ((entrance == DoorEntranceSide.Up && verticalMove > 0) || (entrance == DoorEntranceSide.Down && verticalMove < 0) || (entrance == DoorEntranceSide.Left && horizontalMove < 0) || (entrance == DoorEntranceSide.Right && horizontalMove > 0))
            {
                pm.canMove = false;
                StartCoroutine(GoInDoor());
            }

        }
    }

    /// <summary>
    /// Revisa si tiene el item para abrir la puerta
    /// </summary>
    /// <returns> Retorna un bool indicando si se pudo abrir o no la puerta </returns>
    bool CheckDoor()
    {
        string keyName = currentItem.name;
        List<PickableItemInfo> currentInventory = playerInverntoryControlerReference.GetInventory();

        bool unlocked = false;

        for (int i =0; i<currentInventory.Count && !unlocked; i++)
        {
            if(keyName == currentInventory[i].name)
            {
                unlocked = true;
            }
        }
        return unlocked;
    }

    /// <summary>
    /// Revisa si tiene el item para abrir la puerta
    /// </summary>
    /// <returns> Retorna un bool indicando si se pudo abrir o no la puerta </returns>
    bool CheckDoorway()
    {
        string keyName = doorwayLockDetector.keyName;
        List<PickableItemInfo> currentInventory = playerInverntoryControlerReference.GetInventory();

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

    /// <summary>
    /// Cambia el estado de recivir frio del jugador al contrario del valor enviado como parametro
    /// </summary>
    /// <param name="destinationIsWarm"> el valor contrario del que se le va a poner al jugador </param>
    public void changeColdStatus(bool destinationIsWarm)
    {

        GameManager.instance.coldBarReference.cambiarEstadoEnfriamiento(!destinationIsWarm);
    }

    /// <summary>
    /// Moeve al jugador a la nueva area, activa el fadeOute y desactiva el area original
    /// </summary>
    /// <returns></returns>
    IEnumerator GoInDoor()
    {
        bool destinationIsWarm = currentRoomDetector.destinationIsWarm;
        changeColdStatus(destinationIsWarm);

        fadeController.FadeOut();


        yield return new WaitForSeconds(1f);
        pm.transform.position = currentRoomDetector.destinationDoor.transform.position;
        Camera.main.gameObject.transform.position = new Vector3(currentRoomDetector.destinationDoor.transform.position.x, currentRoomDetector.destinationDoor.transform.position.y, -10f);


        currentRoomDetector.HideScenes();

        yield return new WaitForSeconds(1f);
        fadeController.FadeIn();
        pm.canMove = true;
    }

    
}

/// <summary>
/// Indica que tipo de objetos interactuables hay
/// </summary>
public enum InteractableObjectType { Food, Item, LockedDoor }
