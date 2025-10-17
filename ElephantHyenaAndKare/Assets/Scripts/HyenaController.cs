using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class HyenaController : MonoBehaviour
{
    public float moveSpd = 150;
    float move;
    public bool leftMove = false;
    private bool ground = true;
    private bool noMove = false;
    private bool noBack = false;

    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D col;

    private AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<AudioManager>();
        col = GetComponent<BoxCollider2D>();

        leftMove = false;
        noMove = false;
        rb = this.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Walking", false);

        anim = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (noMove == false)
        {
            rb.velocity = new Vector2(move * moveSpd, Mathf.Clamp(rb.velocity.y, -10000, 0));
            anim.speed = 1;
            if (rb.velocity.x > 1)
            {
                am.PlaySFX(am.Stomping);
                am.SFXSource.pitch = 4;
                am.SFXSource.volume = 0.05f;
                leftMove = false;
                anim.SetBool("Walking", true);
            }
            else if (rb.velocity.x < -1 && noBack == false)
            {
                am.PlaySFX(am.Stomping);
                am.SFXSource.pitch = 4;
                am.SFXSource.volume = 0.05f;
                leftMove = true;
                anim.SetBool("Walking", true);
            }
            else if (noBack == true)
            {
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                am.PlaySFX(null);
                anim.SetBool("Walking", false);
            }

            GetComponent<SpriteRenderer>().flipX = leftMove;

            move = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        }

        if (ground == false)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            collision.gameObject.SetActive(false);
            if (collision.gameObject.transform.localScale.x == 5)
            {
                am.PlaySFX(am.HyenaLaugh);
                am.SFXSource.pitch = 1;
                am.SFXSource.volume = 0.25f;
            }
            else
            {
                am.PlaySFX(am.HyenaLaugh);
                am.SFXSource.pitch = 1;
                am.SFXSource.volume = 0.1f;
            }
        }
        if (collision.gameObject.tag == "Box" && noMove == false)
        {
            noMove = true;
            moveSpd = 0;
            rb.velocity = new Vector2(0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            col.isTrigger = true;
            anim.SetBool("Walking", false);
            anim.Play("Idleh");
            am.PlaySFX(am.HyenaWhine);
            am.SFXSource.pitch = 1;
            am.SFXSource.volume = 0.2f;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            noMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && moveSpd == 0)
        {
            StartCoroutine(SpeedChang());
        }
        else if (collision.CompareTag("Player"))
        {
            col.isTrigger = false;
            moveSpd = 150;
            rb.constraints = RigidbodyConstraints2D.None;
            noBack = false;
            noMove = false;
        }
    }

    private IEnumerator SpeedChang()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSecondsRealtime(0.5f);
        moveSpd = 300;
        noMove = false;
        noBack = true;
    }

    public void NoMove(bool x)
    {
        noMove = x;
    }
}
