using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerInventoryController : MonoBehaviour
{

    //-------------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------------

    // Lista de objetos recogidos 
    [SerializeField]
    private List<PickableItemInfo> inventory;

    // Cuantas raciones de comida tiene el jugador
    public int foodRations;

    // Referencia al controlador de la parte grafica del inventario
    public UIManager uiManagerReference;



    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    void Awake()
    {
        inventory = new List<PickableItemInfo>();
        if(uiManagerReference == null)
            uiManagerReference = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKey("e"))
        {
            comer();
        }
    }

    /// <summary>
    /// Agrega un item a la lista de items que tiene el jugador
    /// </summary>
    /// <param name="newItem"> el nuevo item recogido </param>
    public void AddItem(PickableItem newItem)
    {
        PickableItemInfo i = newItem.piInfo;

        inventory.Add(i);

        Destroy(newItem.gameObject);

        //if (newItem.spriteItem == null) { Debug.Log("la imagen en nula"); }
        //else { Debug.Log("la imagen es no nula"); }
        uiManagerReference.addItem(newItem.spriteItem);
    }

    /// <summary>
    /// Agrega raciones de comida al inventario
    /// </summary>
    public void AddFoodRation()
    {
        foodRations++;
        //uiController.UpdateRations(foodRations);
    }

    /// <summary>
    /// Consume una racion de comida del inventario si la tiene
    /// </summary>
    public void comer()
    {
        if(foodRations>0)
        {
            foodRations--;
            GameManager.instance.hungerBarRefrence.alimentarEnBaseAValor();
            //uiController.UpdateRations(foodRations);
        }
    }

    /// <summary>
    /// Devuelve la lista del inventario del jugador
    /// </summary>
    /// <returns> La lista del inventario del jugador </returns>
    public List<PickableItemInfo> GetInventory(){
        return inventory;
    }

   
}

