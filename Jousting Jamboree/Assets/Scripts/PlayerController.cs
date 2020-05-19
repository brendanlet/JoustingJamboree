using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float xInputMovementSpeed = 1f;
    public float zInputMovementSpeed = 1f;

    public float xMovementBounds = 10;
    public float zMovementBounds = 10;


    private Rigidbody riderRb;
    public Vector3 hitForce = new Vector3(0, 1, 1);
    private GameObject rider;


    private Rigidbody rb;

    public AudioSource myFx;
    public AudioClip hitFx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rider = transform.Find("Rider").gameObject;
        riderRb = rider.transform.Find("Body").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }

    private void Move()
    {

        var movementVector = new Vector3();

        var xMove = Input.GetAxis("Horizontal") * xInputMovementSpeed;
        var zMove = Input.GetAxis("Vertical") * zInputMovementSpeed;

        movementVector.x = xMove;
        movementVector.z = zMove;

        rb.velocity += movementVector;


        if (transform.localPosition.x < -xMovementBounds)
        {
            var tmpVec = transform.localPosition;
            tmpVec.x = -xMovementBounds;
            transform.localPosition = tmpVec;
        }
        else if (transform.localPosition.x > xMovementBounds)
        {
            var tmpVec = transform.localPosition;
            tmpVec.x = xMovementBounds;
            transform.localPosition = tmpVec;
        }

        if (transform.localPosition.z < -zMovementBounds)
        {
            var tmpVec = transform.localPosition;
            tmpVec.z = -zMovementBounds;
            transform.localPosition = tmpVec;
        }
        else if (transform.localPosition.z > zMovementBounds)
        {
            var tmpVec = transform.localPosition;
            tmpVec.z = zMovementBounds;
            transform.localPosition = tmpVec;
        }


    }

    private void OnHit()
    {
        Debug.Log("Ouchie");

        myFx.PlayOneShot(hitFx);

        rider.transform.Find("Main Camera").parent = rider.transform.Find("Body");
        rider.transform.parent = null;

        foreach (Transform part in rider.transform)
        {
            if (part.GetComponent<Rigidbody>() != null)
            {
                part.GetComponent<Rigidbody>().mass = 1;
            }
        }

        var fixedJoints = rider.GetComponentsInChildren<FixedJoint>();
        foreach (var joint in fixedJoints)
        {
            if (joint.gameObject.name != "FootLeft" && joint.gameObject.name != "FootRight")
            {
                Destroy(joint);
            }
        }
        var characterJoints = rider.GetComponentsInChildren<CharacterJoint>();
        foreach (var joint in characterJoints)
        {
            joint.enableProjection = true;
        }

        riderRb.AddForce(hitForce);
        Destroy(this);

    }
}
