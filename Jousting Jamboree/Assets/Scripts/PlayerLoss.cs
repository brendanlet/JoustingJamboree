using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoss : MonoBehaviour
{
    private GameController gameController;

    private AudioSource myHead;
    public AudioClip groundHit;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        myHead = gameObject.GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            myHead.PlayOneShot(groundHit);
            StartCoroutine(RoundLoss());
        }
    }

    IEnumerator RoundLoss()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        gameController.RoundWinEnemy();
    }
}
