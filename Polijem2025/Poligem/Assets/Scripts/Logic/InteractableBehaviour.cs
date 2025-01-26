using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehaviour : InteractableElement
{
    [SerializeField] InfoView infoView;
    [SerializeField] string info;
    [SerializeField] SpriteFader interactionKeyFader;
    [SerializeField] CollectableInfo iteam;
    public bool ExitDoor = false;
    [SerializeField] GameObject endGameScreen;

    bool elementIsInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (elementIsInRange && !DialogView.gameObject.activeSelf && Input.GetKeyDown (KeyCode.E))
        {
            if (ExitDoor && PlayerInfo.Instance.PermitionToLeav)
            {
                endGameScreen.SetActive (true);
            }
            else if (iteam.type != TypeOfCollectable.Invalid)
            {
                infoView.SetInfo (info, iteam);
            }
            else
            {
                infoView.SetInfo (info);
            }
        }
    }

    public override void ShowInteractionKey ()
    {
        interactionKeyFader.FadeIn ();
        elementIsInRange = true;
    }

    public override void HideInteractionKey ()
    {
        interactionKeyFader.FadeOut ();
        elementIsInRange = false;
    }
}
