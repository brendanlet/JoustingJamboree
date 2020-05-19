using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hop : MonoBehaviour
{
    public float hopPower = 3f;

    private Rigidbody rb;
    private Rigidbody parentRb;

    private AudioSource mountFeet;
    public AudioClip clickityClack;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        parentRb = transform.parent.parent.GetComponent<Rigidbody>();
        mountFeet = gameObject.GetComponent<AudioSource>();

    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            if (gameObject.transform.root.name == "PlayerUnit")
            {
                mountFeet.PlayOneShot(clickityClack);
                Debug.Log("Clickity Clack");
            }
            var tmpVec = rb.velocity;
            tmpVec.y = hopPower;
            rb.velocity = tmpVec;
        }
    }
}
