using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player_Movements : MonoBehaviour
{
    // public
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public Animator animator;
    public bool invincibilityFrame;
    //[SerializeField] private LayerMask platforms;
    //private 
   
    private Rigidbody2D _rigidbody;
    private float scale;
    private bool IsJump;
    private bool IsRunn;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        invincibilityFrame= false;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        var movement_x = Input.GetAxisRaw("Horizontal");
        var movement_y = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(movement_x, 0, 0) * Time.deltaTime * MovementSpeed;
        animator.SetFloat("Speed", Mathf.Abs(movement_x));
        //Run animation
        if (movement_x == 0 && movement_y == 0)
        {
            animator.SetBool("IsRun", false);
        }
        else
        {
            animator.SetBool("IsRun", true);
        }
        // Flip Sprite
        if (movement_x < 0)
        {
            transform.localScale = new Vector2(-scale, transform.localScale.y);
        }
        if (movement_x > 0)
        {
            transform.localScale = new Vector2(scale, transform.localScale.y);
        }

        //Jump animation
        if ((Input.GetButtonDown("Jump")) && (Mathf.Abs(_rigidbody.velocity.y) < 0.001f))
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
        //Roll animation
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("IsRoll", true);
           
        }
        else
        {
            animator.SetBool("IsRoll", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            invincibilityFrame = true;
        }
        else
        {
            invincibilityFrame = false;
        }
        //Debug.Log(invincibilityFrame);
        //Guard animation
       // bool isGuard;
        if (Input.GetKey(KeyCode.G))
        {
            animator.SetBool("IsGuard", true);
        }
        else
        {
            animator.SetBool("IsGuard", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
        {
            MovementSpeed = 0;
        }
        else
        {
            MovementSpeed = 3;
        }

    }


    /*private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log(collision.gameObject.tag == "Ground");
        if(collision.gameObject.tag == "Ground")
        {
            animator.SetBool("IsJump", false);
        }
    }*/
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name == "Ground");
        if(collision.gameObject.name == "Ground")
        {
            animator.SetBool("IsJump", false);
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name == "Ground");
        if (collision.gameObject.name == "Ground")
        {
            animator.SetBool("IsJump", true);
        }
    }
}
