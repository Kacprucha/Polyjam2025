using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] DialogView dialogView;
    [SerializeField] ListElement mainRaportTask;
    [SerializeField] ListElement sicretRaportTask;
    [SerializeField] List<InventoryElement> inventorySlots;

    [SerializeField] DialogInfo dialogInfo;
    [SerializeField] string name;
    [SerializeField] Sprite boss;
    [SerializeField] Sprite back;

    protected PlayerInfo playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = new PlayerInfo ();
        playerInfo.EquipmentChanged += playerEquipmentChanged;
        Debug.Log ("PlayerInfo created");

        dialogView.Show (dialogInfo, name, back, boss);
    }

    private void OnEnable ()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void playerEquipmentChanged (CollectableInfo iteam, bool isAdded)
    {
        if (isAdded)
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
        else
        {
            InventoryElement slot = getFirstFreeSlotOrSlotWithIteam (iteam.Type);
            slot.RemoveIteam ();
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
