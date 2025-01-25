using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfCollectable
{
    Invalid,
    PartOfRaport,
    PartOfTueFiles,

}

public class Collectable : MonoBehaviour
{
    [SerializeField] TypeOfCollectable type;
    
    [SerializeField] Sprite icon;
    [SerializeField] SpriteFader interactionKeyFader;

    protected bool isIteamInRange;
    protected bool isCollected = false;

    public TypeOfCollectable Type 
    {
        get { return type; }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInteractionKey ()
    {
        interactionKeyFader.FadeIn ();
        isIteamInRange = true;
    }

    public void HideInteractionKey ()
    {
        interactionKeyFader.FadeOut ();
        isIteamInRange = false;
    }
}
