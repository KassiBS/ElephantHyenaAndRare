using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    private Rigidbody2D rbC;
    bool followP = false;
    public List<GameObject> fBound;
    Transform cam;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rbC = GetComponent<Rigidbody2D>();
        cam = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").transform;
        fBound.Add(GameObject.FindWithTag(""));
    }

    // Update is called once per frame
    void Update()
    {
        if (followP == true)
        {
            cam.transform.Translate(player.transform.position); //player.transform.position.x, player.transform.position.y, player.transform.position.z
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (followP == false)
        {
            followP = true;
        }
        else
        {
            followP = false;
        }
    }
}
