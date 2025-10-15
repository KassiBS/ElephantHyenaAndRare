using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    private Animator anim;
    public BoxCollider2D camCol;
    public BoxCollider2D boxCol;

    public List<GameObject> colliders;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Push_Walk", false);

        camCol = transform.GetChild(0).GetComponent<BoxCollider2D>();
        boxCol = GameObject.FindWithTag("Box").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(camCol, boxCol, true);
        foreach (GameObject i in colliders)
        {
            Physics2D.IgnoreCollision(camCol, i.GetComponent<BoxCollider2D>(), true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("Collided!" + other.gameObject.name);
        if (other.gameObject.tag == "Box")
        {
            Debug.Log("Push");
            anim.SetBool("Push_Walk", true);
            Debug.Log("Wahh " + (anim.GetBool("Push_Walk") == true));
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            Debug.Log("NoPush");
            anim.SetBool("Push_Walk", false);
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine (transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }*/
}
