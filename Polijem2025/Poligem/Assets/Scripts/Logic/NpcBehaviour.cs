using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviour : InteractableElement
{
    [SerializeField] string npcName;

    [SerializeField] SpriteFader interactionKeyFader;

    [SerializeField] Sprite corelatedBackGround;
    [SerializeField] Sprite corelatedPortret;
    [SerializeField] List<DialogInfo> dialogs;

    protected bool isPlayerInRange = false;
    protected int dialogIndex = 0;

    private void Update ()
    {
        if (isPlayerInRange && !DialogView.gameObject.activeSelf && Input.GetKeyDown (KeyCode.E))
        {
            if (dialogs == null)
                return;

            if (dialogs.Count < 1)
                return;

            DialogView.Show(dialogs[dialogIndex], npcName, corelatedBackGround, corelatedPortret);
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
        DialogView.CloseDialog ();
    }
}
