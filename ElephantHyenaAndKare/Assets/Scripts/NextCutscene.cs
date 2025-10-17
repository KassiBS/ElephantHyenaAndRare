using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class NextCutscene : MonoBehaviour
{
    public List<TimelineAsset> cutscenes;
    [SerializeField]
    private BoxCollider2D trigger;
    [SerializeField]
    private PlayableDirector scenePlayer;
    [SerializeField]
    private int curScene;

    private GameObject speechbubble;
    private SpriteRenderer toChang;
    public List<Sprite> icons;

    private bool trigStay;
    private bool birdLeave;

    private void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
        scenePlayer = FindFirstObjectByType<PlayableDirector>();
        speechbubble = transform.GetChild(0).gameObject;
        toChang = speechbubble.transform.GetChild(0).GetComponent<SpriteRenderer>();
        speechbubble.SetActive(false);
        toChang.sprite = null;
        birdLeave = false;
        if (cutscenes.Count != 0 && scenePlayer != null)
        {
            scenePlayer.playableAsset = cutscenes[0];
            scenePlayer.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigStay = true;
        Debug.Log("Collide with " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collide med elephant");
            if (birdLeave == true)
            {
                gameObject.SetActive(false);
            }
            if (curScene != cutscenes.Count-1)
            {
                curScene++;
            }
            if (curScene == cutscenes.Count-1)
            {
                birdLeave = true;
            }
            scenePlayer.playableAsset = cutscenes[curScene];
            scenePlayer.Play();
            trigStay = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && trigStay == true)
        {
            if (curScene != cutscenes.Count - 1)
            {
                curScene++;
            }
            scenePlayer.playableAsset = cutscenes[curScene];
            scenePlayer.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collide drink with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("You need to drink");
            speechbubble.SetActive(true);
            toChang.sprite = icons[0];
        }
    }

    public void NextTrig(bool x)
    {
        trigger.enabled = x;
    }

    public void Drink()
    {
        trigger.enabled = true;
        trigger.isTrigger = false;
    }

    public void Drank()
    {
        trigger.isTrigger = true;
        toChang.sprite = null;
        speechbubble.SetActive(false);
    }
}
