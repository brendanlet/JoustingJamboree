using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementUnlocks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gameController = GameObject.Find("GameController").GetComponent<GameController>();
        var lockedItems = gameController.GetLocked();
        foreach(var item in lockedItems)
        {
            Debug.Log(item);
            var itemTransform = GameObject.Find(item).transform;
            var itemPivot = itemTransform.Find("Pivot");
            var itemCanvas = itemTransform.Find("Canvas");
            itemPivot.Find(item + "Locked").gameObject.SetActive(true);
            itemPivot.Find(item + "Unlocked").gameObject.SetActive(false);
            itemCanvas.Find("Text Text").GetComponent<TextMeshProUGUI>().text = "?????";
            itemCanvas.Find("Flavor Text").GetComponent<TextMeshProUGUI>().text = "?????";

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
