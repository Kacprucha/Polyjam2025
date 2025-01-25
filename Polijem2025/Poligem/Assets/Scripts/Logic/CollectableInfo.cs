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

public class CollectableInfo
{
    TypeOfCollectable type;
    Sprite icon;

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
