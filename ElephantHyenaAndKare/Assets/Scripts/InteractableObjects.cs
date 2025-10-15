using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractebleObject : CollidableObject
{
    private bool z_Interacted = false;

    protected override void OnCollided(GameObject collidedObject)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            OnInteract();
        }
    }

    private void OnInteract()
    {
        if (!z_Interacted)
        {
            z_Interacted = true;
            Debug.Log("INTERACT WITH " + name);
        }
    }
}
