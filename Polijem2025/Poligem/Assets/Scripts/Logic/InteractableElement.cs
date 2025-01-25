using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableElement : MonoBehaviour
{
    [SerializeField] public DialogView DialogView;
    public virtual void ShowInteractionKey ()
    {
        Debug.LogError ("Not implemented");
    }

    public virtual void HideInteractionKey ()
    {
        Debug.LogError ("Not implemented");
    }
}
