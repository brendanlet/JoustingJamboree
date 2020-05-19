using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerWins : MonoBehaviour
{

    public int winCount;

    // Start is called before the first frame update
    void Start()
    { 
        GameController controller = GameObject.Find("GameController").GetComponent<GameController>();
        if(controller.playerWins >= winCount)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
