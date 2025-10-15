using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButtons : MonoBehaviour
{
    public GameObject arrowL;
    public GameObject arrowR;
    public GameObject space;

    private Animator animL;
    private Animator animR;
    private Animator animS;

    // Start is called before the first frame update
    void Start()
    {
        animL = arrowL.GetComponent<Animator>();
        animR = arrowR.GetComponent<Animator>();
        animS = space.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Space))
        {
            animR.speed = 1;
            animL.speed = 1;
            animS.speed = 1;

            animL.Play("ArrowLeftPress", -1, 0);
            animR.Play("ArrowRightPress", -1, 0);
            animS.Play("SpacebarPress", -1, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animL.Play("ArrowLeftPress", -1, 0.5f);
            animL.speed = 0;

            animR.Play("ArrowRightPress", -1, 0);
            animS.Play("SpacebarPress", -1, 0);
            animR.speed = 0;
            animS.speed = 0;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animR.Play("ArrowRightPress", -1, 0.5f);
            animR.speed = 0;

            animL.Play("ArrowLeftPress", -1, 0);
            animS.Play("SpacebarPress", -1, 0);
            animL.speed = 0;
            animS.speed = 0;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            animS.Play("SpacebarPress", -1, 0.5f);
            animS.speed = 0;

            animL.Play("ArrowLeftPress", -1, 0);
            animR.Play("ArrowRightPress", -1, 0);
            animL.speed = 0;
            animR.speed = 0;
        }
    }
}
