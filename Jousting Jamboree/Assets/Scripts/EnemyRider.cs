using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRider : MonoBehaviour
{
    private GameController gameController;
    // Start is called before the first frame update

    private AudioSource enemyHead;
    public AudioClip groundHit;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        enemyHead = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            enemyHead.PlayOneShot(groundHit);
            StartCoroutine(RoundWin());
        }
    }

    IEnumerator RoundWin()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        gameController.RoundWinPlayer();
    }
}
