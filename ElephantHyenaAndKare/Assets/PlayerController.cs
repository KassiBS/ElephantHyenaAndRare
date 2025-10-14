using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpd = 150;
    float move;
    private Rigidbody2D rb;
    public bool leftMove = false;
    //public List<BoxCollider2D> ground = new List<BoxCollider2D>();
    public bool ground = true;

    // Start is called before the first frame update
    void Start()
    {
        //ground.Add(GameObject.FindWithTag("Slope").GetComponent<BoxCollider2D>());
        leftMove = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move * moveSpd, Mathf.Clamp(rb.velocity.y, -10000, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 1)
            leftMove = false;
        else if (rb.velocity.x < -1)
            leftMove = true;

        GetComponent<SpriteRenderer>().flipX = leftMove;

        move = Input.GetAxisRaw("Horizontal")*Time.deltaTime;

        if (ground == false)
        {
            transform.Translate(Vector2.down * Time.deltaTime*2);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ground = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        ground = true;
    }
}
