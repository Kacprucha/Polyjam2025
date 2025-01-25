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
            StartCoroutine (proceedDestroction ());
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

    protected IEnumerator proceedDestroction ()
    {
        this.gameObject.SetActive (false);

        yield return new WaitForEndOfFrame ();

        Destroy (this.gameObject);
    }
}
