using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : InteractableElement
{
    [SerializeField] InfoView infoView;
    [SerializeField] string info;
    [SerializeField] SpriteFader interactionKeyFader;

    [SerializeField] SpriteFader firstDoors;
    [SerializeField] SpriteFader secondDoors;

    bool elementIsInRange = false;

    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        if (elementIsInRange && !DialogView.gameObject.activeSelf && Input.GetKeyDown (KeyCode.E))
        {
            if (PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.Indetyficator) > 0)
            {
                StartCoroutine (OpenDoors ());
            }
            else
            {
                infoView.SetInfo (info);
            }
        }
    }

    public override void ShowInteractionKey ()
    {
        if (firstDoors.gameObject.activeSelf && secondDoors.gameObject.activeSelf)
        {
            interactionKeyFader.FadeIn ();
            elementIsInRange = true;
        }
    }

    public override void HideInteractionKey ()
    {
        interactionKeyFader.FadeOut ();
        elementIsInRange = false;
    }

    protected IEnumerator OpenDoors ()
    {
        firstDoors.FadeOut ();
        secondDoors.FadeOut ();

        yield return new WaitForEndOfFrame ();

        firstDoors.gameObject.SetActive (false);
        secondDoors.gameObject.SetActive (false);
    }
}
