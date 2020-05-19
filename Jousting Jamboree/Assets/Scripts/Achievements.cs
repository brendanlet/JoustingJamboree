using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Achievements : MonoBehaviour
{
    public Transform currentMount;

    public float speedFactor = 0.1f;
    Ray ray;
    RaycastHit hit;
    GameObject nextLocation;
    public int maxRight = 2900;

    void Start()
    {
        nextLocation = GameObject.Find("CameraPivot/NextLocationPivot");
    }
    public void Update()
    {
        if(nextLocation.transform.position.x >= 100)
        {
            GameObject.Find("CameraPivot/Main Camera/BackArrow").SetActive(true);
        }
        else
        {
            GameObject.Find("CameraPivot/Main Camera/BackArrow").SetActive(false);
        }

        if (nextLocation.transform.position.x <= maxRight)
        {
            GameObject.Find("CameraPivot/Main Camera/NextArrow").SetActive(true);
        }
        else
        {
            GameObject.Find("CameraPivot/Main Camera/NextArrow").SetActive(false);
        }
        transform.position = Vector3.Lerp(transform.position, currentMount.position, speedFactor);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speedFactor);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                print(hit.collider.name);
                if (hit.collider.name == "NextArrow")
                {
                    nextLocation.transform.Translate(200, 0, 0);
                    setMount(nextLocation.transform);
                }
                if (hit.collider.name == "BackArrow")
                {
                    nextLocation.transform.Translate(-200, 0, 0);
                    setMount(nextLocation.transform);
                }
                if (hit.collider.name == "BackCube")
                {
                    SceneManager.LoadScene(0);
                }
            }
        }

    }

    public void setMount(Transform newMount)
    {
        currentMount = newMount;
        Debug.Log(currentMount);
    }

}



