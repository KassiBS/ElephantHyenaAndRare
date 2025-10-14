using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    /*private Rigidbody2D rbC;
    bool followP = true;
    Transform cam;
    float camY;
    Transform player;
    public CapsuleCollider2D cap;
    public PolygonCollider2D poly;
    bool slow;*/
    public List<GameObject> fBound;
    Transform player;

    Vector3 worldPos;
    bool noLoop = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        /*cap = GetComponent<CapsuleCollider2D>();
        poly = GetComponent<PolygonCollider2D>();
        rbC = GetComponent<Rigidbody2D>();
        cam = GetComponent<Transform>();
        camY = cam.position.y;
        player = GameObject.FindWithTag("Player").transform;*/
}

// Update is called once per frame
void Update()
    {
        transform.rotation = Quaternion.identity;
        if (noLoop == true && transform.position != GetComponentInParent<Transform>().position)
        {
            transform.position = worldPos;
        }
        if (noLoop == true && transform.localPosition.x < player.transform.position.x + 0.1f && transform.localPosition.x > player.transform.position.x - 0.1f)
        {
            Debug.Log("Evil");
            this.transform.SetParent(player.gameObject.GetComponent<Transform>());
            StartCoroutine(NOSTOP());
        }
        /*if (followP == true)
        {
            //Debug.Log("Following player");
            cam.transform.position = (new Vector3(player.position.x, Mathf.Clamp(player.position.y, camY, player.position.y), cam.position.z)); //Mathf.Clamp(player.transform.position.y, camY, player.transform.position.y)
        }
        else
        {
            cam.transform.Translate(0,0,0);
        }*/
    }

    private IEnumerator OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Bounds" && transform.parent == player && noLoop == false)
        {
            Debug.Log("Woagh");
            transform.SetParent(null);
            //worldPos = new Vector3(GetComponentInParent<Transform>().position.x, GetComponentInParent<Transform>().position.y, -1);
            //Debug.Log(worldPos);
            yield return new WaitForSeconds(0.1f);
            noLoop = true;
        }
    }

    private IEnumerator NOSTOP()
    {
        yield return new WaitForSeconds(0.5f);
        noLoop = false;
    }

    /*private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (followP == true && other.tag == "Bounds")
        {
            Debug.Log("Collided with boundary");
            foreach (GameObject i in fBound)
            {
                if (other.gameObject == i)
                {
                    if (cap.enabled == true)
                    {
                        cap.enabled = false;
                    }
                    if (poly.enabled == true)
                    {
                        poly.enabled = false;
                    }
                    followP = false;
                }
            }
        }
        else if (followP == false)
        {
            followP = true;
            yield return new WaitForSeconds(5);

            cap.enabled = true;
            poly.enabled = true;
        }
    }*/
}
