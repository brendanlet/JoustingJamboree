
﻿
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    public static GameController Instance;
    private Dictionary<string, bool> unlocks;
    private string unlocksPath;
    public int playerWins = 0;
    public int enemyWins = 0;
    public int roundsToWin = 5;
    private string lastLoadedScene = "Main Menu";
    public bool gameOutcome = false;

    public string playerMount;
    public string playerWeapon;

    private int panWins = 0;
    private int ballWins = 0;
    private int pencilWins = 0;
    private int fishWins = 0;
    public int matchesWon = 0;
    public int matchesLost = 0;
    int[] singleUnlocks = new int[16];

    public AudioClip joustMusic;
    public AudioClip unlockMusic;

    public List<string> newUnlocks;

    // Start is called before the first frame update
    void Start()
    {

        unlocks = new Dictionary<string, bool>();

        Debug.Log(Application.persistentDataPath);
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            unlocksPath = Application.persistentDataPath + "\\unlocks.txt";
        }
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            unlocksPath = Application.persistentDataPath + "/unlocks.txt";
        }

        var played = PlayerPrefs.GetInt("firstTime");
        if (played == 0)
        {
            Debug.Log("Firsties");
            unlocks = new Dictionary<string, bool>()
            {
                {"Cheetah", true},
                {"Horse", true },
                {"Beach Ball", false},
                {"Elephant Fish", false},
                {"Axe", false },
                {"Bat", true},
                {"Candy Cane", false},
                {"Chainsaw", false},
                {"Frying Pan", false},
                {"Lamp", false},
                {"Light Saber", false},
                {"Microphone", false},
                {"Pencil", false},
                {"Sword", true},
                {"Tennis Racket", false},
                {"Thor Hammer", false}
            };

            SaveUnlocks();

            PlayerPrefs.SetInt("firstTime", 1);
        }
        else
        {
            Debug.Log("NotFirst");
            LoadUnlocks();
        }




        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "JoustRound")
        {
            GetComponent<AudioSource>().volume = 0.35f;
        }
        else
        {
            GetComponent<AudioSource>().volume = .75f;
        }
        if (scene.name == "AchievementsUnlocked")
        {
            GetComponent<AudioSource>().clip = unlockMusic;
            GetComponent<AudioSource>().Play();
        }
        else if (lastLoadedScene == "AchievementsUnlocked")
        {
            GetComponent<AudioSource>().clip = joustMusic;
            GetComponent<AudioSource>().Play();
        }
    }

    void OnSceneUnload(Scene scene)
    {
        lastLoadedScene = scene.name;
    }

    void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }



    void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RoundWinPlayer()
    {
        if (playerWeapon == "Frying Pan")
        {
            panWins += 1;
        }
        if (playerMount == "Beach Ball")
        {
            ballWins += 1;
        }
        if (playerWeapon == "Pencil")
        {
            pencilWins += 1;
        }
        if (playerMount == "Elephant Fish")
        {
            fishWins += 1;
        }
        CheckForUnlocks();

        playerWins += 1;
        if (playerWins >= roundsToWin)
        {
            enemyWins = 0;
            playerWins = 0;
            VictoryPlayer();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void CheckForUnlocks()
    {
        if (matchesWon == 1 && singleUnlocks[2] == 0)
        {
            Debug.Log("Unlocked Beach Ball");
            Unlock("Beach Ball");
            singleUnlocks[2] = 1;
        }
        else if (matchesWon == 7 && singleUnlocks[15] == 0)
        {
            Unlock("Thor Hammer");
            singleUnlocks[15] = 1;
        }
        else if (matchesWon == 10 && singleUnlocks[10] == 0)
        {
            Unlock("Light Saber");
            singleUnlocks[10] = 1;
        }
        if (playerMount == "Cheetah" && playerWeapon == "Bat" && singleUnlocks[8] == 0)
        {
            Unlock("Frying Pan");
            singleUnlocks[8] = 1;
        }
        if (playerMount == "Horse" && playerWeapon == "Sword" && singleUnlocks[4] == 0)
        {
            Unlock("Axe");
            singleUnlocks[4] = 1;
        }
        if (playerMount == "Horse" && playerWeapon == "Microphone" && singleUnlocks[14] == 0)
        {
            Unlock("Tennis Racket");
            singleUnlocks[14] = 1;
        }

        if (ballWins == 3 && singleUnlocks[12]==0)
        {
            Unlock("Pencil");
            singleUnlocks[12] = 1;
        }
        if (panWins == 3 && singleUnlocks[9]==0)
        {
            Unlock("Lamp");
            singleUnlocks[9] = 1;
        }
        if (pencilWins == 3 && singleUnlocks[6]==0)
        {
            Unlock("Candy Cane");
            singleUnlocks[6] = 1;
        }
        if (fishWins == 3 && singleUnlocks[7]==0)
        {
            Unlock("Chainsaw");
            singleUnlocks[7] = 1;
        }
    }

    public void RoundWinEnemy()
    {
        enemyWins += 1;

        if (playerWeapon == "Lamp" && playerMount == "Elephant Fish" && singleUnlocks[11] == 0)
        {
            Unlock("Microphone");
            singleUnlocks[11] = 1;
        }

        if (enemyWins >= roundsToWin)
        {
            enemyWins = 0;
            playerWins = 0;
            VictoryEnemy();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void VictoryPlayer()
    {
        matchesWon += 1;
        gameOutcome = true;
        CheckForUnlocks();
        LoadScene(4);
    }

    public void VictoryEnemy()
    {
        matchesLost += 1;
        gameOutcome = false;
        if (matchesLost == 1)
        {
            Unlock("Elephant Fish");
        }
        LoadScene(4);
    }

    public void Unlock(string unlockName)
    {
        newUnlocks.Add(unlockName);
        unlocks[unlockName] = true;
        SaveUnlocks();
    }

    public List<string> GetUnlocked()
    {
        List<string> unlockedItems = new List<string>();
        foreach (var unlockable in unlocks.Keys)
        {
            if (unlocks[unlockable] == true)
            {
                unlockedItems.Add(unlockable);
            }
        }
        return unlockedItems;
    }

    public List<string> GetLocked()
    {
        List<string> lockedItems = new List<string>();
        foreach (var unlockable in unlocks.Keys)
        {
            if (unlocks[unlockable] == false)
            {
                lockedItems.Add(unlockable);
            }
        }
        return lockedItems;
    }

    private void SaveUnlocks()
    {
        List<string> serializedStrings = new List<string>();
        foreach (var key in unlocks.Keys)
        {
            serializedStrings.Add(key + ":" + unlocks[key]);
        }
        File.WriteAllLines(unlocksPath, serializedStrings);
    }


    private void LoadUnlocks()
    {
        int i = 0;
        var readUnlocks = File.ReadAllLines(unlocksPath);
        foreach (var line in readUnlocks)
        {
            bool val = false;
            var unlock = line.Substring(0, line.IndexOf(':'));
            var stringVal = line.Substring(line.IndexOf(':') + 1);
            if (stringVal == "True")
            {
                val = true;
                singleUnlocks[i] = 1;
            }
            else if (stringVal == "False")
            {
                val = false;
                singleUnlocks[i] = 0;
            }
            unlocks.Add(unlock, val);
            i++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (SceneManager.GetActiveScene().name != "Main Menu")
            {
                SceneManager.LoadScene(0);
            }
        }

        if (Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha2))
        {
            unlocks = new Dictionary<string, bool>()
            {
                {"Cheetah", true},
                {"Horse", true },
                {"Beach Ball", false},
                {"Elephant Fish", false},
                {"Axe", false },
                {"Bat", true},
                {"Candy Cane", false},
                {"Chainsaw", false},
                {"Frying Pan", false},
                {"Lamp", false},
                {"Light Saber", false},
                {"Microphone", false},
                {"Pencil", false},
                {"Sword", true},
                {"Tennis Racket", false},
                {"Thor Hammer", false}
            };

            for (int i = 0; i < singleUnlocks.Length; i++)
            {
                singleUnlocks[i] = 0;
            }
            SaveUnlocks();
        }
        else if (Input.GetKey(KeyCode.Alpha3) && Input.GetKey(KeyCode.Alpha4))
        {
            unlocks = new Dictionary<string, bool>()
            {
                {"Cheetah", true},
                {"Horse", true },
                {"Beach Ball", true},
                {"Elephant Fish", true},
                {"Axe", true },
                {"Bat", true},
                {"Candy Cane", true},
                {"Chainsaw", true},
                {"Frying Pan", true},
                {"Lamp", true},
                {"Light Saber", true},
                {"Microphone", true},
                {"Pencil", true},
                {"Sword", true},
                {"Tennis Racket", true},
                {"Thor Hammer", true}
            };

            for (int i=0; i<singleUnlocks.Length; i++)
            {
                singleUnlocks[i] = 1;
            }

            SaveUnlocks();
        }

    }

    public void clearCurrentUnlocks()
    {
        newUnlocks.Clear();
    }
}
