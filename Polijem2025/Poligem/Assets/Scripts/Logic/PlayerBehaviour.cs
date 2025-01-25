using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] Transform centerPoint;
    [SerializeField] float sarchingRange = 0.5f;
    [SerializeField] LayerMask npcLayer;

    protected Collider2D[] previosuNpcInRange;

    private void Start ()
    {
        previosuNpcInRange = new Collider2D[0];
    }

    private void Update ()
    {
        Collider2D[] npcInRange = Physics2D.OverlapCircleAll (transform.position, sarchingRange, npcLayer);
        List<Collider2D> npcInRangeList = new List<Collider2D> (npcInRange);

        if (npcInRangeList != null && npcInRangeList.Count > 0) 
        {
            foreach (Collider2D npc in previosuNpcInRange)
            {
                bool npcToHide = true;
                foreach (Collider2D npc2 in npcInRangeList)
                {
                    if (npc == npc2)
                    {
                        npcToHide = false;
                        break;
                    }
                }

                if (npcToHide)
                {
                    npc.GetComponent<NpcBehaviour> ().HideInteractionKey ();
                }
            }
        }
        else
        {
            foreach (Collider2D npc in previosuNpcInRange)
            {
                npc.GetComponent<NpcBehaviour> ().HideInteractionKey ();
            }
        }

        previosuNpcInRange = npcInRange != null && npcInRange.Length > 0 ? npcInRange : new Collider2D[0];

        foreach (Collider2D npc in npcInRange)
        {
            npc.GetComponent<NpcBehaviour> ().ShowInteractionKey ();
        }
    }

    //private void OnDrawGizmosSelected ()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere (transform.position, sarchingRange);
    //}
}
