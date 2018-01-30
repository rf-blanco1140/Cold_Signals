using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerInventoryController : MonoBehaviour {

    [SerializeField]
    private List<PickableItemInfo> inventory;

    public int foodRations;

    public InventoryUIController uiController;

    void Awake()
    {
        inventory = new List<PickableItemInfo>();
        if(uiController == null)
            uiController = FindObjectOfType<InventoryUIController>();
    }

    private void Update()
    {
        if (Input.GetKey("e"))
        {
            comer();
        }
    }

    public void AddItem(PickableItem newItem){
        PickableItemInfo i = newItem.piInfo;

        inventory.Add(i);

        Destroy(newItem.gameObject);

        uiController.AddItemUI(i);
    }

    public void AddFoodRation()
    {
        foodRations++;
        uiController.UpdateRations(foodRations);
    }

    public void comer()
    {
        if(foodRations>0)
        {
            foodRations--;
            GameManager.instance.hungerBarRefrence.alimentarEnBaseAValor();
            uiController.UpdateRations(foodRations);
        }
    }

    public List<PickableItemInfo> GetInventory(){
        return inventory;
    }

   
}

