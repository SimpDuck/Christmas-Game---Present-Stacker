using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private  InputManager inputManager;
    private Rigidbody2D rbody2D;
    private Animator anim;
    private BoxCollider2D capCol2D;

    [SerializeField] float jumpVelocity = 100f;


    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        rbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capCol2D = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        inputManager.OnMousePress += Jump;
    }

    private void OnDisable()
    {
        inputManager.OnMousePressed -= Jump;
    }

    private void Jump()
    {
        rbody2D.velocity = Vector2.up * jumpVelocity;;
        anim.SetBool("isJumping", true);
    }

    public void CanClimb()
    {
        StartCoroutine(Climb());
    
    }
    IEnumerator Climb()
    {
        capCol2D.enabled = false;
        rbody2D.velocity = Vector2.up * 4;
        yield return new WaitForSeconds(1);
        capCol2D.enabled = true;
        OnLanding();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sleigh"||collision.gameObject.tag == "Stack Controller")
        {
            OnLanding();
        }
    }

    public void OnLanding()
    {
        anim.SetBool("isJumping", false);
    }
}
