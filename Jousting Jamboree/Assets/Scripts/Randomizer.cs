using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Randomizer : MonoBehaviour
{
    private GameController gameController;
    private Transform player;
    private Transform enemy;

    private List<GameObject> weaponList;
    private GameObject selectedWeapon;

    private List<GameObject> mountList;
    private GameObject selectedMount;

    public TMPro.TMP_Text displayedText;

    // public List<GameObject> riderList;
    // private GameObject selectedRider;

    // Start is called before the first frame update
    void Start()
    {
        mountList = new List<GameObject>();
        weaponList = new List<GameObject>();
        
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        List<string> unlockedItems = gameController.GetUnlocked();
        GameObject[] weaponPrefabs = Resources.LoadAll<GameObject>("Prefabs/Weapons");
        GameObject[] mountPrefabs = Resources.LoadAll<GameObject>("Prefabs/Mounts");

        foreach (var item in unlockedItems)
        {
            if(item == "Cheetah" || item == "Horse" || item == "Beach Ball" || item == "Elephant Fish")
            {
                foreach(var mountPrefab in mountPrefabs)
                {
                    if(mountPrefab.name == item)
                    {
                        Debug.Log(mountPrefab.name);
                        mountList.Add(mountPrefab);
                    }
                }
            }
            else
            {
                foreach (var weaponPrefab in weaponPrefabs)
                {
                    if (weaponPrefab.name == item)
                    {
                        weaponList.Add(weaponPrefab);
                    }
                }
            }
        }

        player = GameObject.Find("PlayerUnit/Player").transform;
        enemy = GameObject.Find("EnemyUnit/Enemy").transform;

        int randomWeapon = Random.Range(0, weaponList.Count);
        int randomMount = Random.Range(0, mountList.Count);

        selectedWeapon = weaponList[randomWeapon];
        selectedMount = mountList[randomMount];
        //  selectedRider = riderList[Random.Range(0, riderList.Count)];


        var mount = Instantiate(selectedMount, player.Find("Mount"));
        var enemyMount = Instantiate(selectedMount, enemy.Find("Mount"));
        mount.GetComponent<FixedJoint>().connectedBody = player.GetComponent<Rigidbody>();
        enemyMount.GetComponent<FixedJoint>().connectedBody = enemy.GetComponent<Rigidbody>();

        var weapon = Instantiate(selectedWeapon, player.Find("WeaponPivot"));
        var enemyWeapon = Instantiate(selectedWeapon, enemy.Find("WeaponPivot"));
        enemyWeapon.transform.Find("WeaponTip").tag = "EnemyWeaponHitBox";


        string weaponName = weapon.name.Substring(0, weapon.name.Length - 7);
        string mountName = mount.name.Substring(0, mount.name.Length - 7);
        gameController.playerWeapon = weaponName;
        gameController.playerMount = mountName;
        displayedText.text = "Weapon: " + weaponName + "\nMount: " + mountName;

        //GameObject PlayerUnit = 

        if(selectedMount.name == "Beach Ball")
        {
            player.parent.GetComponent<ConstantMovement>().speed = 15;
            enemy.parent.GetComponent<ConstantMovementEnemy>().speed = -15;
        }
        else if(selectedMount.name == "Cheetah")
        {
            player.parent.GetComponent<ConstantMovement>().speed = 30;
            enemy.parent.GetComponent<ConstantMovementEnemy>().speed = -30;
        }
        else if (selectedMount.name == "Elephant Fish")
        {
            player.parent.GetComponent<ConstantMovement>().speed = 20;
            enemy.parent.GetComponent<ConstantMovementEnemy>().speed = -20;
        }
        else if (selectedMount.name == "Horse")
        {
            player.parent.GetComponent<ConstantMovement>().speed = 25;
            enemy.parent.GetComponent<ConstantMovementEnemy>().speed = -25;
        }
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(3);
        displayedText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
