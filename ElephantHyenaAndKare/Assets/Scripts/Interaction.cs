using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private bool z_Interacted = false;
    private PlayerController pc;

    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
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
