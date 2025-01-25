using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviour : InteractableElement
{
    [SerializeField] string npcName;

    [SerializeField] SpriteFader interactionKeyFader;

    [SerializeField] string corelatedDialog;
    [SerializeField] Sprite corelatedBackGround;
    [SerializeField] Sprite corelatedPortret;

    protected bool isPlayerInRange = false;

    private void Update ()
    {
        if (isPlayerInRange && !DialogView.gameObject.activeSelf && Input.GetKeyDown (KeyCode.E))
        {
            DialogView.Show (corelatedDialog, npcName, corelatedBackGround, corelatedPortret);
        }
    }

    public override void ShowInteractionKey ()
    {
        interactionKeyFader.FadeIn ();
        isPlayerInRange = true;
    }

    public override void HideInteractionKey ()
    {
        interactionKeyFader.FadeOut ();
        isPlayerInRange = false;
        DialogView.gameObject.SetActive (false);
    }
}
