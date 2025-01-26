using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    [SerializeField] Animator animator;

    [SerializeField] DialogView DialogView;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        if (!DialogView.inputOption.activeSelf)
        {
            movement.x = Input.GetAxis ("Horizontal");
            movement.y = Input.GetAxis ("Vertical");

            animator.SetFloat ("Horizontal", movement.x);
            animator.SetFloat ("Vertical", movement.y);
            animator.SetFloat ("Speed", movement.sqrMagnitude);
        }
    }

    void FixedUpdate ()
    {
        rb.MovePosition (rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
