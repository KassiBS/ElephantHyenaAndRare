using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private bool z_Interacted = false;
    private PlayerController pc;
    private HyenaController hc;

    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            hc = GameObject.FindWithTag("Hyena").GetComponent<HyenaController>();
        }
        z_Interacted = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.canDrink = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space) && z_Interacted == false)
        {
            OnInteract();
        }
        /*else if (z_Interacted == true && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(pc.TrunkInteract());
        }*/
        if (collidedObject.CompareTag("Hyena"))
        {
            Physics2D.IgnoreCollision(hc.GetComponent<Collider2D>(), pc.GetComponent<Collider2D>(), true);
            hc.transform.Translate(new Vector2(10, hc.gameObject.transform.position.y)*Time.deltaTime);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc.canDrink = false;
        }
        if (collision.CompareTag("Hyena"))
        {
            hc.rb.velocity = new Vector2(0, 0);
            hc.anim.SetBool("Walking", false);
            hc.anim.Play("Idleh");
            hc.NoMove(true);
            if (SceneManager.GetActiveScene().buildIndex != 2)
            {
                Physics2D.IgnoreCollision(hc.GetComponent<Collider2D>(), pc.GetComponent<Collider2D>(), false);
            }
        }
    }

    private void OnInteract()
    {
        if (!z_Interacted)
        {
            Debug.Log("Drink water");
            StartCoroutine(pc.DrinkWater());
            z_Interacted = true;
        }
    }
}
