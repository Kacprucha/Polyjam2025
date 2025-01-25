using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : InteractableElement
{
    [SerializeField] TypeOfCollectable type;
    
    [SerializeField] Sprite icon;
    [SerializeField] SpriteFader interactionKeyFader;

    protected bool isIteamInRange = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isIteamInRange && !DialogView.gameObject.activeSelf && Input.GetKeyDown (KeyCode.Q))
        {
            PlayerInfo.Instance.AddCollectable (new CollectableInfo (type, icon));
            Destroy (this.gameObject);
        }
    }

    public override void ShowInteractionKey ()
    {
        interactionKeyFader.FadeIn ();
        isIteamInRange = true;
    }

    public override void HideInteractionKey ()
    {
        interactionKeyFader.FadeOut ();
        isIteamInRange = false;
    }
}
