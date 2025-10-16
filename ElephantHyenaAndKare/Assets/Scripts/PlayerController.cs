using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpd = 150;
    float move;
    public bool leftMove = false;
    private bool ground = true;
    private bool noMove = false;

    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D camCol;
    public CapsuleCollider2D capCol;
    public NextCutscene nextTrig;

    // Start is called before the first frame update
    void Start()
    {
        nextTrig = GameObject.FindWithTag("Bird").GetComponent<NextCutscene>();
        leftMove = false;
        noMove = false;
        rb = this.GetComponent<Rigidbody2D>();
        Debug.Log("Player Controller Active");
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Walking", false);

        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Push_Walk", false);

        camCol = transform.GetChild(0).GetComponent<BoxCollider2D>();
        capCol = GameObject.FindWithTag("Box").GetComponent<CapsuleCollider2D>();
        Physics2D.IgnoreCollision(camCol, capCol, true);
    }

    private void FixedUpdate()
    {
        if (noMove == false)
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
        }

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
        Debug.Log("Collided!" + collision.gameObject.name);
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

    public IEnumerator DrinkWater()
    {
        noMove = true;
        rb.velocity = new Vector2(0,0);
        anim.speed = 1;
        anim.Play("Drinking");
        yield return new WaitForSecondsRealtime(1.9f);
        anim.Play("Idle");
        noMove = false;
        nextTrig.Drank();
    }

    public IEnumerator TrunkInteract()
    {
        noMove = true;
        rb.velocity = new Vector2(0, 0);
        anim.speed = 1;
        anim.Play("Interact");
        yield return new WaitForSecondsRealtime(0.69f);
        anim.Play("Idle");
        noMove = false;
    }

    public void NoMove(bool x)
    {
        noMove = x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup") && collision.gameObject.activeSelf)
        {
           collision.gameObject.SetActive(false);
        }
    }
}
