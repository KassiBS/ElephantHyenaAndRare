using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private bool z_Interacted = false;
    private PlayerController pc;
    private HyenaController hc;

    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        hc = GameObject.FindWithTag("Hyena").GetComponent<HyenaController>();
        z_Interacted = false;
    }

    private void OnTriggerStay2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Player") && Input.GetKey(KeyCode.Space))
        {
            OnInteract();
        }
        else if (z_Interacted == true && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(pc.TrunkInteract());
        }
        if (collidedObject.CompareTag("Hyena"))
        {
            hc.transform.Translate(new Vector2(10, hc.gameObject.transform.position.y)*Time.deltaTime);
            hc.rb.velocity = new Vector2(0, 0);
            hc.anim.SetBool("Walking", false);
            hc.anim.Play("Idleh");
            hc.NoMove(true);
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
