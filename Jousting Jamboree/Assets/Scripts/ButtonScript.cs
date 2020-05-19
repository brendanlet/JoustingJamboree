using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
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
        transform.position = Vector3.Lerp(transform.position, currentMount.position, speedFactor);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speedFactor);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(hit.collider.name == "StartMatch")
                {
                    var gameController = GameObject.Find("GameController").GetComponent<GameController>();
                    gameController.clearCurrentUnlocks();
                    SceneManager.LoadScene(1);
                }
                if (hit.collider.name == "UnlocksCube")
                {
                    SceneManager.LoadScene(2);
                }
                if (hit.collider.name == "Credits")
                {
                    SceneManager.LoadScene(3);
                }
                else if (hit.collider.name == "Quit")
                {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

                if (hit.collider.tag == "Clickable")
                {
                    setMount(hit.collider.transform.GetChild(0).transform);
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



