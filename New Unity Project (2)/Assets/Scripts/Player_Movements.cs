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
    [SerializeField] private LayerMask platforms;
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

        if (movement_x == 0 && movement_y == 0)
        {
            animator.SetBool("IsRun", false);
        }
        else
        {
            animator.SetBool("IsRun", true);
        }
        if (movement_x < 0) // Flip Sprite
        {
           
            transform.localScale = new Vector2(-scale, transform.localScale.y);
        }
        if (movement_x > 0)
        {

            transform.localScale = new Vector2(scale, transform.localScale.y);
        }

        
        if (IsGround() && (Input.GetButtonDown("Jump")) && (Mathf.Abs(_rigidbody.velocity.y) < 0.001f))
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
        if (!IsGround())
           animator.SetBool("IsJump", true);
        if (IsGround())
            animator.SetBool("IsJump", false);

        if (Input.GetKey(KeyCode.DownArrow) )
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
        Debug.Log(invincibilityFrame);
    }

    public bool IsGround()
        {
        bool b=true;
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, platforms);
            //Debug.Log(raycastHit2D.collider);
        if (raycastHit2D.collider != null)
        {
            b = true;
        }
        if (raycastHit2D.collider == null)
        {
            b = false;
        }
        //Debug.Log(b);
        return b;
        }

    

}
