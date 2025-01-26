using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfCollectable
{
    Invalid,
    PartOfRaport,
    PartOfTueFiles,
    ToiletPaper,
    Rice,
    Indetyficator
}

[Serializable]
public class CollectableInfo
{
    public TypeOfCollectable type;
    public Sprite icon;

    public TypeOfCollectable Type
    {
        get { return type; }
    }

    public Sprite Icon
    {
        get { return icon; }
    }

    public CollectableInfo (TypeOfCollectable type, Sprite icon)
    {
        this.type = type;
        this.icon = icon;
    }
}
