using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    public static PlayerInfo Instance { get; private set; }

    protected List<CollectableInfo> collectedIteams = new List<CollectableInfo> ();

    public delegate void EquipmentChamgedHandler (CollectableInfo iteam);
    public event EquipmentChamgedHandler EquipmentChanged;

    public PlayerInfo ()
    {
        Instance = this;
    }

    public void AddCollectable (CollectableInfo collectable)
    {
        collectedIteams.Add (collectable);
        EquipmentChanged (collectable);
    }

    public int HowManyCollectable (TypeOfCollectable typeOfCollectable)
    {
        int result = 0;

        foreach (CollectableInfo collectable in collectedIteams)
        {
            if (collectable.Type == typeOfCollectable)
            {
                result++;
            }
        }

        return result;
    }
}
