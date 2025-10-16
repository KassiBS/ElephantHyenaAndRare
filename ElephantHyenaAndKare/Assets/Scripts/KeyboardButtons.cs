using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButtons : MonoBehaviour
{
    public GameObject arrowL;
    public GameObject arrowR;
    public List<GameObject> space; // Could get the spacebars object instead since its the parent to all spacebars, for other levels have the space bar follow player and become visible when near interactible things.

    private Animator animL;
    private Animator animR;
    public List<Animator> animS;

    AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.Stomping);
        audioManager.SFXSource.Pause();
        animL = arrowL.GetComponent<Animator>();
        animR = arrowR.GetComponent<Animator>();

        foreach (GameObject i in space)
        {
            animS.Add(i.GetComponent<Animator>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animR.speed = 1;
            animL.speed = 1;
            

            animL.Play("ArrowLeftPress", -1, 0);
            animR.Play("ArrowRightPress", -1, 0);

            audioManager.SFXSource.Pause();


        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            foreach (Animator i in animS)
            {
                i.speed = 1;
                i.Play("SpacebarPress", -1, 0);
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animL.Play("ArrowLeftPress", -1, 0.5f);
            animL.speed = 0;

            animR.Play("ArrowRightPress", -1, 0);
            //animS.Play("SpacebarPress", -1, 0);
            animR.speed = 0;
            //animS.speed = 0;
            audioManager.PlaySFX(audioManager.Stomping);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animR.Play("ArrowRightPress", -1, 0.5f);
            animR.speed = 0;

            animL.Play("ArrowLeftPress", -1, 0);
            //animS.Play("SpacebarPress", -1, 0);
            animL.speed = 0;
            //animS.speed = 0;
            audioManager.PlaySFX(audioManager.Stomping);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            foreach (Animator i in animS)
            {
                i.Play("SpacebarPress", -1, 0.5f);
                i.speed = 0;
            }

            /*animL.Play("ArrowLeftPress", -1, 0);
            animR.Play("ArrowRightPress", -1, 0);
            animL.speed = 0;
            animR.speed = 0;*/
        }
    }
}
