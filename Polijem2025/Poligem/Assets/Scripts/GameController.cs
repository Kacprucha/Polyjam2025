using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] ListElement mainRaportTask;
    [SerializeField] ListElement sicretRaportTask;
    [SerializeField] List<InventoryElement> inventorySlots;

    protected PlayerInfo playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = new PlayerInfo ();
        playerInfo.EquipmentChanged += playerEquipmentChanged;
        Debug.Log ("PlayerInfo created");
    }

    private void OnEnable ()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void playerEquipmentChanged (CollectableInfo iteam)
    {
        switch (iteam.Type)
        {
            case TypeOfCollectable.PartOfRaport:

                mainRaportTask.UpdateCounter ();

                break;

            case TypeOfCollectable.PartOfTueFiles:

                if (!sicretRaportTask.gameObject.activeSelf)
                {
                    sicretRaportTask.gameObject.SetActive (true);
                }

                sicretRaportTask.UpdateCounter ();

                break;
        }

        InventoryElement slot = getFirstFreeSlotOrSlotWithIteam (iteam.Type);

        if (slot.Empty)
        {
            slot.NewIteam (iteam.Icon, iteam.Type);
        }
        else
        {
            slot.AnotherOne ();
        }
    }

    protected InventoryElement getFirstFreeSlotOrSlotWithIteam (TypeOfCollectable type)
    {
        InventoryElement result = null;

        foreach (InventoryElement slot in inventorySlots)
        {
            if (slot.TypeInSlot == type)
            {
                result = slot;
                break;
            }
            else if (slot.Empty)
            {
                result = slot;
                break;
            }
        }

        return result;
    }
}
