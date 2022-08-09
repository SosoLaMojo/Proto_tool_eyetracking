using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
public struct SaveData
{
    public GameLeaves gameLeaves;
    public GameSearchAndFind gameSearchAndFind;
    //Ringo
    public List <GameReading> gameReading1;
    //Lion
    public List<GameReading> gameReading2;
}

public enum EventType
{
    EYES,
    CLICK
}

[Serializable]
public class GameEvent
{
    public float time;
    public Vector3 position;
    public EventType eventType;
}

[Serializable]
public struct GameLeaves
{
    public List<GameEvent> events;
}

[Serializable]
public struct GameSearchAndFind
{
    public List<GameEvent> events;
    public int randomItem;
}

[Serializable]
public struct GameReading
{
    public List<GameEvent> events;
}

public class GameManager : MonoBehaviour
{
    public SaveData saveData;

    public string user_Name;
    private bool isRunning = false;

    public static GameManager instance;

    [SerializeField] GameObject point;
    [SerializeField] LineRenderer line;

    private string sceneName1 = "Leaves_Analyse";
    private string sceneName2 = "Search_and_Find_Analyze";

    // TODO écrire toute les variables de chaque scène de lecture des livres
    private string book1Scenes = "Ringo";
    // etc...

    public bool isLevelTwoUnlocked = false;
    public int currentPage = 0;
   

    private void Awake()
    {
        // Patern singleton pour s'assurer qu'il n'y a qu'un seul GameManager dans toutes les scènes,
        // et qu'il ne soit pas effacer en passant d'une scène à l'autre
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fonction qui permet de créer un Json
    public void CreateJsonGazePoint()
    {
        SaveData saveData = new SaveData();
        string json = JsonUtility.ToJson(saveData, true);
        WriteToFile(saveData);
    }

    // Fonction qui permet de créer le Json
    public void CreateJsonFile()
    {
        FileStream fileStream = new FileStream($"{ GameManager.instance.user_Name }.txt", FileMode.Create);
    }

    public void WriteToFile(SaveData newSaveData)
    {
        saveData = newSaveData;
        string json = JsonUtility.ToJson(saveData, true);
        // Ouvre et clear le Json
        FileStream fileStream = new FileStream($"{ GameManager.instance.user_Name }.txt", FileMode.Truncate);
        // écrit dans le Json
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    // Fonction qui permet de lire le Json
    public SaveData ReadJsonGazePoint()
    {
        // Lecture du json
        string json;
        StreamReader reader = new StreamReader($"{ GameManager.instance.user_Name }.txt", true);
        json = reader.ReadToEnd();

        // Traduit le string en structure SaveData
        saveData = JsonUtility.FromJson<SaveData>(json);

        return saveData;
    }

    // Fonction qui permet d'afficher le gazepoint du Json
    public void DrawJsonGazePoint()
    {
        // supprime les gazepoint préexistants
        int childs = transform.childCount;

        // Lecture des positions
        SaveData saveData;
        saveData = ReadJsonGazePoint();

        // Instantiation du gazepoint contenu dans le Json
        if (isRunning == false)
        {
            if(sceneName1 == SceneManager.GetActiveScene().name)
            {
                StartCoroutine(Wrapper(saveData.gameLeaves.events));
            }
            else if (sceneName2 == SceneManager.GetActiveScene().name)
            {
                StartCoroutine(Wrapper(saveData.gameSearchAndFind.events));
            }
            else if (SceneManager.GetActiveScene().name.Contains(book1Scenes))
            {
                StartCoroutine(Wrapper(saveData.gameReading1[currentPage].events));
            }
        }
    }

    // Coroutine qui lit une liste de la classe GameEvents et les affichent
    IEnumerator Wrapper(List<GameEvent> events)
    {
        float timeOffset = events[0].time;
        isRunning = true;
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].eventType == EventType.EYES)
            {
                Vector2 positionGazePoint = events[i].position;
                GameObject.Instantiate(point, positionGazePoint, Quaternion.identity, transform);
                line.positionCount = i + 1;
                line.SetPosition(i, positionGazePoint);
                yield return new WaitForSeconds(events[i].time - timeOffset);
                timeOffset = events[i].time;
            }
        }
        isRunning = false;
    }
}
