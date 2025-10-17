using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HyenaController : MonoBehaviour
{
    public float moveSpd = 150;
    float move;
    public bool leftMove = false;
    private bool ground = true;
    private bool noMove = false;
    private bool noBack = false;
    float curAlpha;

    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D col;
    public SpriteRenderer heart;

    private AudioManager am;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<AudioManager>();
        col = GetComponent<BoxCollider2D>();
        pc = FindFirstObjectByType<PlayerController>();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            heart = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            curAlpha = heart.color.a;
            heart.color = new Color(255, 255, 255, 0);
        }

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

            move = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        }

        GetComponent<SpriteRenderer>().flipX = leftMove;

        if (ground == false)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            StartCoroutine(BackEllie());
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
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.name == "Poo")
        {
            SceneManager.LoadScene(2);
        }
        if (collision.gameObject.tag == "Box" && noMove == false)
        {
            noMove = true;
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

    public IEnumerator SpeedChang()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSecondsRealtime(0.5f);
        while (transform.position.x < 5 + pc.transform.position.x)
        {
            moveSpd = 300;
            noMove = false;
            noBack = true;
            rb.velocity = new Vector2(1 * moveSpd, Mathf.Clamp(rb.velocity.y, -10000, 0));
            yield return new WaitForSecondsRealtime(0.001f);
        }
        col.isTrigger = false;
        moveSpd = 150;
        rb.constraints = RigidbodyConstraints2D.None;
        noBack = false;
        noMove = false;
    }

    private IEnumerator BackEllie()
    {
        Debug.Log(curAlpha);
        while (curAlpha < 1)
        {
            curAlpha += 0.1f;
            heart.color = new Color(255, 255, 255, curAlpha);

            yield return new WaitForSeconds(0.005f); // update interval
        }
        yield return new WaitForSeconds(1f);
        while (curAlpha > 0)
        {
            curAlpha -= 0.1f;
            heart.color = new Color(255, 255, 255, curAlpha);

            yield return new WaitForSeconds(0.005f); // update interval
        }
    }

    public void NoMove(bool x)
    {
        noMove = x;
    }
}
