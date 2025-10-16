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

    private void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
        scenePlayer = FindFirstObjectByType<PlayableDirector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collide with " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collide med elephant");
            if (curScene != cutscenes.Count-1)
            {
                curScene++;
            }
            scenePlayer.playableAsset = cutscenes[curScene];
            scenePlayer.Play();
        }
    }

    public void NextTrig(bool x)
    {
        trigger.enabled = x;
    }
}
