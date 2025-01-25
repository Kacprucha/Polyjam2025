using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    public static PlayerInfo Instance { get; private set; }

    protected List<Collectable> collectedIteams;

    public PlayerInfo ()
    {
        Instance = this;
    }

    public int HowManyCollectable (TypeOfCollectable typeOfCollectable)
    {
        int result = 0;

        foreach (Collectable collectable in collectedIteams)
        {
            if (collectable.Type == typeOfCollectable)
            {
                result++;
            }
        }

        return result;
    }
}
