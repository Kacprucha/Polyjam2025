using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerInfo
{
    public static PlayerInfo Instance { get; private set; }

    public bool PermitionToLeav = false;

    protected List<CollectableInfo> collectedIteams = new List<CollectableInfo> ();

    public delegate void EquipmentChamgedHandler (CollectableInfo iteam, bool isAdd = true);
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

    public void DelateCollectable (TypeOfCollectable collectable)
    {
        CollectableInfo info = getOneOfCollectables (collectable);
        
        if (collectedIteams.Contains (info))
        {
            collectedIteams.Remove (info);
            EquipmentChanged (info, false);
        }
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

    public void RestartGame ()
    {
        //yield return new WaitForEndOfFrame ();
        SceneManager.LoadScene ("Gameplay");
    }

    private CollectableInfo getOneOfCollectables (TypeOfCollectable collectable)
    {
        CollectableInfo result = null;

        result = collectedIteams.Find ((x) => x.Type == collectable);

        return result;
    }
}
