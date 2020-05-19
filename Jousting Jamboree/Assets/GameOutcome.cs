using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOutcome : MonoBehaviour
{
    int increment = 1200;
    int maxRight = 100;
    // Start is called before the first frame update
    void Start()
    {
        var gameController = GameObject.Find("GameController").GetComponent<GameController>();
        var stats = GameObject.Find("Outcome/Canvas/Unlock").GetComponent<TextMeshProUGUI>();
        stats.text = "Play Session Stats:\nMatch Wins: " + gameController.matchesWon + "\nMatch Losses: " + gameController.matchesLost;

        if(gameController.gameOutcome == false)
        {
            var loss = GameObject.Find("Outcome/Canvas/Text Text").GetComponent<TextMeshProUGUI>();
            loss.text = "You Lost!";
            var shameText = GameObject.Find("Outcome/Canvas/Flavor Text").GetComponent<TextMeshProUGUI>();
            shameText.text = "Looks like you need to work on your jousting skills. Better luck next time!";
        }

        foreach (var unlock in gameController.newUnlocks)
        {
            GameObject.Find(unlock).transform.Translate(increment,0,0);
            increment += 200;
            maxRight += 200;
            string currentText = GameObject.Find(unlock).transform.Find("Canvas/Text Text").GetComponent<TextMeshProUGUI>().text;
            GameObject.Find(unlock).transform.Find("Canvas/Text Text").GetComponent<TextMeshProUGUI>().text = "Unlocked " + currentText;
        }

        var arrowController = GameObject.Find("CameraPivot/Main Camera").GetComponent<Achievements>();

        arrowController.maxRight = maxRight;
        GameObject.Find("Main Menu").transform.Translate(increment, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
