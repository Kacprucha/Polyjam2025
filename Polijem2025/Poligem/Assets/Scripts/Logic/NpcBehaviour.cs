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
    protected bool finishedMainDialog = false;
    protected int dialogIndex = 1;

    private void Update ()
    {
        if (isPlayerInRange && !DialogView.gameObject.activeSelf && Input.GetKeyDown (KeyCode.E))
        {
            if (dialogs == null)
                return;

            if (dialogs.Count < 1)
                return;

            // If main dialog tree was finished use default option
            if (finishedMainDialog == true || dialogs.Count == dialogIndex)
            {
                finishedMainDialog = true;
                dialogIndex = 0;
            }
            else if (finishedMainDialog == false && dialogs.Count > 1 && dialogIndex == 0)
                dialogIndex = 1;

            DialogView.Show(dialogs[dialogIndex], npcName, corelatedBackGround, corelatedPortret);
            dialogIndex++; 
    
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
