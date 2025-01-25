using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] Transform centerPoint;
    [SerializeField] float sarchingRange = 0.5f;
    [SerializeField] LayerMask npcLayer;

    protected Collider2D[] previousElementsInRange;

    private void Start ()
    {
        previousElementsInRange = new Collider2D[0];
    }

    private void Update ()
    {
        Collider2D[] elementsInRange = Physics2D.OverlapCircleAll (transform.position, sarchingRange, npcLayer);
        List<Collider2D> elementsInRangeList = new List<Collider2D> (elementsInRange);

        if (elementsInRangeList != null && elementsInRangeList.Count > 0) 
        {
            foreach (Collider2D element in previousElementsInRange)
            {
                bool elementToHide = true;
                foreach (Collider2D element2 in elementsInRangeList)
                {
                    if (element == element2)
                    {
                        elementToHide = false;
                        break;
                    }
                }

                if (elementToHide)
                {
                    element.GetComponent<InteractableElement> ().HideInteractionKey ();
                }
            }
        }
        else
        {
            foreach (Collider2D element in previousElementsInRange)
            {
                if (element.gameObject.activeSelf)
                {
                    element.GetComponent<InteractableElement> ().HideInteractionKey ();
                }
            }
        }

        previousElementsInRange = elementsInRange != null && elementsInRange.Length > 0 ? elementsInRange : new Collider2D[0];

        foreach (Collider2D element in elementsInRange)
        {
            if (element.GetComponent<NpcBehaviour> () != null)
            {
                element.GetComponent<NpcBehaviour> ().ShowInteractionKey ();
            }
            else if (element.GetComponent<Collectable> () != null)
            {
                element.GetComponent<Collectable> ().ShowInteractionKey ();
            }
            else if (element.GetComponent<InteractableBehaviour> () != null)
            {
                element.GetComponent<InteractableBehaviour> ().ShowInteractionKey ();
            }
        }
    }

    //private void OnDrawGizmosSelected ()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere (transform.position, sarchingRange);
    //}
}
