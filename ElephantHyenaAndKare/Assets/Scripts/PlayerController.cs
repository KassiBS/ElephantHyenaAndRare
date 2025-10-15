using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpd = 150;
    float move;
    public bool leftMove = false;
    private bool ground = true;

    private Rigidbody2D rb;
    private Animator anim;
    public BoxCollider2D camCol;
    public BoxCollider2D boxCol;
    public List<GameObject> colliders;

    // Start is called before the first frame update
    void Start()
    {
        leftMove = false;
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Walking", false);

        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Push_Walk", false);

        camCol = transform.GetChild(0).GetComponent<BoxCollider2D>();
        boxCol = GameObject.FindWithTag("Box").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(camCol, boxCol, true);
        foreach (GameObject i in colliders)
        {
            Physics2D.IgnoreCollision(camCol, i.GetComponent<BoxCollider2D>(), true);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move * moveSpd, Mathf.Clamp(rb.velocity.y, -10000, 0));
        anim.speed = 1;

        if (rb.velocity.x > 1)
        {
            leftMove = false;
            if (anim.GetBool("Push_Walk") == false)
                anim.SetBool("Walking", true);
            else
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Push_Walk", true);
            }
        }
        else if (rb.velocity.x < -1)
        {
            leftMove = true;
            if (anim.GetBool("Push_Walk") == false)
                anim.SetBool("Walking", true);
            else
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Push_Walk", true);
            }
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Push_Walk", false);
        }

        GetComponent<SpriteRenderer>().flipX = leftMove;

        move = Input.GetAxisRaw("Horizontal") * Time.deltaTime;

        if (ground == false)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 1f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ground = false;
        if (collision.gameObject.tag == "Box")
        {
            //Debug.Log("NoPush");
            anim.SetBool("Push_Walk", false);
            anim.speed = 1;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        ground = true;
        //Debug.Log("Collided!" + collision.gameObject.name);
        if (collision.gameObject.tag == "Box")
        {
            if (rb.velocity.x !< 1 && rb.velocity.x !> -1)
            {
                //Debug.Log("Push Still");
                anim.SetBool("Push_Walk", true);
                anim.speed = 0;
            }
            else
            {
                //Debug.Log("Push Move");
                anim.SetBool("Push_Walk", true);
                anim.speed = 1;
            }
        }
    }
}
