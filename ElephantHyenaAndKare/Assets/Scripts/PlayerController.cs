using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    public float moveSpd = 150;
    float move;
    public bool leftMove = false;
    private bool ground = true;
    private bool noMove = false;
    private bool drank = false;

    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D camCol;
    public CapsuleCollider2D capCol;
    public NextCutscene nextTrig;

    private AudioManager am;
    private bool swim;
    private bool rock;
    private bool tree;
    private bool drink;

    [SerializeField]
    private PlayableDirector scenePlayer;
    public TimelineAsset cutScene;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponentInChildren<AudioManager>();

        nextTrig = GameObject.FindWithTag("Bird").GetComponent<NextCutscene>();
        leftMove = false;
        noMove = false;
        rb = this.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Walking", false);

        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Push_Walk", false);

        camCol = transform.GetChild(0).GetComponent<BoxCollider2D>();
        capCol = GameObject.FindWithTag("Box").GetComponent<CapsuleCollider2D>();
        Physics2D.IgnoreCollision(camCol, capCol, true);

        scenePlayer = FindFirstObjectByType<PlayableDirector>();

        drank = false;
    }

    private void FixedUpdate()
    {
        if (noMove == false)
        {
            rb.velocity = new Vector2(move * moveSpd, Mathf.Clamp(rb.velocity.y, -10000, 0));
            anim.speed = 1;
            if (rb.velocity.x > 1)
            {
                if (swim == true)
                {
                    am.PlaySFX(am.Swim);
                    am.SFXSource.pitch = 1;
                    am.SFXSource.volume = 0.25f;
                }
                else if (rock == false)
                {
                    am.PlaySFX(am.Stomping);
                    am.SFXSource.pitch = 2;
                    am.SFXSource.volume = 0.25f;
                }

                leftMove = false;
                if (anim.GetBool("Push_Walk") == false)
                {
                    anim.SetBool("Walking", true);
                }
                else
                {
                    anim.SetBool("Walking", false);
                    anim.SetBool("Push_Walk", true);
                }
            }
            else if (rb.velocity.x < -1)
            {
                if (swim == true)
                {
                    am.PlaySFX(am.Swim);
                    am.SFXSource.pitch = 1;
                    am.SFXSource.volume = 0.25f;
                }
                else if (rock == false)
                {
                    am.PlaySFX(am.Stomping);
                    am.SFXSource.pitch = 2;
                    am.SFXSource.volume = 0.25f;
                }

                leftMove = true;
                if (anim.GetBool("Push_Walk") == false)
                {
                    anim.SetBool("Walking", true);
                }
                else
                {
                    anim.SetBool("Walking", false);
                    anim.SetBool("Push_Walk", true);
                }
            }
            else
            {
                am.PlaySFX(null);
                anim.SetBool("Walking", false);
                anim.SetBool("Push_Walk", false);
            }

            GetComponent<SpriteRenderer>().flipX = leftMove;

            move = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && drank == true)
        {
            StartCoroutine(TrunkInteract());
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
            rock = false;
            anim.SetBool("Push_Walk", false);
            anim.speed = 1;
        }
        if (collision.gameObject.tag == "Swim")
        {
            swim = false;
            anim.Play("Idle");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        ground = true;
        Debug.Log("Collided!" + collision.gameObject.name);
        if (collision.gameObject.tag == "Box")
        {
            if (rb.velocity.x! < 1 && rb.velocity.x! > -1)
            {
                //Debug.Log("Push Still");
                anim.SetBool("Push_Walk", true);
                anim.speed = 0;
            }
            else
            {
                //Debug.Log("Push Move");
                rock = true;
                am.PlaySFX(am.Rock);
                am.SFXSource.pitch = 1;
                am.SFXSource.volume = 1;
                anim.SetBool("Push_Walk", true);
                anim.speed = 1;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "EndScene")
        {
            scenePlayer.playableAsset = cutScene;
            scenePlayer.Play();
        }
        if (collision.gameObject.CompareTag("Swim"))
        {
            swim = true;
            anim.Play("Swim_Idle");
        }
    }

    public IEnumerator DrinkWater()
    {
        noMove = true;
        rb.velocity = new Vector2(0, 0);
        anim.speed = 1;
        anim.Play("Drinking");
        yield return new WaitForSecondsRealtime(0.4f);
        am.PlaySFX(am.Drink);
        am.SFXSource.pitch = 0.75f;
        am.SFXSource.volume = 0.5f;
        yield return new WaitForSecondsRealtime(1.5f);
        if (swim == false)
        {
            anim.Play("Idle");
        }
        else
        {
            anim.Play("Swim_Idle");
        }
        noMove = false;
        drank = true;
        nextTrig.Drank();
    }

    public IEnumerator TrunkInteract()
    {
        noMove = true;
        rb.velocity = new Vector2(0, 0);
        anim.speed = 1;
        am.PlaySFX(am.Thump);
        am.SFXSource.pitch = 0.8f;
        am.SFXSource.volume = 0.1f;
        anim.Play("Interact");
        yield return new WaitForSecondsRealtime(0.69f);
        if (swim == false)
        {
            anim.Play("Idle");
        }
        else
        {
            anim.Play("Swim_Idle");
        }
        noMove = false;
    }

    public void NoMove(bool x)
    {
        noMove = x;
    }

    public void NewScene(int x)
    {
        SceneManager.LoadScene(x);
    }


}
