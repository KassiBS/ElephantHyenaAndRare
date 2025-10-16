using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D fillCol;
    [SerializeField]
    private CapsuleCollider2D box;
    [SerializeField]
    private Rigidbody2D rbB;

    private void Start()
    {
        fillCol = GetComponent<BoxCollider2D>();
        box = transform.GetChild(0).GetComponent<CapsuleCollider2D>();
        rbB = box.gameObject.GetComponent<Rigidbody2D>();
        fillCol.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            rbB.gravityScale = 2;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            rbB.constraints = RigidbodyConstraints2D.FreezeAll;
            other.isTrigger = true;
            fillCol.enabled = true;
        }
    }
}