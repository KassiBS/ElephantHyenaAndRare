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

    public Rigidbody2D rb;
    public Animator anim;

    private AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponentInChildren<AudioManager>();

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
            else if (rb.velocity.x < -1)
            {
                am.PlaySFX(am.Stomping);
                am.SFXSource.pitch = 4;
                am.SFXSource.volume = 0.05f;
                leftMove = true;
                anim.SetBool("Walking", true);
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

    public void NoMove(bool x)
    {
        noMove = x;
    }
}
