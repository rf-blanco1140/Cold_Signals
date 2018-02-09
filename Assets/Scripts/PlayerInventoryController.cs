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

    // Indica si el jugador puede comer en este momento
    private bool canEat;

    //-------------------------------------------------------------------
    // Metodos
    //-------------------------------------------------------------------

    void Awake()
    {
        canEat = true;
        inventory = new List<PickableItemInfo>();
        if(uiManagerReference == null)
            uiManagerReference = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKey("e") && canEat)
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

        uiManagerReference.addItem(newItem.spriteItem);
    }

    /// <summary>
    /// Agrega raciones de comida al inventario
    /// </summary>
    public void AddFoodRation()
    {
        foodRations++;
        uiManagerReference.changeNumRations(foodRations);
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
            uiManagerReference.changeNumRations(foodRations);
            canEat = false;
            StartCoroutine(comerLag());
        }
    }

    /// <summary>
    /// Devuelve la lista del inventario del jugador
    /// </summary>
    /// <returns> La lista del inventario del jugador </returns>
    public List<PickableItemInfo> GetInventory()
    {
        return inventory;
    }

   /// <summary>
   /// Espacio de tiempo entre una accion de comer raciones y otra
   /// </summary>
   /// <returns></returns>
    public IEnumerator comerLag()
    {
        yield return new WaitForSeconds(0.5f);
        canEat = true;
    }

}

