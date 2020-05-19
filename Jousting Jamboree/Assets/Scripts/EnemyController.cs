using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameController gameController;
    public Vector3 hitForce = new Vector3(0, 1, 1);
    private Rigidbody riderRb;
    private Camera playerCamera, enemyCamera;
    private GameObject rider;

    public AudioSource myFx;
    public AudioClip hitFx;

    // Start is called before the first frame update
    void Start()
    {
        rider = transform.Find("Rider").gameObject;
        riderRb = rider.transform.Find("Body").GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        enemyCamera = transform.Find("Rider/Body/Enemy Camera").GetComponent<Camera>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnHit()
    {
        myFx.PlayOneShot(hitFx);
        Debug.Log("Yowza");
        enemyCamera.gameObject.SetActive(true);
        var tmpRect = playerCamera.rect;
        tmpRect.x = -.5f;
        playerCamera.rect = tmpRect;
        tmpRect = enemyCamera.rect;
        tmpRect.x = .5f;
        enemyCamera.rect = tmpRect;

        transform.Find("Rider").parent = null;

        foreach (Transform part in rider.transform)
        {
            part.GetComponent<Rigidbody>().mass = 1;
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


        riderRb.isKinematic = false;
        riderRb.AddForce(hitForce);

        //Detatch from mount and ragdoll
    }
}
