using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text ammountLabel;

    int ammount = 0;
    bool empty = true;
    TypeOfCollectable typeOfCollectable = TypeOfCollectable.Invalid;

    public bool Empty { get { return empty; } }

    public TypeOfCollectable TypeInSlot {  get { return typeOfCollectable; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewIteam (Sprite icon, TypeOfCollectable type)
    {
        typeOfCollectable = type;

        this.icon.gameObject.SetActive (true);
        this.icon.sprite = icon;
        this.icon.preserveAspect = true;

        AnotherOne ();
    }

    public void AnotherOne ()
    {
        ammount++;
        ammountLabel.text = ammount.ToString();
    }

    public void FreeSpace ()
    {
        icon.gameObject.SetActive (false);
    }
}
