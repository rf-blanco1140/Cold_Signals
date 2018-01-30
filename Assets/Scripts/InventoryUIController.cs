using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{

    [SerializeField]
    private int max_slots;

    private int slots_used;

    [SerializeField]
    private GameObject slotPrefab;
    private UnityEngine.UI.GridLayoutGroup slotGrid;

    public Text rationsText;

    void Awake()
    {
        slotGrid = GetComponent<UnityEngine.UI.GridLayoutGroup>();
    }

    void Start()
    {
        float panelWidth = GetComponent<RectTransform>().rect.width;

        float xCellSize = panelWidth / max_slots;
        xCellSize -= slotGrid.spacing.x;
        slotGrid.cellSize = new Vector2(xCellSize, xCellSize);
    }

    public void AddItemUI(PickableItemInfo itemInfo)
    {

        if (max_slots > slots_used)
        {
            GameObject starSlotGO = (GameObject)Instantiate(slotPrefab);
            starSlotGO.transform.SetParent(slotGrid.transform);
            starSlotGO.transform.localScale = new Vector3(1, 1, 1);

            if (itemInfo.sprite != null)
            {
                starSlotGO.transform.GetChild(0).GetComponent<Image>().sprite = itemInfo.sprite;
            }
        }
    }

    public void UpdateRations(int rations)
    {
        rationsText.text = rations + "";
    }



}
