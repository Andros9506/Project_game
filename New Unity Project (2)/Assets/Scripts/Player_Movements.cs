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
    [SerializeField] private LayerMask platforms;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
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
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        animator.SetFloat("Speed", Mathf.Abs(movement));

        if (movement == 0)
        {
            animator.SetBool("IsRun", false);
        }
        else
        {
            animator.SetBool("IsRun", true);
        }
        if (movement < 0) // Flip Sprite
        {
            transform.localScale = new Vector2(-scale, transform.localScale.y);
        }
        if (movement > 0)
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
        Debug.Log(b);
        return b;
        }


    

}
