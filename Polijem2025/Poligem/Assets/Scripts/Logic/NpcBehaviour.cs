using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{
    [SerializeField] string npcName;

    [SerializeField] SpriteFader interactionKeyFader;
    [SerializeField] DialogView dialogView;

    [SerializeField] string corelatedDialog;
    [SerializeField] Sprite corelatedBackGround;
    [SerializeField] Sprite corelatedPortret;

    protected bool isPlayerInRange = false;

    private void Update ()
    {
        if (isPlayerInRange && Input.GetKeyDown (KeyCode.E))
        {
            dialogView.Show (corelatedDialog, npcName, corelatedBackGround, corelatedPortret);
        }
    }

    public void ShowInteractionKey ()
    {
        interactionKeyFader.FadeIn ();
        isPlayerInRange = true;
    }

    public void HideInteractionKey ()
    {
        interactionKeyFader.FadeOut ();
        isPlayerInRange = false;
    }
}
