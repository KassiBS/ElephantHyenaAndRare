using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private bool z_Interacted = false;
    private PlayerController pc;

    AudioManager audioManager;

    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.Drink);
        audioManager.SFXSource.Pause();
    }

    private void OnTriggerStay2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Player") && Input.GetKey(KeyCode.Space))
        {
            OnInteract();
        }
    }

    private void OnInteract()
    {
        if (!z_Interacted)
        {
            z_Interacted = true;
            Debug.Log("Drink water");
            StartCoroutine(pc.DrinkWater());

            audioManager.PlaySFX(audioManager.Drink);
        }
    }
}
