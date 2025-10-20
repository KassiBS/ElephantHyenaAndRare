using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreePush : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D fillCol;
    [SerializeField]
    private GameObject tree;
    [SerializeField]
    private CapsuleCollider2D bridge;
    [SerializeField]
    private Rigidbody2D rbB;
    [SerializeField]
    private Animator animT;

    private PlayerController pc;
    private AudioManager am;

    bool noLoop = true;
    bool collided = false;

    private void Start()
    {
        //Debug.Log("Start function");
        am = GameObject.FindFirstObjectByType<AudioManager>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        noLoop = true;
        animT = transform.GetChild(0).GetComponent<Animator>();
        animT.speed = 0;
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Start animation");
            StartCoroutine(smoothTransition());
        }
    }*/

    private void Update()
    {
        if (collided == true && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Start animation");
            StartCoroutine(pc.TrunkInteract());
            StartCoroutine(smoothTransition());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Colliding with " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            collided = true;
        }
    }

    private IEnumerator smoothTransition()
    {
        animT.speed = 1;
        yield return new WaitForSecondsRealtime(0.15f);
        am.PlaySFX(am.Tree);
        am.SFXSource.pitch = 0.75f;
        am.SFXSource.volume = 1;
        yield return new WaitForSecondsRealtime(0.6f);

        Debug.Log("Transition");
        if (noLoop == true)
        {
            bridge.GetComponent<SpriteRenderer>().enabled = true;
            bridge.enabled = true;
            rbB.constraints = RigidbodyConstraints2D.None;
            fillCol.enabled = true;

            /*fillCol = GetComponent<BoxCollider2D>();
            tree = transform.GetChild(0).gameObject;
            bridge = transform.GetChild(1).GetComponent<CapsuleCollider2D>();
            rbB = bridge.gameObject.GetComponent<Rigidbody2D>();*/

            tree.SetActive(false);
            noLoop = false;

            yield return new WaitForSecondsRealtime(0.5f);

            rbB.constraints = RigidbodyConstraints2D.FreezeAll;
            bridge.enabled = false;
            fillCol.isTrigger = false;
        }
    }
}
