using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    public Transform currentMount;

    public float speedFactor = 0.1f;
    Ray ray;
    RaycastHit hit;

    void Start()
    {

    }
    public void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.name == "BackCube")
                {
                    SceneManager.LoadScene(0);
                }
                print(hit.collider.name);
            }
        }

    }
}
